using Abp.Authorization;
using Abp.Domain.Repositories;
using AuctionSystem.Authorization;
using AuctionSystem.Categories.Dto;
using AuctionSystem.Locations;
using AuctionSystem.Locations.Dto;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuctionSystem.Common
{
    [AbpAuthorize]
    public class CommonLookUpAppService : AuctionSystemAppServiceBase, ICommonLookUpAppService
    {
        private readonly IRepository<AuctionSystem.Location.Location, long> _locationRepository;
        private readonly IRepository<AuctionSystem.Category.Category, long> _categoryRepository;
        public CommonLookUpAppService(IRepository<AuctionSystem.Location.Location, long> locationRepository, IRepository<Category.Category, long> categoryRepository)
        {
            _locationRepository = locationRepository;
            _categoryRepository = categoryRepository;
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

    }
}
