using System.ComponentModel.DataAnnotations;

namespace Ananke.Domain.Entity.Items
{
    public record Extension : BaseEntity
    {
        [Required]
        public string? Name { get; set; }
        public ICollection<Item>? Items { get; set; }
        public ExtensionType? Type { get; set; }
        public int? TypeId { get; set; }
    }
}