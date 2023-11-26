using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Collections.Extensions;
using Abp.Domain.Repositories;
using AuctionSystem.Authorization;
using System.Linq.Dynamic.Core;
using AuctionSystem.Categories.Dto;
using AuctionSystem.Auctions.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuctionSystem.Enums;
using AuctionSystem.Auction;
using AutoMapper.Internal.Mappers;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Collections;
using Abp.BackgroundJobs;
using AuctionSystem.BackgroundJobs;
using Hangfire;
using AuctionSystem.Products.Dto;
using Microsoft.EntityFrameworkCore;
using AuctionSystem.Authorization.Users;
using Abp.UI;

namespace AuctionSystem.Auctions
{
    [AbpAuthorize(PermissionNames.Pages_Auctions)]
    public class AuctionAppService : AuctionSystemAppServiceBase, IAuctionAppService
    {
        private readonly IRepository<AuctionSystem.Auction.Auction, long> _auctionRepository;
        private readonly IRepository<AuctionSystem.Product.Product, long> _productRepository;
        private readonly UserManager _userManager;
        private readonly IRepository<AuctionSystem.Location.Location, long> _locationRepository;
        private readonly IRepository<AuctionSystem.Category.Category, long> _categoryRepository;
        public AuctionAppService(IRepository<Auction.Auction, long> auctionRepository, IRepository<Product.Product, long> productRepository, IRepository<Location.Location, long> locationRepository, IRepository<Category.Category, long> categoryRepository, UserManager userManager)
        {
            _auctionRepository = auctionRepository;
            _productRepository = productRepository;
            _locationRepository = locationRepository;
            _categoryRepository = categoryRepository;
            _userManager = userManager;
        }
        public async Task<PagedResultDto<ProductDto>> GetAllAuctionProduct(GetAllAuctionsInput input)   
        {
            var currentUser = await UserManager.Users.Where(x => x.Id == AbpSession.UserId).FirstOrDefaultAsync();
            var data = await _productRepository.GetAll().Where(x => x.OwnerId != AbpSession.UserId && x.IsProductSale != true && x.Status == ProductStatus.InAuction && x.LocationId == currentUser.LocationId).ToListAsync();
            data = data
                .WhereIf(input.ProductId != null, x => x.Id == input.ProductId)
                .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => e.Name.ToLower().Trim().Contains(input.Filter.ToLower().Trim())).ToList();

            var pagination = data.Skip(input.SkipCount).Take(input.MaxResultCount);
            var product = (from o in pagination
                           join o1 in _locationRepository.GetAll() on o.LocationId equals o1.Id into j1
                           from s1 in j1.DefaultIfEmpty()

                           join o2 in _categoryRepository.GetAll() on o.CategoryId equals o2.Id into j2
                           from s2 in j2.DefaultIfEmpty()
                           select new ProductDto
                           {
                               BasePrice = o.BasePrice,
                               Name = o.Name,
                               Id = o.Id,
                               Status = o.Status,
                               AuctionStartAt = o.AuctionStartAt,
                               AuctionEndAt = o.AuctionEndAt,
                               ImageBase64 = Convert.ToBase64String(o.Image),
                               LocationName = s1 != null ? s1.Name : "",
                               CategoryName = s2 != null ? s2.Name : "",
                           }).ToList();
            return new PagedResultDto<ProductDto>(
                data.Count(), product);
        }

        public async Task<List<AuctionDto>> GetAllProducttBid(long productId)
        {
            var qurrey = _auctionRepository.GetAll().Where(x => x.ProductId == productId);
            var result = await (from o in qurrey
                                join o1 in _userManager.Users on o.UserId equals o1.Id into j1
                                from s1 in j1.DefaultIfEmpty()
                                select new AuctionDto
                                {
                                    Bid = o.Bid,
                                    UserName = s1 != null ? s1.UserName : "",

                                }).ToListAsync();
            return result;
        }
        public async Task<bool> AddUserBid(long productId, double bid)
        {
            var isHigherBidExist = await _auctionRepository.GetAll().Where(x => x.ProductId == productId && x.Bid >= bid).AnyAsync();
            var isProductInAuction = await _productRepository.GetAll().Where(x => x.Id == productId && x.Status == ProductStatus.InAuction).AnyAsync();
            if (isHigherBidExist)
            {
                throw new UserFriendlyException("Please add a Higher bid");
            }
            else if (isProductInAuction)
            {
                AuctionSystem.Auction.Auction auction = new Auction.Auction();
                auction.Bid = bid;
                auction.ProductId= productId;
                auction.UserId = AbpSession.UserId;
                auction.TenantId = (int)AbpSession.TenantId;
                await _auctionRepository.InsertAsync(auction);
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}

