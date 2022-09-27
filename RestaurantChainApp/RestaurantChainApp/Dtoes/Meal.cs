using System;
using System.Collections.Generic;

namespace RestaurantChainApp.Dtoes
{
    public class Meal : Dish, ICloneable
    {
        public List<Dish> Dishes { get; set; }

        public void AddDish(Dish dish)
        {
            Dishes = Dishes ?? new List<Dish>();
            Dishes.Add(dish);
        }

        object ICloneable.Clone()
        {
            List<Dish> dishes = new List<Dish>();

            foreach (Dish dish in this.Dishes) 
            {
                dishes.Add(dish);
            }

            return new Meal 
            {
                Name = this.Name,
                Description = this.Description,
                Price = this.Price,
                IsMeal = this.IsMeal,
                Dishes = dishes
            };
        }
    }
}
