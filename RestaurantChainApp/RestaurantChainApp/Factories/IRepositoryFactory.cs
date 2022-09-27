using RestaurantChainApp.Repositories;

namespace RestaurantChainApp.Factories
{
    public interface IRepositoryFactory
    {
        MealsDishesRepository CreateMealsDishesRepository();
        MenuItemsRepository CreateMenuItemsRepository();
        OrderItemsRepository CreateOrderItemsRepository();
        OrdersRepository CreateOrdersRepository();
    }
}
