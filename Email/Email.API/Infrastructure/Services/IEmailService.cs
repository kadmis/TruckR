using System.Threading;
using System.Threading.Tasks;
using Email.API.Infrastructure.Models;

namespace Email.API.Infrastructure.Services
{
    public interface IEmailService
    {
        public Task SendEmail(EmailModel email, CancellationToken cancellationToken = default);
    }
}
