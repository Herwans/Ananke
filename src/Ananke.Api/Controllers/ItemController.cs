using Microsoft.AspNetCore.Mvc;
using MediatR;
using Ananke.Application.Features.Items.Commands;
using Ananke.Application.Features.Items.Queries;
using Ananke.Application.DTO;

namespace Ananke.Api.Controllers
{
    [ApiController]
    [Route("items")]
    public class ItemController : Controller
    {
        private readonly ISender _sender;

        public ItemController(ISender sender)
        {
            _sender = sender;
        }

        [HttpGet]
        public async Task<IActionResult> Index(CancellationToken cancellationToken)
        {
            return Ok(await _sender.Send(new GetItemsQuery(), cancellationToken));
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