using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuctionSystem.Location
{
    [Table("Locations")]
    public class Location : FullAuditedEntity<long>, IMustHaveTenant
    {
        public string Name { get; set; }
        public int UserId { get; set; }
        public int TenantId { get; set; }
    }
}
