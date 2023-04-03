using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuctionSystem.Locations.Dto
{
    public class LocationDto : EntityDto<long?>
    {
        public string Name { get; set; }
    }
}
