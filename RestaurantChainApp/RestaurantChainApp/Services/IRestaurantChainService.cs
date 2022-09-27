using RestaurantChainApp.Dtoes;
using System.Collections.Generic;

namespace RestaurantChainApp.Services
{
    public interface IRestaurantChainService
    {
        Menu GetMenu();
        List<Dish> GetSingleDishes();
        List<Meal> GetMeals();
        
        OrderDto GetOrder(int orderid);
        void CreateOrder(OrderDto order);
        void ModifyOrder(OrderDto order);
        void RemoveOrder(int orderid);
    }
}
