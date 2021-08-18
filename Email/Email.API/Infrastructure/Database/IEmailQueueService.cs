using Email.API.Infrastructure.Database.Entities;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Email.API.Infrastructure.Database
{
    public interface IEmailQueueService
    {
        public Task Add(EmailQueueItem email, CancellationToken cancellationToken = default);
        public Task<IEnumerable<EmailQueueItem>> NotSent(CancellationToken cancellationToken = default);
        public Task SetAsSent(IEnumerable<EmailQueueItem> emails, CancellationToken cancellationToken = default);
    }
}
