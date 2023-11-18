using Microsoft.EntityFrameworkCore;
using Abp.Zero.EntityFrameworkCore;
using AuctionSystem.Authorization.Roles;
using AuctionSystem.Authorization.Users;
using AuctionSystem.MultiTenancy;


namespace AuctionSystem.EntityFrameworkCore
{
    public class AuctionSystemDbContext : AbpZeroDbContext<Tenant, Role, User, AuctionSystemDbContext>
    {
        public virtual DbSet<AuctionSystem.Location.Location> Locations { get; set; }


        public virtual DbSet<AuctionSystem.Category.Category> Categories { get; set; }
        public virtual DbSet<AuctionSystem.Product.Product> Products { get; set; }

        public AuctionSystemDbContext(DbContextOptions<AuctionSystemDbContext> options)
            : base(options)
        {
        }
    }
}
