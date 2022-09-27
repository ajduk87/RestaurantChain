using AutoMapper;
using RestaurantChainApp.Dtoes;
using RestaurantChainApp.Entities;

namespace RestaurantChainApp.Mappings
{
    public class DtoToEntitesProfile : Profile
    {
        public DtoToEntitesProfile()
        {
            CreateMap<OrderDto, Order>();
            CreateMap<OrderItemDto, OrderItem>();
        }
    }
}
