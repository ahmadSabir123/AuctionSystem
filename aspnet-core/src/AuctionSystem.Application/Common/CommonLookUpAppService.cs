using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Collections.Extensions;
using Abp.Domain.Repositories;
using AuctionSystem.Auctions.Dto;
using AuctionSystem.Authorization;
using AuctionSystem.Categories.Dto;
using AuctionSystem.Enums;
using AuctionSystem.Locations;
using AuctionSystem.Locations.Dto;
using AuctionSystem.Products.Dto;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace AuctionSystem.Common
{
    [AbpAuthorize]
    public class CommonLookUpAppService : AuctionSystemAppServiceBase, ICommonLookUpAppService
    {
        private readonly IRepository<AuctionSystem.Location.Location, long> _locationRepository;
        private readonly IRepository<AuctionSystem.Category.Category, long> _categoryRepository;
        private readonly IRepository<AuctionSystem.Product.Product, long> _productRepository;
        private readonly IRepository<AuctionSystem.Auction.Auction, long> _auctionRepository;
        public CommonLookUpAppService(IRepository<AuctionSystem.Location.Location, long> locationRepository, IRepository<Category.Category, long> categoryRepository, IRepository<Product.Product, long> productRepository, IRepository<Auction.Auction, long> auctionRepository)
        {
            _locationRepository = locationRepository;
            _categoryRepository = categoryRepository;
            _productRepository = productRepository;
            _auctionRepository = auctionRepository;
        }
        public async Task<List<LocationDto>> GetAllLocationsForDropdown()
        {
            var data = await _locationRepository.GetAll().ToArrayAsync();
            return ObjectMapper.Map<List<LocationDto>>(data);
        }
        public async Task<List<CategoryDto>> GetAllCategoriesForDropdown()
        {
            var data = await _categoryRepository.GetAll().ToArrayAsync();
            return ObjectMapper.Map<List<CategoryDto>>(data);
        }
        public async Task<PagedResultDto<ProductDto>> GetAllSoldProducts(GetAllAuctionsInput input)
        {
            var currentUser = await UserManager.Users.Where(x => x.Id == AbpSession.UserId).FirstOrDefaultAsync();
            var data = await _productRepository.GetAll().Include(x => x.OwnerFk).Include(x => x.NewOwnerFk).Where(x => x.OwnerId == AbpSession.UserId && x.IsProductSale == true && x.Status == ProductStatus.AuctionDone).ToListAsync();
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
                               BuyFrom = o.OwnerFk != null ? o.OwnerFk.FullName : "",
                               SoldTo = o.NewOwnerFk != null ? o.NewOwnerFk.FullName : "",
                               SoldPrice = o.SoldPrice,
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
        public async Task<PagedResultDto<ProductDto>> GetAllReadyForSaleProducts(GetAllAuctionsInput input)
        {
            var currentUser = await UserManager.Users.Where(x => x.Id == AbpSession.UserId).FirstOrDefaultAsync();
            var data = await _productRepository.GetAll().Where(x => x.OwnerId == AbpSession.UserId && x.IsProductSale == false && x.Status == ProductStatus.AuctionDone).ToListAsync();
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
                               SoldPrice = o.SoldPrice,
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
        public async Task<PagedResultDto<ProductDto>> GetAllBuyProducts(GetAllAuctionsInput input)
        {
            var currentUser = await UserManager.Users.Where(x => x.Id == AbpSession.UserId).FirstOrDefaultAsync();
            var data = await _productRepository.GetAll().Include(x=>x.OwnerFk).Include(x=>x.NewOwnerFk).Where(x => x.NewOwnerId == AbpSession.UserId && x.IsProductSale == true).ToListAsync();
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
                               BuyFrom = o.OwnerFk != null ? o.OwnerFk.FullName : "",
                               SoldTo = o.NewOwnerFk != null ? o.NewOwnerFk.FullName : "",
                               SoldPrice = o.SoldPrice,
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

        public async Task SellProduct(long productId)
        {
            var product = await _productRepository.FirstOrDefaultAsync(x => x.Id == productId);
            var auctionRecord = await _auctionRepository.GetAll().Where(x => x.ProductId == productId).OrderByDescending(x => x.Bid).FirstOrDefaultAsync();
            product.NewOwnerId = auctionRecord.UserId;
            product.SoldPrice = auctionRecord.Bid;
            product.IsProductSale = true;
            await _productRepository.UpdateAsync(product);
        }

        public async Task<AuctionDto> GetHigherBidUserDetail(long productId)
        {
            var auctionRecord = await _auctionRepository.GetAll().Include(x=>x.UserFk).Include(x=>x.ProductFk).Where(x => x.ProductId == productId).OrderByDescending(x => x.Bid).FirstOrDefaultAsync();
            var data = ObjectMapper.Map<AuctionDto>(auctionRecord);
            data.UserName = auctionRecord.UserFk.Name;
            data.ProductName = auctionRecord.ProductFk.Name;
            return data;
        }
        public async Task<long> GetAllSoldProductsCount()
        {
            return await _productRepository.GetAll().Include(x => x.OwnerFk).Include(x => x.NewOwnerFk).Where(x => x.OwnerId == AbpSession.UserId && x.IsProductSale == true && x.Status == ProductStatus.AuctionDone).CountAsync();
        }
        public async Task<long> GetAllReadyForSaleProductsCount()
        {
            return await _productRepository.GetAll().Where(x => x.OwnerId == AbpSession.UserId && x.IsProductSale == false && x.Status == ProductStatus.AuctionDone).CountAsync();

        }
        public async Task<long> GetAllBuyProductsCount()
        {
            return await _productRepository.GetAll().Include(x => x.OwnerFk).Include(x => x.NewOwnerFk).Where(x => x.NewOwnerId == AbpSession.UserId && x.IsProductSale == true).CountAsync();
        }
        public async Task<long> GetAllProductCount()
        {
            return await _productRepository.GetAll().Where(x => x.OwnerId == AbpSession.UserId).CountAsync();
        }

    }
}
