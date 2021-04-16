using BuildingBlocks.Application.Helpers;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Auth.API.Controllers
{
    public abstract class BaseController : ControllerBase
    {
        protected readonly IMediator _mediator;

        public BaseController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        protected Guid GetCurrentUserId()
        {
            return HttpContext.UserIdentity().UserId;
        }
    }
}
