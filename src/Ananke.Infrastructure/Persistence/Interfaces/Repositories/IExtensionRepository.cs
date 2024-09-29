using Ananke.Domain.Entity.Items;

namespace Ananke.Infrastructure.Persistence.Interfaces.Repositories
{
    public interface IExtensionRepository
    {
        Task AddAsync(string extension, CancellationToken cancellationToken = default);

        Task<Extension?> GetByNameAsync(string name, CancellationToken cancellationToken = default);
    }
}