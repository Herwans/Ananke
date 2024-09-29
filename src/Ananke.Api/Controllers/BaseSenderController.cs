using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Ananke.Api.Controllers
{
    [ApiController]
    public abstract class BaseSenderController(ISender sender) : ControllerBase
    {
        protected readonly ISender _sender = sender;
    }
}