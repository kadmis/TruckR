using System;
using System.Threading;
using System.Threading.Tasks;
using Auth.Application.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Auth.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : BaseController
    {
        public AuthController(IMediator mediator):base(mediator)
        {
        }

        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate([FromBody] AuthenticationCommand command, CancellationToken cancellationToken = default)
        {
            return new JsonResult(await Mediator.Send(command, cancellationToken));
        }

        [HttpPost("register-driver")]
        public async Task<IActionResult> RegisterDriver([FromBody] RegisterDriverCommand command, CancellationToken cancellationToken = default)
        {
            return new JsonResult(await Mediator.Send(command, cancellationToken));
        }

        [HttpPost("register-dispatcher")]
        public async Task<IActionResult> RegisterDispatcher([FromBody] RegisterDispatcherCommand command, CancellationToken cancellationToken = default)
        {
            return new JsonResult(await Mediator.Send(command, cancellationToken));
        }
    }
}
