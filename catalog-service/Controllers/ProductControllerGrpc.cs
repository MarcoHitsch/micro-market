using catalog_service.Data;
using Grpc.Core;
using Microsoft.EntityFrameworkCore;
using ProductGrpcService;

namespace catalog_service.Controllers
{
    public class ProductControllerGrpc : ProductService.ProductServiceBase
    {
        private readonly ApplicationDbContext dbContext;

        public ProductControllerGrpc(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public override async Task<GetProductResponse> GetProdcut(GetProductRequest request, ServerCallContext context)
        {
            var product = await dbContext.Products.SingleAsync(p => p.Id == Guid.Parse(request.Id));
            
            var response = new GetProductResponse()
            {
                Product = new Product
                {
                    Id = product.Id.ToString(),
                    Name = product.Name,
                    Price = (float)product.Price,
                    Stock = product.Stock,
                }
            };

            return response;
        }
    }
}
