namespace basket_service.Model
{
    public class Basket
    {
        public Guid UserId { get; set; }
        public IList<BasketItem> Items { get; set; } = new List<BasketItem>();
    }
}
