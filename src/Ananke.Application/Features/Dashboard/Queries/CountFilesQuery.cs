using Ananke.Infrastructure.Persistence.Interfaces.Repositories;
using MediatR;

namespace Ananke.Application.Features.Dashboard.Queries
{
    public record CountFilesQuery() : IRequest<int> { }

    public class CountFilesQueryHandler : IRequestHandler<CountFilesQuery, int>
    {
        private readonly IItemRepository _itemRepository;

        public CountFilesQueryHandler(IItemRepository itemRepository)
        {
            _itemRepository = itemRepository;
        }

        public async Task<int> Handle(CountFilesQuery request, CancellationToken cancellationToken)
        {
            return (await _itemRepository.CountAsync(cancellationToken));
        }
    }
}