using basket_service.Data;
using basket_service.Model;
using basket_service.Services;
using basket_service_api.Basket;
using Microsoft.AspNetCore.Mvc;

namespace basket_service.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BasketController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly CatalogService catalogService;

        public BasketController(ApplicationDbContext context, CatalogService catalogService)
        {
            this.context = context;
            this.catalogService = catalogService;
        }

        [HttpGet("{userId}", Name = "GetBasket")]
        public async Task<ActionResult<Basket>> Get(Guid userId)
        {
            var basket = await context.GetBasket(userId);

            if (basket == null)
                return this.NotFound();

            return basket;
        }

        [HttpPost("{userId}", Name = "AddToBasket")]
        public async Task<Basket> AddItem(Guid userId, AddItemDTO dto)
        {
            var basket = await context.GetBasket(userId);

            if (basket == null)
                basket = new Basket() { UserId = userId };

            var productInfo = await catalogService.GetProductInfo(dto.ProductId);

            var item = new BasketItem
            {
                ProductId = dto.ProductId,
                Name = productInfo.Name,
                UnitPrice = productInfo.UnitPrice,
                Amount = dto.Amount
            };

            basket.Items.Add(item);
            await context.SetBasket(userId, basket);

            return basket;
        }
    }
}