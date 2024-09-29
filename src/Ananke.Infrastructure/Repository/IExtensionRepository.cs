using Ananke.Domain.Entity.Items;

namespace Ananke.Infrastructure.Repository
{
    public interface IExtensionRepository
    {
        Task AddAsync(string extension, CancellationToken cancellationToken = default);

        Task<Extension?> GetByNameAsync(string name, CancellationToken cancellationToken = default);
    }
}