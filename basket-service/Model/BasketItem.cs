namespace basket_service.Model
{
    public class BasketItem
    {
        public Guid ProductId { get; set; }
        public string Name { get; set; }
        public int Amount { get; set; }
        public decimal UnitPrice { get; set; }
    }
}
