using Ananke.Application.DTO;
using Ananke.Application.Mappers;
using Ananke.Infrastructure.Repository;
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

        public Task<IEnumerable<ItemDTO>> Handle(GetItemsQuery request, CancellationToken cancellationToken)
        {
            return Task.FromResult(_itemRepository.GetItems().Select(ItemMapper.ToDTO));
        }
    }
}