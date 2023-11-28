using Abp.Application.Services;
using Abp.Application.Services.Dto;
using AuctionSystem.Auctions.Dto;
using AuctionSystem.Categories.Dto;
using AuctionSystem.Locations.Dto;
using AuctionSystem.Products.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuctionSystem.Common
{
    public interface ICommonLookUpAppService : IApplicationService
    {
        Task<long> GetAllSoldProductsCount();
        Task<long> GetAllReadyForSaleProductsCount();
        Task<long> GetAllBuyProductsCount();
        Task<long> GetAllProductCount();
        Task<AuctionDto> GetHigherBidUserDetail(long productId);
        Task<PagedResultDto<ProductDto>> GetAllReadyForSaleProducts(GetAllAuctionsInput input);
        Task SellProduct(long productId);
        Task<PagedResultDto<ProductDto>> GetAllBuyProducts(GetAllAuctionsInput input);
        Task<PagedResultDto<ProductDto>> GetAllSoldProducts(GetAllAuctionsInput input);
        Task<List<LocationDto>> GetAllLocationsForDropdown();
        Task<List<CategoryDto>> GetAllCategoriesForDropdown();
    }
}
