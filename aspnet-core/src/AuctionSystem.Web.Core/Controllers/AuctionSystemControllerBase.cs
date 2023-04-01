using Abp.AspNetCore.Mvc.Controllers;
using Abp.IdentityFramework;
using Microsoft.AspNetCore.Identity;

namespace AuctionSystem.Controllers
{
    public abstract class AuctionSystemControllerBase: AbpController
    {
        protected AuctionSystemControllerBase()
        {
            LocalizationSourceName = AuctionSystemConsts.LocalizationSourceName;
        }

        protected void CheckErrors(IdentityResult identityResult)
        {
            identityResult.CheckErrors(LocalizationManager);
        }
    }
}
