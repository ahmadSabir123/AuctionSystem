using Abp.Authorization;
using AuctionSystem.Authorization.Roles;
using AuctionSystem.Authorization.Users;

namespace AuctionSystem.Authorization
{
    public class PermissionChecker : PermissionChecker<Role, User>
    {
        public PermissionChecker(UserManager userManager)
            : base(userManager)
        {
        }
    }
}
