using Abp.Application.Services;
using AuctionSystem.Categories.Dto;
using AuctionSystem.Locations.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuctionSystem.Common
{
    public interface ICommonLookUpAppService : IApplicationService
    {
        Task<List<LocationDto>> GetAllLocationsForDropdown();
        Task<List<CategoryDto>> GetAllCategoriesForDropdown();
    }
}
