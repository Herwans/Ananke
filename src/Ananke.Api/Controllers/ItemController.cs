using Microsoft.AspNetCore.Mvc;
using Ananke.Application.Services;
using Ananke.Api.Requests;

namespace Ananke.Api.Controllers
{
    [ApiController]
    [Route("items")]
    public class ItemController : Controller
    {
        private readonly IItemService _itemService;

        public ItemController(IItemService itemService)
        {
            _itemService = itemService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return new OkObjectResult(_itemService.GetItems());
        }

        [HttpGet("{id}")]
        public IActionResult GetItem(int id)
        {
            return new OkObjectResult(_itemService.GetItem(id));
        }

        [HttpPost("add-item")]
        public IActionResult AddItem([FromBody] AddItemRequest request)
        {
            _itemService.AddItem(request.Path);
            return Ok();
        }

        [HttpPost("add-directory")]
        public IActionResult AddDirectory([FromBody] AddDirectoryRequest request)
        {
            _itemService.AddDirectory(request.Path, request.Recursive);
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult RemoveItem(int id)
        {
            _itemService.RemoveItem(id);
            return Ok();
        }
    }
}