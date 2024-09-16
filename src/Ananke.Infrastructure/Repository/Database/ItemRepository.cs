using Ananke.Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace Ananke.Infrastructure.Repository.Database
{
    public class ItemRepository : IItemRepository
    {
        private readonly AnankeContext _context;

        public ItemRepository(AnankeContext context)
        {
            _context = context;
        }

        public void Add(Item item)
        {
            Extension? extension = _context.Extensions.FirstOrDefault(ext => ext.Name == item.Extension.Name);
            if (extension != null)
            {
                item.Extension = extension;
            }

            Folder? folder = _context.Folders.FirstOrDefault(folder => folder.Path == item.Folder.Path);
            if (folder != null)
            {
                item.Folder = folder;
            }

            _context.Items.Add(item);
            _context.SaveChanges();
        }

        public void AddAll(IEnumerable<Item> items)
        {
            var extensions = _context.Extensions.ToDictionary(e => e.Name);
            var folders = _context.Folders.ToDictionary(f => f.Path);

            foreach (var item in items)
            {
                if (extensions.TryGetValue(item.Extension.Name, out var extension))
                {
                    item.Extension = extension;
                }
                else
                {
                    _context.Extensions.Add(item.Extension);
                    extensions.Add(item.Extension.Name, item.Extension);
                }

                if (folders.TryGetValue(item.Folder.Path, out var folder))
                {
                    item.Folder = folder;
                }
                else
                {
                    _context.Folders.Add(item.Folder);
                    folders.Add(item.Folder.Path, item.Folder);
                }

                _context.Items.Add(item);
            }
            _context.SaveChanges();
        }

        public IEnumerable<Item> GetAll()
        {
            return _context.Set<Item>()
                .Include(item => item.Folder)
                .Include(item => item.Extension)
                .ToList();
        }

        public Item? GetById(int id)
        {
            return _context.Set<Item>()
                .Include(item => item.Folder)
                .Include(item => item.Extension)
                .First(item => item.Id == id);
        }

        public void RemoveById(int id)
        {
            _context.Items.Remove(GetById(id));
            _context.SaveChanges();
        }
    }
}