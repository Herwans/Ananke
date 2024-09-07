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

        public Task<ItemDTO?> Handle(GetItemByIdQuery request, CancellationToken cancellationToken)
        {
            Item? item = _itemRepository.GetById(request.Id);
            return Task.FromResult(item == null ? null : ItemMapper.ToDTO(item));
        }
    }
}