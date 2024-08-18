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
    }
}