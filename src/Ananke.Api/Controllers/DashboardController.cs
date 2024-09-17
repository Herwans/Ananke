using Ananke.Application.DTO;
using Ananke.Application.Features.Dashboard.Queries;
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
        public async Task<IActionResult> DisksUsage(CancellationToken cancellationToken)
        {
            Task<IEnumerable<DiskDTO>> task = _sender.Send(new GetDisksUsageQuery(), cancellationToken);
            IEnumerable<DiskDTO> result = await task;

            if (task.IsCanceled)
            {
                return StatusCode(StatusCodes.Status499ClientClosedRequest, "Request was cancelled.");
            }

            return Ok(result);
        }
    }
}