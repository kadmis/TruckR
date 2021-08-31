using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading;
using System.Threading.Tasks;
using Transport.Application.TransportGroups.Queries.DriverInfo;

namespace Transport.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransportGroupsController : ControllerBase
    {
        private readonly IMediator _mediator;
        public TransportGroupsController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpGet("driver-info/{driverId}")]
        public async Task<JsonResult> FetchDriverInfo(Guid driverId, CancellationToken cancellationToken = default)
        {
            return new JsonResult(await _mediator.Send(new FetchDriverInfoQuery(driverId), cancellationToken));
        }
    }
}
