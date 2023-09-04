using catalog_service_api.Events;
using MongoDB.Driver;
using order_service.Data;

namespace order_service.Events
{
    public class CatalogEventHandler
    {
        private readonly ApplicationDbContext context;

        public CatalogEventHandler(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task HandleProductChanged(ProductPriceChanged msg)
        {
            var orders = await context.Orders.Find(p => p.Items.Any(i => i.ProductId == msg.EntityId)).ToListAsync();

            foreach(var order in orders)
            {
                order.UpdatePriceForProduct(msg.EntityId, msg.NewValue);
                await context.Orders.ReplaceOneAsync(x => x.Id == order.Id, order);
            }
        }
    }
}
