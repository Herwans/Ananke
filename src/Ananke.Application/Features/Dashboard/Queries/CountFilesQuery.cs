using Ananke.Application.DTO;
using Ananke.Infrastructure.Repository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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