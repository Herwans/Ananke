using Ananke.Application.Mappers;
using Ananke.Domain.Entity.Items;
using Ananke.Infrastructure.Repository;
using MediatR;

namespace Ananke.Application.Features.Items.Commands
{
    public record AddItemCommand(string Path) : IRequest { }

    public class AddItemCommandHandler : IRequestHandler<AddItemCommand>
    {
        private readonly IItemRepository _itemRepository;

        public AddItemCommandHandler(IItemRepository itemRepository)
        {
            _itemRepository = itemRepository;
        }

        public async Task Handle(AddItemCommand request, CancellationToken cancellationToken)
        {
            IEnumerable<Item> items = await _itemRepository.GetAllAsync(cancellationToken);
            if (!items.Any(x => x.Path == request.Path))
            {
                Item item = ItemMapper.ToEntity(request.Path);
                await _itemRepository.AddAsync(item, cancellationToken);
            }
        }
    }
}