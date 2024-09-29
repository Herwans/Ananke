namespace Ananke.Domain.Entity.Media.Images.Galleries
{
    public record Gallery : BaseEntity
    {
        public string? Name { get; set; }
        public ICollection<GalleryChapter> Chapters { get; set; }
    }
}