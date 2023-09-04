using basket_service.Model;
using Grpc.Net.Client;
using Microsoft.Extensions.Options;
using ProductGrpcService;

namespace basket_service.Services
{
    public class CatalogService
    {
        private GrpcChannel channel;
        private ProductService.ProductServiceClient client;

        public CatalogService(string connectionString)
        {
            this.channel = GrpcChannel.ForAddress(connectionString);
            this.client = new ProductService.ProductServiceClient(channel);
        }

        public async Task<ProductInfo> GetProductInfo(Guid productId)
        {
            var request = new GetProductRequest()
            {
                Id = productId.ToString()
            };

            var response = await client.GetProdcutAsync(request);
            var productInfo = new ProductInfo
            {
                Id = Guid.Parse(response.Product.Id),
                Name = response.Product.Name,
                UnitPrice = (decimal)response.Product.Price,
            };

            return productInfo;

        }
    }
}
