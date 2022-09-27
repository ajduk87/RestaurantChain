using System;

namespace RestaurantChainApp.Dtoes
{
    public class Dish : Dto, ICloneable
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public bool IsMeal { get; set; }

        public object Clone()
        {
            return new Dish 
            {
                Name = this.Name,
                Description = this.Description,
                Price = this.Price,
                IsMeal = this.IsMeal
            };
        }
    }
}
