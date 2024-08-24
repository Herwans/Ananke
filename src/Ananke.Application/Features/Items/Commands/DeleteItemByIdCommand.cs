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

        public Task Handle(DeleteItemByIdCommand request, CancellationToken cancellationToken)
        {
            _itemRepository.RemoveById(request.Id);
            return Task.CompletedTask;
        }
    }
}