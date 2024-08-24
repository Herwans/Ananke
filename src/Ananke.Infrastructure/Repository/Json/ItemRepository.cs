using Ananke.Domain.Entity;
using Newtonsoft.Json;

namespace Ananke.Infrastructure.Repository.Json
{
    public class ItemRepository : IItemRepository
    {
        private const string FILE = @"H:\Item.json";
        private readonly List<Item> items;

        public ItemRepository()
        {
            if (!File.Exists(FILE))
            {
                items = [];
            }
            else
            {
                items = JsonConvert.DeserializeObject<List<Item>>(File.ReadAllText(FILE));
            }
        }

        private void Save()
        {
            File.WriteAllText(FILE, JsonConvert.SerializeObject(items));
        }

        public void Add(Item item)
        {
            int lastId = items.Max(item => item.Id) ?? 0;
            item.Id = ++lastId;
            items.Add(item);
            Save();
        }

        public void AddAll(IEnumerable<Item> items)
        {
            int lastId = items.Max(item => item.Id) ?? 0;
            items.Select(item => item.Id = ++lastId).ToList();
            this.items.AddRange(items);
            Save();
        }

        public IEnumerable<Item> GetItems()
        {
            return items;
        }

        public void RemoveById(int id)
        {
            items.Remove(items.Find(item => item.Id == id));
        }

        public Item? GetItemById(int id)
        {
            return items.Find(item => item.Id == id);
        }
    }
}