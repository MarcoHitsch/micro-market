using Microsoft.Extensions.Options;
using MongoDB.Driver;
using order_service.Model;

namespace order_service.Data
{
    public class ApplicationDbContext
    {
        public IMongoCollection<Order> Orders { get; set; }

        public ApplicationDbContext(IOptions<ConnectionOptions> options)
        {
            var mongoClient = new MongoClient(
            options.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                options.Value.DatabaseName);

            this.Orders = mongoDatabase.GetCollection<Order>("Orders");
        }
    }
}
