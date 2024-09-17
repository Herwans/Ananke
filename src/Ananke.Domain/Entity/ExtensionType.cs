namespace Ananke.Domain.Entity
{
    public record ExtensionType : BaseEntity
    {
        public string? Type { get; set; }
        public ICollection<Extension>? Extensions { get; set; }
    }
}