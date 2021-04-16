using Assignments.API.Infrastructure.Commands;
using Assignments.API.Infrastructure.Models.Requests;
using BuildingBlocks.API.Infrastructure.Helpers;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Assignments.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AssignmentsController : ControllerBase
    {
        private readonly IMediator _mediator;
        public AssignmentsController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateAssignment([FromBody] CreateAssignmentRequest request, CancellationToken cancellationToken = default)
        {
            return new JsonResult(await _mediator.Send(CreateAssignmentCommand.FromRequest(request, HttpContext.UserIdentity()), cancellationToken));
        }
    }
}
