using Abp.Application.Services;
using Abp.Application.Services.Dto;
using AuctionSystem.Categories.Dto;
using AuctionSystem.Locations.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuctionSystem.Locations
{
    public interface ILocationAppService : IApplicationService
    {
        Task<long> CreateOrEdit(LocationDto input);
        Task Delete(EntityDto<long> input);
    }
}
