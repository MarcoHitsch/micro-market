using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace order_service_api.Order
{
    public class OrderPostDto
    {
        public List<OrderItemDto> Items { get; set; } = new List<OrderItemDto>();
    }
}
