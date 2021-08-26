using System.Threading;
using System.Threading.Tasks;
using Auth.Application.Commands.Authenticate;
using Auth.Application.Commands.RefreshToken;
using Auth.Application.Commands.RegisterDispatcher;
using Auth.Application.Commands.RegisterDriver;
using MediatR;
using Microsoft.AspNetCore.Authorization;
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

        [HttpPost("refresh-token")]
        public async Task<IActionResult> Refresh([FromBody] RefreshTokenCommand command, CancellationToken cancellationToken = default)
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
