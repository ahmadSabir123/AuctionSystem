using System.Threading.Tasks;
using Abp.Application.Services;
using AuctionSystem.Authorization.Accounts.Dto;

namespace AuctionSystem.Authorization.Accounts
{
    public interface IAccountAppService : IApplicationService
    {
        Task<IsTenantAvailableOutput> IsTenantAvailable(IsTenantAvailableInput input);

        Task<RegisterOutput> Register(RegisterInput input);
    }
}
