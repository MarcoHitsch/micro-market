using Microsoft.AspNetCore.Mvc;
using RestServices.Catalog;
using RestServices.Order;

namespace RestGateway.Controllers;

[ApiController]
[Route("[controller]")]
public class OrderController : ControllerBase
{
    private readonly OrderService service;

    public OrderController(OrderService service)
    {
        this.service = service;
    }

    [HttpGet]
    public async Task<ICollection<Order>> GetOrders()
    {
        return await this.service.GetOrdersAsync();
    }

    [HttpPost("{userId}")]
    public async Task<ActionResult> NewOrder(Guid userId, OrderPostDto dto)
    {
        await this.service.NewOrderAsync(userId, dto);
        return this.Ok();
    }


}