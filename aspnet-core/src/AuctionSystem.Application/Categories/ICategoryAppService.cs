using Abp.Application.Services;
using Abp.Application.Services.Dto;
using AuctionSystem.Categories.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuctionSystem.Categorys
{
    public interface ICategoryAppService : IApplicationService
    {
        Task<long> CreateOrEdit(CategoryDto input);
        Task Delete(EntityDto<long> input);
    }
}
