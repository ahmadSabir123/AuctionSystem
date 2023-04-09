using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Collections.Extensions;
using Abp.Domain.Repositories;
using AuctionSystem.Authorization;
using System.Linq.Dynamic.Core;
using AuctionSystem.Categories.Dto;
using AuctionSystem.Locations.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuctionSystem.Locations
{
    [AbpAuthorize(PermissionNames.Pages_Locations)]
    public class LocationAppService: AuctionSystemAppServiceBase, ILocationAppService
    {
        private readonly IRepository<AuctionSystem.Location.Location, long> _locationRepository;
        public LocationAppService(IRepository<AuctionSystem.Location.Location, long> locationRepository)
        {
            _locationRepository = locationRepository;
        }
        public async Task<long> CreateOrEdit(LocationDto input)
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
        protected virtual async Task<long> Create(LocationDto input)
        {
            var Category = ObjectMapper.Map<AuctionSystem.Location.Location>(input);

            if (AbpSession.TenantId != null)
            {
                Category.TenantId = (int)AbpSession.TenantId;
            }

            input.Id = await _locationRepository.InsertAndGetIdAsync(Category);
            return (long)input.Id;
        }
        protected virtual async Task<long> Update(LocationDto input)
        {
            var supplier = await _locationRepository.FirstOrDefaultAsync((long)input.Id);
            ObjectMapper.Map(input, supplier);
            return (long)input.Id;

        }
        public async Task<PagedResultDto<LocationDto>> GetAllLocation(GetAllLocationsInput input)
        {

            var data =  _locationRepository.GetAll().WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => e.Name.ToLower().Trim().Contains(input.Filter.ToLower().Trim()));
            var location =  data.Skip(input.SkipCount).Take(input.MaxResultCount).ToList();
            return new PagedResultDto<LocationDto>(
                data.Count(),
                ObjectMapper.Map<List<LocationDto>>(location)
            );
        }

        public async Task Delete(EntityDto<long> input)
        {
            await _locationRepository.DeleteAsync(input.Id);
        }
    }
}

