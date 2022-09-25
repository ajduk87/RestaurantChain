using RestaurantChainApp.Dto;
using RestaurantChainApp.Entities;

namespace RestaurantChainApp.BusinessLogic
{
    public interface IPriceCalculator
    {
        double Calculate(Dish dish, int amount);
    }
}
