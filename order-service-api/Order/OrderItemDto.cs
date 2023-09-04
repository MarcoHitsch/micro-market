namespace order_service_api.Order;

public class OrderItemDto
{
    public Guid ProductId { get; set; }
    public int Amount { get; set; }
}