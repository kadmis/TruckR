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
            return new JsonResult(await _mediator.Send(command, cancellationToken));
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] DriverRegistrationCommand command, CancellationToken cancellationToken = default)
        {
            return new JsonResult(await _mediator.Send(command, cancellationToken));
        }
    }
}
