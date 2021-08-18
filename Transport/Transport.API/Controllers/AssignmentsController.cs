using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading;
using System.Threading.Tasks;
using Transport.Application.Assignments.Commands.CreateAssignment;
using Transport.Application.Assignments.Commands.FinishAssignment;
using Transport.Application.Assignments.Commands.TakeAssignment;
using Transport.Application.Assignments.Queries.AssignmentDetails;
using Transport.Application.Assignments.Queries.GetTransportDocument;

namespace Transport.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AssignmentsController : ControllerBase
    {
        private readonly IMediator _mediator;
        public AssignmentsController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpGet("{assignmentId}")]
        public async Task<JsonResult> Details(Guid assignmentId, 
            CancellationToken cancellationToken = default)
        {
            return new JsonResult(await _mediator.Send(new AssignmentDetailsQuery(assignmentId), cancellationToken));
        }

        [Authorize(Roles = "Dispatcher")]
        [HttpPost]
        public async Task<JsonResult> CreateAssignment([FromBody] CreateAssignmentCommand command, 
            CancellationToken cancellationToken = default)
        {
            return new JsonResult(await _mediator.Send(command, cancellationToken));
        }

        [Authorize(Roles = "Driver")]
        [HttpPut("{assignmentId}/take")]
        public async Task<JsonResult> TakeAssignment(Guid assignmentId, 
            CancellationToken cancellationToken = default)
        {
            return new JsonResult(await _mediator.Send(new TakeAssignmentCommand(assignmentId), cancellationToken));
        }

        [Authorize(Roles = "Driver")]
        [HttpPut("{assignmentId}/finish")]
        public async Task<JsonResult> FinishAssignment(Guid assignmentId, 
            CancellationToken cancellationToken = default)
        {
            return new JsonResult(await _mediator.Send(new FinishAssignmentCommand(assignmentId), cancellationToken));
        }

        [HttpGet("{assignmentId}/download-document")]
        public async Task<FileStreamResult> DownloadTransportDocument(Guid assignmentId, 
            CancellationToken cancellationToken = default)
        {
            return await _mediator.Send(new GetTransportDocumentQuery(assignmentId), cancellationToken);
        }
    }
}
