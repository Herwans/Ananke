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
        public IActionResult Index()
        {
            return Ok(_sender.Send(new GetItemsQuery()).Result);
        }

        [HttpGet("{id}")]
        public IActionResult GetItem(int id)
        {
            ItemDTO? itemDTO = _sender.Send(new GetItemByIdQuery(id)).Result;
            if (itemDTO == null)
                return NotFound();
            return Ok(itemDTO);
        }

        [HttpPost("add-item")]
        public IActionResult AddItem(AddItemCommand command)
        {
            _sender.Send(command);
            return Ok();
        }

        [HttpPost("add-directory")]
        public IActionResult AddDirectory(AddDirectoryCommand command)
        {
            _sender.Send(command);
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult RemoveItem(int id)
        {
            _sender.Send(new DeleteItemByIdCommand(id));
            return Ok();
        }
    }
}