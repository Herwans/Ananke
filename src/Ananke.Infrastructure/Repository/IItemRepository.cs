using Ananke.Domain.Entity;

namespace Ananke.Infrastructure.Repository
{
    public interface IItemRepository
    {
        IEnumerable<Item> GetItems();

        void Add(Item item);

        void AddAll(IEnumerable<Item> items);

        void RemoveById(int id);

        Item? GetItemById(int id);
    }
}