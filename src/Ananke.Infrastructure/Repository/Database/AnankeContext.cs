using Ananke.Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace Ananke.Infrastructure.Repository.Database
{
    public class AnankeContext(DbContextOptions<AnankeContext> options) : DbContext(options)
    {
        public DbSet<Extension> Extensions { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<Folder> Folders { get; set; }
        public DbSet<ExtensionType> ExtensionTypes { get; set; }
    }
}