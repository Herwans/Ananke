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

        [HttpGet("files-last")]
        public async Task<IActionResult> FilesLast(CancellationToken cancellationToken)
        {
            Task<ItemDTO[]> task = _sender.Send(new GetLastFilesQuery(), cancellationToken);
            ItemDTO[] result = await task;

            if (task.IsCanceled)
            {
                return StatusCode(StatusCodes.Status499ClientClosedRequest, "Request was cancelled.");
            }

            return Ok(result);
        }

        [HttpGet("files-count")]
        public async Task<IActionResult> FilesCount(CancellationToken cancellationToken)
        {
            Task<int> task = _sender.Send(new CountFilesQuery(), cancellationToken);
            int result = await task;

            if (task.IsCanceled)
            {
                return StatusCode(StatusCodes.Status499ClientClosedRequest, "Request was cancelled.");
            }

            return Ok(result);
        }
    }
}