using System;
using System.Threading;
using System.Threading.Tasks;
using Auth.API.Infrastructure.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Auth.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : BaseController
    {
        private readonly IMediator _mediator;

        public AuthController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
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
    }
}
