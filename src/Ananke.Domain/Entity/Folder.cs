namespace Ananke.Domain.Entity
{
    public record Folder : BaseEntity
    {
        public string Name { get; set; }
        public int? ParentId { get; set; }
        public Folder Parent { get; set; }
        public ICollection<Folder> Folders { get; set; }
        public ICollection<Item> Items { get; set; }
        public string? Path { get; set; }
    }
}