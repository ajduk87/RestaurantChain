using RestaurantChainApp.Repositories;

namespace RestaurantChainApp.Factories
{
    public interface IRepositoryFactory
    {
        MealsDishesRepository CreateMealsDishesRepository();
    }
}
