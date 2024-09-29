using Ananke.Domain.Entity.Media.Images.Galleries;
using Ananke.Infrastructure.Repository.Media.Images;
using Microsoft.EntityFrameworkCore;

namespace Ananke.Infrastructure.Repository.EFCore.Media.Images
{
    public class GalleryRepository(AnankeContext context) : IGalleryRepository
    {
        private readonly AnankeContext _context = context;

        public async Task CreateAsync(Gallery gallery, CancellationToken cancellationToken = default)
        {
            await _context.Galleries.AddAsync(gallery, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<Gallery> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            return await _context.Set<Gallery>().Where(g => g.Id == id)
                .Include(gallery => gallery.Chapters)
                .ThenInclude(chapter => chapter.Items)
                .ThenInclude(item => item.Item)
                .ThenInclude(item => item.Folder)
                .IgnoreAutoIncludes()
                .FirstAsync(cancellationToken);
        }
    }
}