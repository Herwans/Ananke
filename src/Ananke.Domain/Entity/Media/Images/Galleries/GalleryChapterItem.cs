using Ananke.Domain.Entity.Items;
using System.ComponentModel.DataAnnotations;

namespace Ananke.Domain.Entity.Media.Images.Galleries
{
    public record GalleryChapterItem : BaseEntity
    {
        [Required]
        public GalleryChapter Chapter { get; set; }
        [Required]
        public Item Item { get; set; }

        public int? Position { get; set; }
    }
}