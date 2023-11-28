using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using AuctionSystem.Enums;
using AuctionSystem.Authorization.Users;

namespace AuctionSystem.Product
{
    [Table("Products")]
    public class Product : FullAuditedEntity<long>, IMustHaveTenant
    {
        public string Name { get; set; }
        public int TenantId { get; set; }
        public double? BasePrice { get; set; }
        public byte[] Image { get; set; }
        public ProductStatus Status { get; set; }
        public bool IsProductSale {  get; set; }
        public DateTime? AuctionStartAt { get; set; }
        public DateTime? AuctionEndAt { get; set; }
        public double? SoldPrice { get; set; }
        public virtual long? NewOwnerId { get; set; }
        [ForeignKey("NewOwnerId")]
        public User NewOwnerFk { get; set; }
        public virtual long? OwnerId { get; set; }
        [ForeignKey("OwnerId")]
        public User OwnerFk { get; set; }
        public virtual long? LocationId { get; set; }
        [ForeignKey("LocationId")]
        public AuctionSystem.Location.Location LocationIdFk { get; set; }
        public virtual long? CategoryId { get; set; }

        [ForeignKey("CategoryId")]
        public AuctionSystem.Category.Category CategoryFk { get; set; }
    }
}
