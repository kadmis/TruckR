using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Auth.API.Controllers
{
    public abstract class BaseController : ControllerBase
    {
        protected readonly IMediator Mediator;

        public BaseController(IMediator mediator)
        {
            Mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }
    }
}
