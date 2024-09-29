using Ananke.Application.Mappers;
using Ananke.Application.Services;
using Ananke.Domain.Entity.Items;
using Ananke.Domain.Entity.Media.Images.Galleries;
using Ananke.Infrastructure.Repository;
using Ananke.Infrastructure.Repository.Media.Images;
using MediatR;

namespace Ananke.Application.Features.Media.Images.Commands
{
    public record CreateGalleryFromFolderCommand(int FolderId, bool Sorted = false) : IRequest { }

    public class CreateGalleryFromFolderCommandHandler : IRequestHandler<CreateGalleryFromFolderCommand>
    {
        private readonly IItemRepository _itemRepository;
        private readonly IGalleryRepository _galleryRepository;

        public CreateGalleryFromFolderCommandHandler(IItemRepository itemRepository, IGalleryRepository galleryRepository)
        {
            _itemRepository = itemRepository;
            _galleryRepository = galleryRepository;
        }

        public async Task Handle(CreateGalleryFromFolderCommand request, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            List<Item> items = await _itemRepository.GetByFolderIdAsync(request.FolderId, cancellationToken);
            if (items.Count > 0)
            {
                Gallery gallery = new();
                gallery.Chapters = [];
                GalleryChapter chapter = new();
                chapter.Items = [];
                int i = 1;
                foreach (var item in items)
                {
                    chapter.Items.Add(new() { Item = item, Position = request.Sorted ? i++ : null });
                }
                gallery.Chapters.Add(chapter);
                await _galleryRepository.CreateAsync(gallery, cancellationToken);
            }
        }
    }
}