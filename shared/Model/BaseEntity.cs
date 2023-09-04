using System.ComponentModel.DataAnnotations;

namespace shared.Model
{
    public class BaseEntity
    {
        [Key]
        public virtual Guid Id { get; set; }
    }
}