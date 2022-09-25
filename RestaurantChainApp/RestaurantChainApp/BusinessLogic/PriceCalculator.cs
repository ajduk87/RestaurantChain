using RestaurantChainApp.BusinessLogic.CalculationPriceStrategies;
using RestaurantChainApp.Dto;
using RestaurantChainApp.Entities;

namespace RestaurantChainApp.BusinessLogic
{
    public class PriceCalculator : IPriceCalculator
    {

        private CalculationPriceStrategy calculationPrice;


        public double Calculate(Dish dish, int amount) 
        {
            bool isHappyHour = false;

            CalculationPriceStrategy dishPriceStrategy = new DishPriceStrategy();
            CalculationPriceStrategy mealPriceStrategy = new MealPriceStrategy();

            calculationPrice = dish.IsMeal ? mealPriceStrategy : 
                                             dishPriceStrategy;

            double price = isHappyHour ? 0.8 * calculationPrice.Calculate(dish) :
                                         calculationPrice.Calculate(dish);

            return price;
        }
    }
}
