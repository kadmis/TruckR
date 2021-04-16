using Auth.Domain.Data.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace Auth.Domain.Services.Registration
{
    public interface IUserRegistrationService
    {
        Task<User> RegisterDriver(string username, string firstname, string lastname, string password, string email, string phoneNumber, CancellationToken cancellationToken = default);
        Task<User> RegisterDispatcher(string username, string firstname, string lastname, string password, string email, string phoneNumber, CancellationToken cancellationToken = default);
    }
}
