using Ananke.Domain.Entity;

namespace Ananke.Infrastructure.Repository
{
    public interface IItemRepository
    {
        IEnumerable<Item> GetAll();

        void Add(Item item);

        void AddAll(IEnumerable<Item> items);

        void RemoveById(int id);

        Item? GetById(int id);
    }
}