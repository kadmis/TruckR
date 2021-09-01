using System;
using System.Threading;
using System.Threading.Tasks;

namespace Transport.SignalRClient.Clients.Shared
{
    public interface ITransportHubClient
    {
        Task SendAssignmentFailed(Guid assignmentId, Guid driverId, Guid dispatcherId, CancellationToken cancellationToken = default);
        Task SendAssignmentExpired(Guid assignmentId, Guid dispatcherId, CancellationToken cancellationToken = default);
    }
}
