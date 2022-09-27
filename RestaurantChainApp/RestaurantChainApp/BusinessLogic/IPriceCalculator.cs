using RestaurantChainApp.Dto;
using RestaurantChainApp.Entities;

namespace RestaurantChainApp.BusinessLogic
{
    public interface IPriceCalculator
    {
        double CalculateForMeal(Meal meal);
        double CalculateForOrderItem(Dish dish, int amount);
    }
}
