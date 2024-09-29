using Ananke.Domain.Entity.Media.Images.Galleries;

namespace Ananke.Infrastructure.Repository.Media.Images
{
    public interface IGalleryRepository
    {
        Task CreateAsync(Gallery gallery, CancellationToken cancellationToken = default);

        Task<Gallery> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    }
}