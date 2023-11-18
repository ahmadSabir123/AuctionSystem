using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Collections.Extensions;
using Abp.Domain.Repositories;
using AuctionSystem.Authorization;
using System.Linq.Dynamic.Core;
using AuctionSystem.Categories.Dto;
using AuctionSystem.Products.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuctionSystem.Enums;
using AuctionSystem.Product;
using AutoMapper.Internal.Mappers;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Collections;

namespace AuctionSystem.Products
{
    [AbpAuthorize(PermissionNames.Pages_Products)]
    public class ProductAppService : AuctionSystemAppServiceBase, IProductAppService
    {
        private readonly IRepository<AuctionSystem.Product.Product, long> _productRepository;
        public ProductAppService(IRepository<Product.Product, long> productRepository)
        {
            _productRepository = productRepository;
        }
        public async Task<long> CreateOrEdit(ProductDto input)
        {
            if (input.Id == null)
            {
                return await Create(input);
            }
            else
            {
                return await Update(input);
            }
        }
        protected virtual async Task<long> Create(ProductDto input)
        {
            var product = ObjectMapper.Map<AuctionSystem.Product.Product>(input);
            product.Image = Convert.FromBase64String(input.ImageBase64);
            product.OwnerId = AbpSession.UserId;
            product.Status = ProductStatus.ReadyForAuction;


            if (AbpSession.TenantId != null)
            {
                product.TenantId = (int)AbpSession.TenantId;
            }

            input.Id = await _productRepository.InsertAndGetIdAsync(product);
            return (long)input.Id;
        }
        protected virtual async Task<long> Update(ProductDto input)
        {
            var product = await _productRepository.FirstOrDefaultAsync((long)input.Id);
            product.Image = Convert.FromBase64String(input.ImageBase64);
            product.OwnerId = AbpSession.UserId;
            var data = ObjectMapper.Map(input, product);
            await _productRepository.UpdateAsync(data);
            return (long)input.Id;

        }
        public async Task<PagedResultDto<ProductDto>> GetAllProduct(GetAllProductsInput input)   
        {

            var data = _productRepository.GetAll().Where(x=>x.OwnerId==AbpSession.UserId)
                .WhereIf(input.ProductId != null, x => x.Id == input.ProductId)
                .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => e.Name.ToLower().Trim().Contains(input.Filter.ToLower().Trim()));
            var location = data.Skip(input.SkipCount).Take(input.MaxResultCount).ToList();
            return new PagedResultDto<ProductDto>(
                data.Count(),
                ObjectMapper.Map<List<ProductDto>>(location)
            );
        }

        public async Task Delete(EntityDto<long> input)
        {
            await _productRepository.DeleteAsync(input.Id);
        }
        public async Task<ProductDto> GetProductForEdit(long productId)
        {
            var product = await _productRepository.FirstOrDefaultAsync(x => x.Id == productId);
            var date = ObjectMapper.Map<ProductDto>(product);
            date.ImageBase64 = Convert.ToBase64String(date.Image);
            return date;
        }
    }
}

