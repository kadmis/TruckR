using System;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Auth.API.Infrastructure.Commands;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Auth.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : Controller
    {
        private readonly IMediator _mediator;
        private Guid GetCurrentUserId()
        {
            if (Guid.TryParse(HttpContext.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value, out Guid guid))
            {
                return guid;
            }
            else
            {
                throw new Exception("Couldn't get user.");
            }
        }

        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate([FromBody] AuthenticationCommand command, CancellationToken cancellationToken = default)
        {
            return new JsonResult(await _mediator.Send(command, cancellationToken));
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegistrationCommand command, CancellationToken cancellationToken = default)
        {
            return new JsonResult(await _mediator.Send(command, cancellationToken));
        }

        [HttpPatch("activate/{userId}/{activationId}")]
        public async Task<IActionResult> Activate(Guid userId, Guid activationId, CancellationToken cancellationToken = default)
        {
            return new JsonResult(await _mediator.Send(new ActivateUserCommand(userId, activationId), cancellationToken));
        }

        [Authorize]
        [HttpPatch("reset-password")]
        public async Task<IActionResult> ResetPassword(CancellationToken cancellationToken = default)
        {
            return new JsonResult(await _mediator.Send(new ResetPasswordCommand(GetCurrentUserId()), cancellationToken));
        }
    }
}
