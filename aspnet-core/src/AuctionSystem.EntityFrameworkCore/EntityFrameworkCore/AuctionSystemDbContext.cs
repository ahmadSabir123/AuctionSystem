using Microsoft.EntityFrameworkCore;
using Abp.Zero.EntityFrameworkCore;
using AuctionSystem.Authorization.Roles;
using AuctionSystem.Authorization.Users;
using AuctionSystem.MultiTenancy;

namespace AuctionSystem.EntityFrameworkCore
{
    public class AuctionSystemDbContext : AbpZeroDbContext<Tenant, Role, User, AuctionSystemDbContext>
    {
        /* Define a DbSet for each entity of the application */
        
        public AuctionSystemDbContext(DbContextOptions<AuctionSystemDbContext> options)
            : base(options)
        {
        }
    }
}
