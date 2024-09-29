using Ananke.Domain.Entity.Items;
using System.ComponentModel.DataAnnotations;

namespace Ananke.Domain.Entity.Media
{
    public record Set : BaseEntity
    {
        [Required]
        public string? Name { get; set; }
        public ICollection<Item>? Items { get; set; }
    }
}