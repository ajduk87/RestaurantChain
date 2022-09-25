using RestaurantChainApp.BusinessLogic.CalculationPriceStrategies;
using RestaurantChainApp.Dto;
using RestaurantChainApp.Entities;
using RestaurantChainApp.Factories;
using System;

namespace RestaurantChainApp.BusinessLogic
{
    public class PriceCalculator : IPriceCalculator
    {

        private readonly EnvironmentSettings envSettings;
        private CalculationPriceStrategy calculationPrice;

        public PriceCalculator(IEnvironmentSettingsFactory environmentSettingsFactory)
        {
            envSettings = environmentSettingsFactory.GetEnvironmentSettings();
        }

        private bool IsHappyHour() 
        {
            int currentHour = DateTime.Now.Hour;
            return currentHour >= envSettings.HappyHourBegin && currentHour <= envSettings.HappyHourEnd;
        }


        public double Calculate(Dish dish, int amount) 
        {
            CalculationPriceStrategy dishPriceStrategy = new DishPriceStrategy();
            CalculationPriceStrategy mealPriceStrategy = new MealPriceStrategy();

            calculationPrice = dish.IsMeal ? mealPriceStrategy : 
                                             dishPriceStrategy;

            double price = IsHappyHour() ? 0.8 * calculationPrice.Calculate(dish) :
                                         calculationPrice.Calculate(dish);

            return price;
        }
    }
}
