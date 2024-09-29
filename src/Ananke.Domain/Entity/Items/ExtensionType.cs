namespace Ananke.Domain.Entity.Items
{
    public record ExtensionType : BaseEntity
    {
        public string? Type { get; set; }
        public ICollection<Extension>? Extensions { get; set; }
    }
}