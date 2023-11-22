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

namespace AuctionSystem.Auctions
{
    public interface IAuctionAppService : IApplicationService
    {
        Task<PagedResultDto<ProductDto>> GetAllAuctionProduct(GetAllAuctionsInput input);
        Task<List<AuctionDto>> GetAllProducttBid(long productId);
        Task<bool> AddUserBid(long productId, double bid);
    }
}
