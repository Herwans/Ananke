using Ananke.Application.DTO;
using Ananke.Application.Mappers;
using Ananke.Infrastructure.Repository;
using MediatR;

namespace Ananke.Application.Features.Dashboard.Queries
{
    public record GetLastFilesQuery() : IRequest<ItemDTO[]> { }

    public class GetLastFilesQueryHandler : IRequestHandler<GetLastFilesQuery, ItemDTO[]>
    {
        private readonly IItemRepository _itemRepository;

        public GetLastFilesQueryHandler(IItemRepository itemRepository)
        {
            _itemRepository = itemRepository;
        }

        public async Task<ItemDTO[]> Handle(GetLastFilesQuery request, CancellationToken cancellationToken = default)
        {
            return (await _itemRepository.GetAllLastAsync(10, cancellationToken)).Select(ItemMapper.ToDTO).ToArray();
        }
    }
}