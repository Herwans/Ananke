using Ananke.Application.Mappers;
using Ananke.Application.Services;
using Ananke.Domain.Entity;
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

        public Task Handle(AddDirectoryCommand request, CancellationToken cancellationToken)
        {
            ProcessDirectory(request.Path, request.Recursive);
            return Task.CompletedTask;
        }

        public void ProcessDirectory(string parentDirectory, bool recursive)
        {
            List<Item> items = [];
            IEnumerable<Item> current = _itemRepository.GetAll();
            foreach (string file in _fileSystemService.GetFiles(parentDirectory))
            {
                if (current.Any(x => x.Path == file)) { continue; }

                Item item = ItemMapper.ToEntity(file);
                items.Add(item);
            }

            _itemRepository.AddAll(items);

            if (recursive)
            {
                foreach (string directory in _fileSystemService.GetDirectories(parentDirectory))
                    ProcessDirectory(directory, recursive);
            }
        }
    }
}