using System.Threading.Tasks;
using AuctionSystem.Configuration.Dto;

namespace AuctionSystem.Configuration
{
    public interface IConfigurationAppService
    {
        Task ChangeUiTheme(ChangeUiThemeInput input);
    }
}
