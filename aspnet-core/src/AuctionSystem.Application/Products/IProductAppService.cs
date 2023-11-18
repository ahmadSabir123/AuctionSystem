using Abp.Application.Services;
using Abp.Application.Services.Dto;
using AuctionSystem.Categories.Dto;
using AuctionSystem.Locations.Dto;
using AuctionSystem.Products.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuctionSystem.Products
{
    public interface IProductAppService : IApplicationService
    {
        Task<ProductDto> GetProductForEdit(long productId);
        Task<long> CreateOrEdit(ProductDto input);
        Task Delete(EntityDto<long> input);
        Task<PagedResultDto<ProductDto>> GetAllProduct(GetAllProductsInput input);
    }
}
