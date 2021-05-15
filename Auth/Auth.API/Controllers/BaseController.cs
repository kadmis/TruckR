using BuildingBlocks.Application.Identity;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Auth.API.Controllers
{
    public abstract class BaseController : ControllerBase
    {
        protected readonly IMediator Mediator;
        private readonly IIdentityAccessor _identity;

        public BaseController(IMediator mediator, IIdentityAccessor identity)
        {
            Mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _identity = identity ?? throw new ArgumentNullException(nameof(identity));
        }
        public BaseController(IMediator mediator)
        {
            Mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        protected Guid UserId()
        {
            return _identity.UserIdentity().UserId;
        }
    }
}
