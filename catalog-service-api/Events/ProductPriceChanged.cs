using shared.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace catalog_service_api.Events
{
    public class ProductPriceChanged : PropertyChanged<decimal>
    {
    }
}
