using System.Data.Common;
using Microsoft.EntityFrameworkCore;

namespace AuctionSystem.EntityFrameworkCore
{
    public static class AuctionSystemDbContextConfigurer
    {
        public static void Configure(DbContextOptionsBuilder<AuctionSystemDbContext> builder, string connectionString)
        {
            builder.UseSqlServer(connectionString);
        }

        public static void Configure(DbContextOptionsBuilder<AuctionSystemDbContext> builder, DbConnection connection)
        {
            builder.UseSqlServer(connection);
        }
    }
}
