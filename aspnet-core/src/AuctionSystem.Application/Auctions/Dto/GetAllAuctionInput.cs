using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuctionSystem.Auctions.Dto
{
    public class GetAllAuctionsInput : PagedAndSortedResultRequestDto
    {
        public long? ProductId { get; set; }
        public string Filter { get; set; }
    }
}
