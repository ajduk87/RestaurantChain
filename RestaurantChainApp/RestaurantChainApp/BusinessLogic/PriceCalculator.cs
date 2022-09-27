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

        private double CalculateForMeal(Meal meal) 
        {
            return IsHappyHour() ? Math.Round(0.8 * mealPriceStrategy.Calculate(meal), 2) :
                                   Math.Round(mealPriceStrategy.Calculate(meal), 2);
        }

        private double CalculateForDish(Dish dish)
        {
            return IsHappyHour() ? Math.Round(0.8 * dishPriceStrategy.Calculate(dish) , 2):
                                   Math.Round(dishPriceStrategy.Calculate(dish) , 2);
        }

        public List<Meal> CalculateForMeals(List<Meal> meals) 
        {
            List<Meal> newMeals = new List<Meal>();
            foreach (var meal in meals) 
            {
                meal.Price = CalculateForMeal(meal);
                newMeals.Add(meal);
            }

            return newMeals;
        }

        public List<Dish> CalculateForDishes(List<Dish> dishes) 
        {
            List<Dish> newDishes = new List<Dish>();
            foreach (var dish in dishes)
            {
                dish.Price = CalculateForDish(dish);
                newDishes.Add(dish);
            }

            return newDishes;
        }
    }
}
