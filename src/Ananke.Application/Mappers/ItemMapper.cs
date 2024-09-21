using Ananke.Application.DTO;
using Ananke.Domain.Entity;

namespace Ananke.Application.Mappers
{
    public static class ItemMapper
    {
        public static ItemDTO ToDTO(Item item)
        {
            return new()
            {
                Path = item.Path,
                Name = item.Name,
                Directory = item.Folder.Name,
                Extension = item.Extension.Name,
                AddedAt = new()
            };
        }

        public static Item ToEntity(ItemDTO item)
        {
            return ToEntity(item.Path);
        }

        public static Item ToEntity(string path)
        {
            Folder folder = new() { Path = Path.GetDirectoryName(path), Name = Path.GetFileName(Path.GetDirectoryName(path)) };
            Extension extension = new() { Name = Path.GetExtension(path).Trim('.').ToLower() };
            Item item = new() { Extension = extension, Folder = folder, Name = Path.GetFileNameWithoutExtension(path) };

            return item;
        }
    }
}