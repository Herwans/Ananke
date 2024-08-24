using Ananke.Domain.Entity;
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

        public Task Handle(AddItemCommand request, CancellationToken cancellationToken)
        {
            if (!_itemRepository.GetItems().Any(x => x.Path == request.Path))
            {
                Item item = new()
                {
                    Path = request.Path
                };
                _itemRepository.Add(item);
            }

            return Task.CompletedTask;
        }
    }
}