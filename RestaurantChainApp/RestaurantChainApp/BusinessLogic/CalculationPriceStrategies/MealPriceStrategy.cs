using RestaurantChainApp.Dto;
using System.Linq;

namespace RestaurantChainApp.BusinessLogic.CalculationPriceStrategies
{
    public class MealPriceStrategy : CalculationPriceStrategy
    {
        public override double Calculate(Dish dish)
        {
            Meal meal = (Meal)dish;
            return 0.9 * meal.Dishes.Sum(dish => dish.Price);
        }
    }
}
