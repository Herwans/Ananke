﻿using Ananke.Application.Features.Items.Queries;
using Ananke.Application.Features.Media.Images.Commands;
using Ananke.Application.Features.Media.Images.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Ananke.Api.Controllers.Media
{
    [ApiController]
    [Route("images")]
    public class ImageController(ISender sender) : BaseSenderController(sender)
    {
        [HttpGet]
        public async Task<IActionResult> GetAllImages(int page = 1, int size = 10, CancellationToken cancellationToken = default)
        {
            return Ok(await _sender.Send(new GetItemsByTypeQuery("images", page, size), cancellationToken));
        }

        [HttpPost("gallery/create")]
        public async Task<IActionResult> CreateGalleryFromFolder(CreateGalleryFromFolderCommand request, CancellationToken cancellationToken = default)
        {
            await _sender.Send(request, cancellationToken);
            return Ok();
        }

        [HttpGet("gallery")]
        public async Task<IActionResult> GetGallery(int galleryId, CancellationToken cancellationToken = default)
        {
            return Ok(await _sender.Send(new GetGalleryByIdQuery(galleryId), cancellationToken));
        }
    }
}