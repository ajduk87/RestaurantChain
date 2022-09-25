using RestaurantChainApp.Repositories;

namespace RestaurantChainApp.Factories
{
    public class RepositoryFactory : IRepositoryFactory
    {
        public MealsDishesRepository CreateMealsDishesRepository() 
        {
            return new MealsDishesRepository();
        }
    }
}
