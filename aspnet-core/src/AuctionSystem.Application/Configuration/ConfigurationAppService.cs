using System.Threading.Tasks;
using Abp.Authorization;
using Abp.Runtime.Session;
using AuctionSystem.Configuration.Dto;

namespace AuctionSystem.Configuration
{
    [AbpAuthorize]
    public class ConfigurationAppService : AuctionSystemAppServiceBase, IConfigurationAppService
    {
        public async Task ChangeUiTheme(ChangeUiThemeInput input)
        {
            await SettingManager.ChangeSettingForUserAsync(AbpSession.ToUserIdentifier(), AppSettingNames.UiTheme, input.Theme);
        }
    }
}
