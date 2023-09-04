namespace basket_service_api.Basket
{
    public class AddItemDTO
    {
        public Guid ProductId { get; set; }
        public int Amount { get; set; }
    }
}