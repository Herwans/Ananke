using Ananke.Application.DTO;
using Ananke.Application.Mappers;
using Ananke.Domain.Entity.Items;
using Ananke.Infrastructure.Repository;
using MediatR;
using System.Drawing;

namespace Ananke.Application.Features.Items.Queries
{
    // TODO: Implement the version with ExtensionType
    public record GetItemsByTypeQuery(string Type, int Page = 1, int Size = 10) : IRequest<List<ItemDTO>> { }

    public class GetItemsByTypeQueryHandler : IRequestHandler<GetItemsByTypeQuery, List<ItemDTO>>
    {
        private readonly IItemRepository _itemRepository;

        private Dictionary<string, List<string>> extensionsDictionary = new Dictionary<string, List<string>>()
{
    { "images", new List<string>()
        {
            "jpg", "jpeg", "png", "gif", "bmp", "tiff", "tif",
            "webp", "svg", "ico", "heic", "heif", "psd", "raw",
            "cr2", "nef", "orf", "arw", "eps", "ai", "indd", "pdf"
        }
    },
    { "videos", new List<string>()
        {
            "mp4", "avi", "mkv", "mov", "flv", "wmv", "m4v", "webm",
            "3gp", "mpg", "mpeg", "ogv", "vob", "rm", "rmvb", "mxf", "f4v"
        }
    }
};

        public GetItemsByTypeQueryHandler(IItemRepository itemRepository)
        {
            _itemRepository = itemRepository;
        }

        public async Task<List<ItemDTO>> Handle(GetItemsByTypeQuery request, CancellationToken cancellationToken = default)
        {
            IEnumerable<Item> items = await _itemRepository.GetByExtensionsAsync([.. extensionsDictionary[request.Type]], request.Page, request.Size, cancellationToken);
            return items.Select(item => ItemMapper.ToDTO(item)).ToList();
        }
    }
}