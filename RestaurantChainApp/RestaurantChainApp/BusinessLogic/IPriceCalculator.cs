using RestaurantChainApp.Dtoes;
using RestaurantChainApp.Entities;
using System.Collections.Generic;

namespace RestaurantChainApp.BusinessLogic
{
    public interface IPriceCalculator
    {
        List<Meal> CalculateForMeals(List<Meal> meals);
        double CalculateForOrderItem(Dish dish, int amount);
    }
}
