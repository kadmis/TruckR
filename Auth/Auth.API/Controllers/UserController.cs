﻿using Auth.API.Infrastructure.Commands;
using Auth.API.Infrastructure.Models.Requests;
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
    public class UserController : BaseController
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpGet("activate/{userId}/{activationId}")]
        public async Task<IActionResult> Activate(Guid userId, Guid activationId, CancellationToken cancellationToken = default)
        {
            return new JsonResult(await _mediator.Send(new ActivateUserCommand(userId, activationId), cancellationToken));
        }

        [Authorize]
        [HttpGet("reset-password")]
        public async Task<IActionResult> ResetPassword(CancellationToken cancellationToken = default)
        {
            return new JsonResult(await _mediator.Send(new ResetPasswordCommand(GetCurrentUserId()), cancellationToken));
        }

        [HttpPatch("set-password")]
        public async Task<IActionResult> SetPassword([FromBody] SetPasswordRequest request, CancellationToken cancellationToken = default)
        {
            return new JsonResult(await _mediator.Send(new SetPasswordCommand(request.UserId, request.ResetToken, request.Password), cancellationToken));
        }

        [HttpGet("remind-password")]
        public async Task<IActionResult> RemindPassword([FromQuery] RemindPasswordRequest request, CancellationToken cancellationToken = default)
        {
            return new JsonResult(await _mediator.Send(new RemindPasswordCommand(request.Email, request.Username), cancellationToken));
        }

        [HttpGet("remind-username")]
        public async Task<IActionResult> RemindUsername([FromQuery] string email, CancellationToken cancellationToken = default)
        {
            return new JsonResult(await _mediator.Send(new RemindUsernameCommand(email), cancellationToken));
        }
    }
}