using RestaurantChainApp.Entities;
using System.Collections.Generic;

namespace RestaurantChainApp.Dtoes
{
    public class Menu : Dto
    {
        public List<Dish> Dishes { get; set; }

        public Menu(List<Dish> dishes)
        {
            Dishes = dishes;
        }
    }
}
