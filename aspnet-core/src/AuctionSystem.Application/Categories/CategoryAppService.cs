

using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Collections.Extensions;
using Abp.Domain.Repositories;
using AuctionSystem.Authorization;
using AuctionSystem.Categories.Dto;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuctionSystem.Categorys
{
    [AbpAuthorize(PermissionNames.Pages_Categories)]
    public class CategoryAppService : AuctionSystemAppServiceBase, ICategoryAppService
    {
        private readonly IRepository<AuctionSystem.Category.Category,long> _categoryRepository;
        public CategoryAppService(IRepository<AuctionSystem.Category.Category, long> categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }
        public async Task<long> CreateOrEdit(CategoryDto input)
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
        protected virtual async Task<long> Create(CategoryDto input)
        {
            var Category = ObjectMapper.Map<AuctionSystem.Category.Category>(input);

            if (AbpSession.TenantId != null)
            {
                Category.TenantId = (int)AbpSession.TenantId;
            }

            input.Id = await _categoryRepository.InsertAndGetIdAsync(Category);
            return (long)input.Id;
        }
        protected virtual async Task<long> Update(CategoryDto input)
        {
            var supplier = await _categoryRepository.FirstOrDefaultAsync((long)input.Id);
            ObjectMapper.Map(input, supplier);
            return (long)input.Id;

        }
        public async Task<PagedResultDto<CategoryDto>> GetAllCategories(GetAllCategoriesInput input)
        {

            var data = _categoryRepository.GetAll()
                .WhereIf(input.CatagoryId != null, x => x.Id == input.CatagoryId)
                .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => e.Name.ToLower().Trim().Contains(input.Filter.ToLower().Trim()));
            var location = data.Skip(input.SkipCount).Take(input.MaxResultCount).ToList();
            return new PagedResultDto<CategoryDto>(
                data.Count(),
                ObjectMapper.Map<List<CategoryDto>>(location)
            );
        }

        public async Task Delete(EntityDto<long> input)
        {
            await _categoryRepository.DeleteAsync(input.Id);
        }
    }
}
