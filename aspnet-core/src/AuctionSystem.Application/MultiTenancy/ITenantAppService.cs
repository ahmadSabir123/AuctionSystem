using Abp.Application.Services;
using AuctionSystem.MultiTenancy.Dto;

namespace AuctionSystem.MultiTenancy
{
    public interface ITenantAppService : IAsyncCrudAppService<TenantDto, int, PagedTenantResultRequestDto, CreateTenantDto, TenantDto>
    {
    }
}

