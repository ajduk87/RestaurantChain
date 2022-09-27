using RestaurantChainApp.Entities;
using RestaurantChainApp.Enums;
using System.Collections.Generic;

namespace RestaurantChainApp.Dto
{
    public class OrderDto : Dto
    {
        public List<OrderItem> orderItems { get; set; }
        public double Total { get; set; }
        public OrderStatus Status { get; set; }
    }
}
