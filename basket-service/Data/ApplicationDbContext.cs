using basket_service.Model;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace basket_service.Data
{
    public class ApplicationDbContext
    {
        private readonly IDistributedCache cache;

        public ApplicationDbContext(IDistributedCache cache)
        {
            this.cache = cache;
        }

        public async Task<Basket?> GetBasket(Guid userId)
        {
            try
            {
                var json = await cache.GetStringAsync($"{userId}");
                var basket = JsonConvert.DeserializeObject<Basket>(json);
                return basket;
            }
            catch
            {
                return null;
            }
        }

        public async Task SetBasket(Guid userId, Basket basket)
        {
            var json = JsonConvert.SerializeObject(basket);
            await cache.SetStringAsync($"{userId}", json);
        }
    }
}
