using Ananke.Application.DTO;
using Ananke.Application.Mappers;
using Ananke.Domain.Entity;
using Ananke.Infrastructure.Repository;
using MediatR;

namespace Ananke.Application.Features.Items.Queries
{
    public record GetItemByIdQuery(int Id) : IRequest<ItemDTO?> { }

    public class GetItemByIdQueryHandler : IRequestHandler<GetItemByIdQuery, ItemDTO?>
    {
        private readonly IItemRepository _itemRepository;

        public GetItemByIdQueryHandler(IItemRepository itemRepository)
        {
            _itemRepository = itemRepository;
        }

        public async Task<ItemDTO?> Handle(GetItemByIdQuery request, CancellationToken cancellationToken = default)
        {
            Item? item = await _itemRepository.GetByIdAsync(request.Id, cancellationToken);
            return item == null ? null : ItemMapper.ToDTO(item);
        }
    }
}