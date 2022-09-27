using RestaurantChainApp.BusinessLogic.CalculationPriceStrategies;
using RestaurantChainApp.Dtoes;
using RestaurantChainApp.Entities;
using RestaurantChainApp.Factories;
using System;
using System.Collections.Generic;

namespace RestaurantChainApp.BusinessLogic
{
    public class PriceCalculator : IPriceCalculator
    {

        private readonly EnvironmentSettings envSettings;
        private readonly CalculationPriceStrategy dishPriceStrategy;
        private readonly CalculationPriceStrategy mealPriceStrategy;

        public PriceCalculator(IEnvironmentSettingsFactory environmentSettingsFactory)
        {
            envSettings = environmentSettingsFactory.GetEnvironmentSettings();

            dishPriceStrategy = new DishPriceStrategy();
            mealPriceStrategy = new MealPriceStrategy();
        }

        private bool IsHappyHour() 
        {
            int currentHour = DateTime.Now.Hour;
            return currentHour >= envSettings.HappyHourBegin && currentHour <= envSettings.HappyHourEnd;
        }

        public double CalculateForMeal(Meal meal) 
        {
            return mealPriceStrategy.Calculate(meal);
        }


        public double CalculateForOrderItem(Dish dish, int amount) 
        {
            CalculationPriceStrategy calculationPrice = dish.IsMeal ? mealPriceStrategy : 
                                             dishPriceStrategy;

            double price = IsHappyHour() ? Math.Round(0.8 * calculationPrice.Calculate(dish), 2) :
                                           Math.Round(calculationPrice.Calculate(dish), 2);

            return price;
        }
    }
}
