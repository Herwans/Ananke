using Ananke.Application.Features.Dashboard.Queries;
using Ananke.Application.Features.Items.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Ananke.Api.Controllers
{
    [ApiController]
    [Route("dashboard")]
    public class DashboardController : Controller
    {
        private readonly ISender _sender;

        public DashboardController(ISender sender)
        {
            _sender = sender;
        }

        [HttpGet("disks-usage")]
        public IActionResult DisksUsage()
        {
            return Ok(_sender.Send(new GetDisksUsageQuery()).Result);
        }
    }
}