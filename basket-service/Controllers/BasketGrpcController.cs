using basket_service.Data;
using BasketGrpcService;
using Grpc.Core;

namespace basket_service.Controllers
{
    public class BasketGrpcController : BasketService.BasketServiceBase
    {
        private readonly ApplicationDbContext cacheContext;

        public BasketGrpcController(ApplicationDbContext cacheContext)
        {
            this.cacheContext = cacheContext;
        }

        public override async Task<GetBasketResponse> GetBasket(GetBasketRequest request, ServerCallContext context)
        {
            var basket = await cacheContext.GetBasket(Guid.Parse(request.UserId));
            var response = new GetBasketResponse();

            foreach(var item in basket.Items)
            {
                response.Items.Add(new BasketItem
                {
                    Id = item.ProductId.ToString(),
                    Amount = item.Amount,
                });
            }

            return response;
        }
    }
}
