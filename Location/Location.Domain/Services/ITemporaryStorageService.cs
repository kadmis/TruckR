using System.Threading.Tasks;

namespace Location.Domain.Services
{
    public interface ITemporaryStorageService<T> where T : class
    {
        Task AddAsync(T item);
    }
}
