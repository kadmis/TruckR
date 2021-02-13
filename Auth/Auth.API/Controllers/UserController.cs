using Auth.API.Infrastructure.Commands;
using Auth.API.Infrastructure.Models.Requests;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace Auth.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : BaseController
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
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

        [Authorize]
        [HttpPatch("set-password")]
        public async Task<IActionResult> SetPassword([FromBody] SetPasswordRequest request, CancellationToken cancellationToken = default)
        {
            return new JsonResult(await _mediator.Send(new SetPasswordCommand(GetCurrentUserId(), request.ResetToken, request.Password), cancellationToken));
        }
    }
}
