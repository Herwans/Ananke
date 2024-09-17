using Ananke.Infrastructure.Repository;
using MediatR;

namespace Ananke.Application.Features.Items.Commands
{
    public record DeleteItemByIdCommand(int Id) : IRequest { }

    public class DeleteItemByIdCommandHandler : IRequestHandler<DeleteItemByIdCommand>
    {
        private readonly IItemRepository _itemRepository;

        public DeleteItemByIdCommandHandler(IItemRepository itemRepository)
        {
            _itemRepository = itemRepository;
        }

        public async Task Handle(DeleteItemByIdCommand request, CancellationToken cancellationToken)
        {
            await _itemRepository.RemoveByIdAsync(request.Id, cancellationToken);
        }
    }
}