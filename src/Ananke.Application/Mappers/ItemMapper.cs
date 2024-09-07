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
                Path = item.Path
            };
        }

        public static Item ToEntity(ItemDTO item)
        {
            return new()
            {
                Path = item.Path,
            };
        }

        public static Item ToEntity(string path)
        {
            Item item = new()
            {
                Path = path,
                Directory = Path.GetDirectoryName(path),
                Name = Path.GetFileNameWithoutExtension(path),
            };

            return item;
        }

        private static Extension? GetExtension(string path)
        {
            string? extension = Path.GetExtension(path);
            if (extension == null || extension == string.Empty) { return null; }
            extension = extension.Trim('.');
            return extension == null ? null : new() { Name = extension };
        }
    }
}