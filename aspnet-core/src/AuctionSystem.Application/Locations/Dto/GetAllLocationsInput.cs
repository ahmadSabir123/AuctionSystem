using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuctionSystem.Locations.Dto
{
    public class GetAllLocationsInput : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }
        public int? LocationId { get; set; }
    }
}
