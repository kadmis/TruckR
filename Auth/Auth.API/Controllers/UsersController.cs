using Auth.Application.Commands.Activate;
using Auth.Application.Commands.RemindPassword;
using Auth.Application.Commands.RemindUsername;
using Auth.Application.Commands.ResetPassword;
using Auth.Application.Commands.SetPassword;
using Auth.Application.Queries.UserDetails;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Auth.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : BaseController
    {
        public UsersController(IMediator mediator) : base(mediator)
        {
        }

        [HttpPatch("activate/{userId}/{activationId}")]
        public async Task<IActionResult> Activate(Guid userId, Guid activationId, CancellationToken cancellationToken = default)
        {
            return new JsonResult(await Mediator.Send(new ActivateUserCommand(userId, activationId), cancellationToken));
        }

        [Authorize]
        [HttpGet("reset-password")]
        public async Task<IActionResult> ResetPassword(CancellationToken cancellationToken = default)
        {
            return new JsonResult(await Mediator.Send(new ResetPasswordCommand(), cancellationToken));
        }

        [HttpPatch("set-password")]
        public async Task<IActionResult> SetPassword([FromBody] SetPasswordCommand command, CancellationToken cancellationToken = default)
        {
            return new JsonResult(await Mediator.Send(command, cancellationToken));
        }

        [HttpGet("remind-password")]
        public async Task<IActionResult> RemindPassword([FromQuery] RemindPasswordCommand command, CancellationToken cancellationToken = default)
        {
            return new JsonResult(await Mediator.Send(command, cancellationToken));
        }

        [HttpGet("remind-username")]
        public async Task<IActionResult> RemindUsername([FromQuery] string email, CancellationToken cancellationToken = default)
        {
            return new JsonResult(await Mediator.Send(new RemindUsernameCommand(email), cancellationToken));
        }

        [Authorize]
        [HttpGet("details")]
        public async Task<IActionResult> GetUserDetails([FromQuery] UserDetailsQuery query, CancellationToken cancellationToken = default)
        {
            return new JsonResult(await Mediator.Send(query, cancellationToken));
        }
    }
}
