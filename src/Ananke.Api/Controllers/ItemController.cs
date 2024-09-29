using Microsoft.AspNetCore.Mvc;
using MediatR;
using Ananke.Application.Features.Items.Commands;
using Ananke.Application.Features.Items.Queries;
using Ananke.Application.DTO;
using System.Drawing;

namespace Ananke.Api.Controllers
{
    [ApiController]
    [Route("items")]
    public class ItemController(ISender sender) : BaseController(sender)
    {
        [HttpGet]
        public async Task<IActionResult> Index(CancellationToken cancellationToken)
        {
            return Ok(await _sender.Send(new GetItemsQuery(), cancellationToken));
        }

        [HttpGet("type")]
        public async Task<IActionResult> GetItemsByType(string type, int page = 1, int size = 10, CancellationToken cancellationToken = default)
        {
            return Ok(await _sender.Send(new GetItemsByTypeQuery(type, page, size), cancellationToken));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetItem(int id, CancellationToken cancellationToken)
        {
            ItemDTO? itemDTO = await _sender.Send(new GetItemByIdQuery(id), cancellationToken);
            if (itemDTO == null)
                return NotFound();
            return Ok(itemDTO);
        }

        [HttpPost("add-item")]
        public async Task<IActionResult> AddItem(AddItemCommand command, CancellationToken cancellationToken)
        {
            await _sender.Send(command, cancellationToken);
            return Ok();
        }

        [HttpPost("add-directory")]
        public async Task<IActionResult> AddDirectoryAsync(AddDirectoryCommand command, CancellationToken cancellationToken)
        {
            await _sender.Send(command, cancellationToken);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveItem(int id, CancellationToken cancellationToken)
        {
            await _sender.Send(new DeleteItemByIdCommand(id), cancellationToken);
            return Ok();
        }
    }
}