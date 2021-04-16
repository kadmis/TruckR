using Location.Application.DTO.Filters;
using Location.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Location.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class StatisticsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public StatisticsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetForUser(Guid userId, [FromQuery] StatisticsFilter filter, CancellationToken cancellationToken = default)
        {
            return new JsonResult(await _mediator.Send(new GetStatisticsForUserQuery(userId, filter), cancellationToken));
        }
    }
}
