using Ananke.Application.DTO;
using Ananke.Domain.Entity;
using Ananke.Infrastructure.Repository;
using Ananke.Application.Mappers;

namespace Ananke.Application.Services
{
    public interface IItemService
    {
        void AddDirectory(string path, bool recursive = false);

        void AddItem(string path);

        ItemDTO? GetItem(int id);

        IEnumerable<ItemDTO> GetItems();

        void RemoveItem(int id);
    }

    public class ItemService : IItemService
    {
        private readonly IItemRepository _itemRepository;
        private readonly IFileSystemService _fileSystemService;

        public ItemService(IItemRepository itemRepository, IFileSystemService fileSystemService)
        {
            _itemRepository = itemRepository;
            _fileSystemService = fileSystemService;
        }

        public void AddDirectory(string directoryPath, bool recursive = false)
        {
            List<Item> items = [];
            IEnumerable<Item> current = _itemRepository.GetItems();
            foreach (string file in _fileSystemService.GetFiles(directoryPath))
            {
                if (current.Any(x => x.Path == file)) { continue; }

                Item item = new()
                {
                    Path = file
                };
                items.Add(item);
            }

            _itemRepository.AddAll(items);

            if (recursive)
            {
                foreach (string directory in _fileSystemService.GetDirectories(directoryPath))
                    AddDirectory(directory, recursive);
            }
        }

        public void AddItem(string path)
        {
            if (_itemRepository.GetItems().Any(x => x.Path == path))
            {
                return;
            }
            Item item = new()
            {
                Path = path
            };
            _itemRepository.Add(item);
        }

        public ItemDTO? GetItem(int id)
        {
            Item? item = _itemRepository.GetItemById(id);
            return item == null ? null : ItemMapper.ToDTO(item);
        }

        public IEnumerable<ItemDTO> GetItems()
        {
            IEnumerable<ItemDTO> items = [];
            return _itemRepository.GetItems().Select(ItemMapper.ToDTO);
        }

        public void RemoveItem(int id)
        {
            _itemRepository.RemoveById(id);
        }
    }
}