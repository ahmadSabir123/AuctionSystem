using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using AuctionSystem.Authorization.Users;

namespace AuctionSystem.UserLocation
{
    [Table("UserLocations")]
    public class UserLocation : FullAuditedEntity<long>, IMustHaveTenant
    {

        public virtual long? UserId { get; set; }

        [ForeignKey("UserId")]
        public User UserIdFk { get; set; }
        public virtual long? LocationId { get; set; }

        [ForeignKey("LocationId")]
        public AuctionSystem.Location.Location LocationIdFk { get; set; }
        public int TenantId { get; set; }

    }
}
