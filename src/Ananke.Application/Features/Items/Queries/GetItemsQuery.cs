using Ananke.Application.DTO;
using Ananke.Application.Mappers;
using Ananke.Infrastructure.Persistence.Interfaces.Repositories;
using MediatR;

namespace Ananke.Application.Features.Items.Queries
{
    public record GetItemsQuery() : IRequest<IEnumerable<ItemDTO>> { }

    public class GetItemsQueryHandler : IRequestHandler<GetItemsQuery, IEnumerable<ItemDTO>>
    {
        private readonly IItemRepository _itemRepository;

        public GetItemsQueryHandler(IItemRepository itemRepository)
        {
            _itemRepository = itemRepository;
        }

        public async Task<IEnumerable<ItemDTO>> Handle(GetItemsQuery request, CancellationToken cancellationToken)
        {
            return (await _itemRepository.GetAllAsync(cancellationToken)).Select(ItemMapper.ToDTO);
        }
    }
}