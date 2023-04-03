using AuctionSystem.Categories.Dto;
using AuctionSystem.Locations.Dto;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuctionSystem
{
    public class CustomDtoMapper
    {
        public static void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<LocationDto, AuctionSystem.Location.Location>().ReverseMap();
            configuration.CreateMap<CategoryDto, AuctionSystem.Category.Category>().ReverseMap();
        }
    }
}
