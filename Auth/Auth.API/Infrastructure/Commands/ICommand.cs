using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Auth.API.Infrastructure.Commands
{
    public interface ICommand<T> : IRequest<T>
    {
    }
}
