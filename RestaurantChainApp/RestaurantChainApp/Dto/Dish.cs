namespace RestaurantChainApp.Dto
{
    public class Dish : Dto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public bool IsMeal { get; set; }
    }
}
