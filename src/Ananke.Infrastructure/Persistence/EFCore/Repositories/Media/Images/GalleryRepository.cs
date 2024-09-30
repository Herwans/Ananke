using Ananke.Domain.Entity.Media.Images.Galleries;
using Ananke.Infrastructure.Persistence.Interfaces.Repositories.Media.Images;
using Microsoft.EntityFrameworkCore;

namespace Ananke.Infrastructure.Persistence.EFCore.Repositories.Media.Images
{
    public class GalleryRepository(AnankeContext context) : BaseRepository(context), IGalleryRepository
    {
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