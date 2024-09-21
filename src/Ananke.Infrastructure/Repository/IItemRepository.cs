using Ananke.Domain.Entity;

namespace Ananke.Infrastructure.Repository
{
    public interface IItemRepository
    {
        Task<IEnumerable<Item>> GetAllAsync(CancellationToken cancellationToken = default);

        Task<IEnumerable<Item>> GetAllLastAsync(int limit = 10, CancellationToken cancellationToken = default);

        Task AddAsync(Item item, CancellationToken cancellationToken = default);

        Task AddAllAsync(IEnumerable<Item> items, CancellationToken cancellationToken = default);

        Task RemoveByIdAsync(int id, CancellationToken cancellationToken = default);

        Task<Item?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
        Task<int> CountAsync(CancellationToken cancellationToken);
    }
}