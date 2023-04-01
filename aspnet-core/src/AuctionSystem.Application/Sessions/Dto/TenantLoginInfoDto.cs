using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using AuctionSystem.MultiTenancy;

namespace AuctionSystem.Sessions.Dto
{
    [AutoMapFrom(typeof(Tenant))]
    public class TenantLoginInfoDto : EntityDto
    {
        public string TenancyName { get; set; }

        public string Name { get; set; }
    }
}
