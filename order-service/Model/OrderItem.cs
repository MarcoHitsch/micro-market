using shared.Model;

namespace order_service.Model
{
    public class OrderItem
    {
        public Guid ProductId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Amount { get; set; }
        public decimal DiscountPercent { get; set; }
        public decimal TotalGross { get; set; }
        public decimal TotalNet { get; set; }

        public void UpdateTotal()
        {
            this.TotalGross = Price * Amount;
            this.TotalNet = Price * Amount * ((100 - DiscountPercent) / 100);
        }
    }
}
