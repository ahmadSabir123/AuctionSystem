using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace AuctionSystem.Category
{
    [Table("Categories")]
    public class Category : FullAuditedEntity<long>, IMustHaveTenant
    {
        public string Name { get; set; }
        public int TenantId { get; set; }
    }
}
