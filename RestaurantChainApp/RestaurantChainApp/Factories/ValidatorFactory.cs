using RestaurantChainApp.Validators;

namespace RestaurantChainApp.Factories
{
    public class ValidatorFactory : IValidatorFactory
    {
        public CreateOrderValidator CreateOrderValidator() 
        {
            return new CreateOrderValidator();
        }
        public UpdateOrderValidator UpdateOrderValidator() 
        {
            return new UpdateOrderValidator();
        }
    }
}
