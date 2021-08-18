using System;
using System.Threading;
using System.Threading.Tasks;

namespace Transport.Domain.Groups
{
    public interface ITransportGroupsRepository
    {
        Task<TransportGroup> FindById(Guid id, CancellationToken cancellationToken = default);
        TransportGroup Add(TransportGroup group);
        Task<TransportGroup> FindGroupWithFreeSpots(CancellationToken cancellationToken = default);
    }
}
