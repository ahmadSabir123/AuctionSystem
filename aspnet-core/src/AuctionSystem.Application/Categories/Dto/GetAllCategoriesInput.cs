using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuctionSystem.Categories.Dto
{
    public class GetAllCategoriesInput : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }
        public int? CatagoryId { get; set; }
    }
}
