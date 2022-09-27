using AutoMapper;
using RestaurantChainApp.Dto;
using RestaurantChainApp.Entities;

namespace RestaurantChainApp.Mappings
{
    public class EntitiesToDtosProfile : Profile
    {
        public EntitiesToDtosProfile()
        {
            CreateMap<MenuItem, Dish>();
            CreateMap<MenuItem, Meal>();
        }
    }
}
