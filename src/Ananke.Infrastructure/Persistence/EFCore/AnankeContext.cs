using Ananke.Domain.Entity.Items;
using Ananke.Domain.Entity.Media.Images.Galleries;
using Microsoft.EntityFrameworkCore;

namespace Ananke.Infrastructure.Persistence.EFCore
{
    public class AnankeContext(DbContextOptions<AnankeContext> options) : DbContext(options)
    {
        public DbSet<Extension> Extensions { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<Folder> Folders { get; set; }
        public DbSet<ExtensionType> ExtensionTypes { get; set; }
        public DbSet<Gallery> Galleries { get; set; }
        public DbSet<GalleryChapter> GalleryChapters { get; set; }
        public DbSet<GalleryChapterItem> GalleryChapterItems { get; set; }
    }
}