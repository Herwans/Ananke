using System.ComponentModel.DataAnnotations;

namespace Ananke.Domain.Entity.Media.Images.Galleries
{
    public record GalleryChapter : BaseEntity
    {
        [Required]
        public Gallery Gallery { get; set; }
        public string? Name { get; set; }
        public ICollection<GalleryChapterItem> Items { get; set; }
    }
}