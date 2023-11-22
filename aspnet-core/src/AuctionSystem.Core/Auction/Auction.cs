using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using AuctionSystem.Authorization.Users;

namespace AuctionSystem.Auction
{
    [Table("Auctions")]
    public class Auction : FullAuditedEntity<long>, IMustHaveTenant
    {
        public double? Bid { get; set; }
        public int TenantId { get; set; }
        
        public virtual long? UserId { get; set; }
        [ForeignKey("UserId")]
        public User UserFk { get; set; }
        public virtual long? ProductId { get; set; }
        [ForeignKey("ProductId")]
        public AuctionSystem.Product.Product ProductFk { get; set; }
    }
}
