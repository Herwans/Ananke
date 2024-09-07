namespace Ananke.Domain.Entity
{
    public record Item : BaseEntity
    {
        public string? Path { get; set; }
        public string? Directory { get; set; }
        public string? Name { get; set; }
        public int? Extension { get; set; }
    }
}