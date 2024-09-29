using Ananke.Domain.Entity.Items;

namespace Ananke.Infrastructure.Persistence.Interfaces.Repositories
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

        Task<IEnumerable<Item>> GetByExtensionsAsync(string[] extensions, int page = 1, int size = 10, CancellationToken cancellationToken = default);

        Task<List<Item>> GetByFolderIdAsync(int folderId, CancellationToken cancellationToken);
    }
}