using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuctionSystem.Products.Dto
{
    public class GetAllProductsInput : PagedAndSortedResultRequestDto
    {
        public long? ProductId { get; set; }
        public string Filter { get; set; }
    }
}
