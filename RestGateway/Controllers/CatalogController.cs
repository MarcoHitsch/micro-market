using Microsoft.AspNetCore.Mvc;
using RestServices.Catalog;

namespace RestGateway.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CatalogController : ControllerBase
    {
        private readonly CatalogService service;
        private readonly ILogger<CatalogController> logger;

        public CatalogController(CatalogService service, ILogger<CatalogController> logger)
        {
            this.service = service;
            this.logger = logger;
        }

        [HttpGet]
        public async Task<ICollection<Product>> GetProducts()
        {
            logger.LogInformation("Test123");
            return await this.service.GetProductsAsync();
        }

        [HttpPatch("{productId}")]
        public async Task<ActionResult> UpdatePrice(Guid productId, ProductPatchDTO dto)
        {
            await this.service.UpdateProductAsync(productId, dto);
            return this.Ok();
        }
    }
}