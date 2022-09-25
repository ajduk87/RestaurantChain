using System.Collections.Generic;

namespace RestaurantChainApp.Dto
{
    public class Meal : Dish
    {
        public List<Dish> Dishes = new List<Dish>();

        public void AddDish(Dish dish)
        {
            Dishes.Add(dish);
        }
    }
}
