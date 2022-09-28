using RestaurantChainApp.Validators;

namespace RestaurantChainApp.Factories
{
    public interface IValidatorFactory
    {
        CreateOrderValidator CreateOrderValidator();
        UpdateOrderValidator UpdateOrderValidator();
    }
}
