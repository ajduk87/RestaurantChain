using RestaurantChainApp.BusinessLogic;
using RestaurantChainApp.Dto;
using RestaurantChainApp.Entities;

namespace RestaurantChainApp.Services
{
    public class RestaurantChainService : IRestaurantChainService
    {

        private IPriceCalculator priceCalculator { get; set; }

        public RestaurantChainService(IPriceCalculator priceCalculator)
        {
            this.priceCalculator = priceCalculator;
        }

        public Menu GetMenu() 
        {
            return new Menu(new System.Collections.Generic.List<MenuItem>());
        }
        public Order GetOrder(int orderid)
        {
            return new Order();
        }

        public void AddOrderItemToOrder(int orderid, OrderItem orderItem) 
        {
        }
        
        public void CheckCurrentOrderState(int orderid) 
        {
        }
        public void CheckoutOrder(int orderid) 
        {
        }
    }
}
