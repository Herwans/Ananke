using Ananke.Application.Mappers;
using Ananke.Application.Services;
using Ananke.Domain.Entity.Items;
using Ananke.Infrastructure.Repository;
using MediatR;

namespace Ananke.Application.Features.Items.Commands
{
    public record AddDirectoryCommand(string Path, bool Recursive = false) : IRequest { }

    public class AddDirectoryCommandHandler : IRequestHandler<AddDirectoryCommand>
    {
        private readonly IItemRepository _itemRepository;
        private readonly IFileSystemService _fileSystemService;

        public AddDirectoryCommandHandler(IItemRepository itemRepository, IFileSystemService fileSystemService)
        {
            _itemRepository = itemRepository;
            _fileSystemService = fileSystemService;
        }

        public async Task Handle(AddDirectoryCommand request, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            await ProcessDirectoryAsync(request.Path, request.Recursive, cancellationToken);
        }

        public async Task ProcessDirectoryAsync(
            string parentDirectory,
            bool recursive,
            CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            List<Item> items = [];
            IEnumerable<Item> current = await _itemRepository.GetAllAsync(cancellationToken);
            foreach (string file in _fileSystemService.GetFiles(parentDirectory))
            {
                cancellationToken.ThrowIfCancellationRequested();

                if (current.Any(x => x.Path == file)) { continue; }

                Item item = ItemMapper.ToEntity(file);
                items.Add(item);
            }

            await _itemRepository.AddAllAsync(items, cancellationToken);

            if (recursive)
            {
                foreach (string directory in _fileSystemService.GetDirectories(parentDirectory))
                {
                    cancellationToken.ThrowIfCancellationRequested();
                    await ProcessDirectoryAsync(directory, recursive, cancellationToken);
                }
            }
        }
    }
}