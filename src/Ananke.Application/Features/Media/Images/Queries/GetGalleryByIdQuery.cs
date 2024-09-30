using Ananke.Application.DTO;
using Ananke.Application.Mappers;
using Ananke.Domain.Entity.Items;
using Ananke.Domain.Entity.Media.Images.Galleries;
using Ananke.Infrastructure.Persistence.Interfaces.Repositories.Media.Images;
using MediatR;

namespace Ananke.Application.Features.Media.Images.Queries
{
    public record GetGalleryByIdQuery(int Id) : IRequest<Gallery?> { }

    public class GetGalleryByIdQueryHandler : IRequestHandler<GetGalleryByIdQuery, Gallery?>
    {
        private readonly IGalleryRepository _galleryRepository;

        public GetGalleryByIdQueryHandler(IGalleryRepository galleryRepository)
        {
            _galleryRepository = galleryRepository;
        }

        public async Task<Gallery?> Handle(GetGalleryByIdQuery request, CancellationToken cancellationToken = default)
        {
            Gallery? gallery = await _galleryRepository.GetByIdAsync(request.Id, cancellationToken);
            return gallery;
        }
    }
}