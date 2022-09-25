using RestaurantChainApp.Dto;
using RestaurantChainApp.Entities;

namespace RestaurantChainApp.Services
{
    public interface IRestaurantChainService
    {
        Menu GetMenu();
        void AddOrderItemToOrder(int orderid, OrderItem orderItem);
        Order GetOrder(int orderid);
        void CheckCurrentOrderState(int orderid);
        void CheckoutOrder(int orderid);
    }
}
