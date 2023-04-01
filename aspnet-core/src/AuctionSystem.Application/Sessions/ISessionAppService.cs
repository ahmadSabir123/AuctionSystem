using System.Threading.Tasks;
using Abp.Application.Services;
using AuctionSystem.Sessions.Dto;

namespace AuctionSystem.Sessions
{
    public interface ISessionAppService : IApplicationService
    {
        Task<GetCurrentLoginInformationsOutput> GetCurrentLoginInformations();
    }
}
