using shared.Model;

namespace catalog_service.Model
{
    public class Category : BaseEntity
    {
        public string Name { get; set; }
        public decimal VatRate { get; set; }
    }
}
