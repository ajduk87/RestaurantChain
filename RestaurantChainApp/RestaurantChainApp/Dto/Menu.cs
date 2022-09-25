using RestaurantChainApp.Entities;
using System.Collections.Generic;

namespace RestaurantChainApp.Dto
{
    public class Menu : Entity
    {
        public List<MenuItem> Items { get; set; }

        public Menu(List<MenuItem> items)
        {
            Items = items;
        }
    }
}
