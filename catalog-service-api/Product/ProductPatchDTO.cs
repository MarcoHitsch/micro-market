using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace catalog_service_api.Product
{
    public class ProductPatchDTO
    {
        public Guid Id { get; set; }
        public decimal Price { get; set; }
    }
}
