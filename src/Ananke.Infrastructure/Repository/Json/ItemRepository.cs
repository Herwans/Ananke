using Ananke.Domain.Entity;
using Newtonsoft.Json;

namespace Ananke.Infrastructure.Repository.Json
{
    public class ItemRepository : IItemRepository
    {
        private readonly string _file;
        private readonly List<Item> _items;
        private readonly IExtensionRepository _extensionRepository;

        public ItemRepository(string file, IExtensionRepository extensionRepository)
        {
            _file = file;
            if (!File.Exists(file))
            {
                _items = [];
            }
            else
            {
                _items = JsonConvert.DeserializeObject<List<Item>>(File.ReadAllText(_file));
            }
            _extensionRepository = extensionRepository;
        }

        private void Save()
        {
            File.WriteAllText(_file, JsonConvert.SerializeObject(_items));
        }

        public void Add(Item item)
        {
            int lastId = _items.Max(item => item.Id) ?? 0;
            string extension = Path.GetExtension(item.Path).Trim('.');
            item.Id = ++lastId;
            if (extension != "")
            {
                if (_extensionRepository.GetByName(extension) == null)
                    _extensionRepository.Add(extension);
                item.Extension = _extensionRepository.GetByName(extension).Id;
            }
            _items.Add(item);
            Save();
        }

        public void AddAll(IEnumerable<Item> items)
        {
            int lastId = items.Max(item => item.Id) ?? 0;
            items.Select(item =>
            {
                item.Id = ++lastId;
                string extension = Path.GetExtension(item.Path).Trim('.');
                if (extension != "")
                {
                    if (_extensionRepository.GetByName(extension) == null)
                        _extensionRepository.Add(extension);
                    item.Extension = _extensionRepository.GetByName(extension).Id;
                }
                return item;
            }).ToList();
            this._items.AddRange(items);
            Save();
        }

        public IEnumerable<Item> GetAll()
        {
            return _items;
        }

        public void RemoveById(int id)
        {
            _items.Remove(_items.Find(item => item.Id == id));
        }

        public Item? GetById(int id)
        {
            return _items.Find(item => item.Id == id);
        }
    }
}