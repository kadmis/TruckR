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
using Transport.Application.Assignments.Queries.DriversCurrentAssignment;
using Transport.Application.Assignments.Queries.FreeAssignments;
using Transport.Application.Assignments.Queries.GetTransportDocument;
using Transport.Application.Assignments.Queries.TransportDocumentInfo;

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
        public async Task<JsonResult> CreateAssignment([FromForm] CreateAssignmentCommand command, 
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

        [HttpGet("{assignmentId}/document-info")]
        public async Task<JsonResult> FetchTransportDocumentInfo(Guid assignmentId,CancellationToken cancellationToken = default)
        {
            return new JsonResult(await _mediator.Send(new TransportDocumentInfoQuery(assignmentId), cancellationToken));
        }

        [HttpGet("drivers-current-assignment/{driverId}")]
        public async Task<JsonResult> DriversCurrentAssignment(Guid driverId, CancellationToken cancellationToken = default)
        {
            return new JsonResult(await _mediator.Send(new DriversCurrentAssignmentQuery(driverId), cancellationToken));
        }

        [HttpGet("free-assignments")]
        public async Task<JsonResult> FetchFreeAssignments([FromQuery] FreeAssignmentsQuery query, CancellationToken cancellationToken = default)
        {
            return new JsonResult(await _mediator.Send(query, cancellationToken));
        }
    }
}
