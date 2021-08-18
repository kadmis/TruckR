using System.Threading;
using System.Threading.Tasks;
using Email.API.Infrastructure.Database.Entities;

namespace Email.API.Infrastructure.Services
{
    public interface IEmailService
    {
        public Task<bool> SendEmail(EmailQueueItem email, CancellationToken cancellationToken = default);
    }
}
