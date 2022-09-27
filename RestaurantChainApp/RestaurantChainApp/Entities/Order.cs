using RestaurantChainApp.Enums;
using System.Collections.Generic;

namespace RestaurantChainApp.Entities
{
    public class Order : Entity
    {
        public List<OrderItem> orderItems { get; set; }
        public double Total { get; set; }
        public OrderStatus Status { get; set; }
    }
}
