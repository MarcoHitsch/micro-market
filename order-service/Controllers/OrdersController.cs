using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;
using order_service.Data;
using order_service.Model;
using order_service_api.Order;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace order_service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly ILogger<OrdersController> logger;
        private readonly ApplicationDbContext context;

        public OrdersController(ILogger<OrdersController> logger, ApplicationDbContext context)
        {
            this.logger = logger;
            this.context = context;
        }

        // GET: api/<OrdersController>
        [HttpGet(Name = "GetOrders")]
        public async Task<IEnumerable<Order>> Get()
        {
            return await context.Orders.Find(_ => true).ToListAsync();
        }

        // GET api/<OrdersController>/5
        [HttpGet("{id}", Name = "GetOrderById")]
        public async Task<Order> Get(string id)
        {
            return await context.Orders.Find(o => o.Id == id).SingleAsync();
        }

        // POST api/<OrdersController>
        [HttpPost(Name = "NewOrder")]
        public async Task Post(Guid userId, OrderPostDto dto)
        {
            var order = new Order()
            {
                Id = ObjectId.GenerateNewId().ToString(), //Guid.NewGuid().ToString("N").Substring(0, 24),
            };

            foreach (var item in dto.Items)
            {
                order.Items.Add(new OrderItem()
                {
                    ProductId = item.ProductId,
                    Amount = item.Amount
                });
            }
                
            await context.Orders.InsertOneAsync(order);
        }

        // PUT api/<OrdersController>/5
        [HttpPut("{id}", Name = "UpdateOrder")]
        public async Task Put(string id, Order order)
        {
            await context.Orders.ReplaceOneAsync(o => o.Id == id, order);
        }

        // DELETE api/<OrdersController>/5
        [HttpDelete("{id}", Name = "DeleteOrder")]
        public async Task Delete(string id)
        {
            await context.Orders.DeleteOneAsync(o => o.Id == id);
        }
    }
}
