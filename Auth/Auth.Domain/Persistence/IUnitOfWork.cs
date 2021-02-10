using Auth.Domain.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Auth.Domain.Persistence
{
    public interface IUnitOfWork
    {
        Task<int> Save(CancellationToken cancellationToken = default);
        IUserRepository UserRepository { get; }
    }
}
