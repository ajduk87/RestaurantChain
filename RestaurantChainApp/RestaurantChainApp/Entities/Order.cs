using System.Collections.Generic;

namespace RestaurantChainApp.Entities
{
    public class Order : Entity
    {
        public List<OrderItem> orderItems { get; set; }
        public bool IsCheckouted { get; set; }
    }
}
