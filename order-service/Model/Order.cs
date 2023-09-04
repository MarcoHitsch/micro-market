using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using shared.Model;

namespace order_service.Model
{
    public class Order
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        public DateTime OrderDate { get; set; }
        public bool IsPayed { get; set; }
        public bool IsShipped { get; set; }
        public IList<OrderItem> Items { get; set; }
        public decimal DiscountPercent { get; set; }
        public decimal TotalGross { get; set; }
        public decimal TotalNet { get; set; }

        public Order()
        {
            Items = new List<OrderItem>();
        }

        public void UpdatePriceForProduct(Guid productId, decimal newPrice)
        {
            foreach(var item in Items.Where(i => i.ProductId == productId))
            {
                item.Price = newPrice;
                item.UpdateTotal();
            }

            UpdateTotal();
        }

        public void UpdateTotal()
        {
            this.TotalGross = this.Items.Sum(i => i.TotalGross);
            this.TotalNet = this.Items.Sum(i => i.TotalNet) * ((100 - DiscountPercent) / 100);
        }
    }
}
