using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Transport.Application.Files
{
    public interface IFilesStorage
    {
        Task<StorageFile> Read(string path, string name, CancellationToken cancellationToken = default);
        Task<bool> Save(string path, string name, long fileLength, string contentType, Stream fileStream, CancellationToken cancellationToken = default);
        Task<bool> Remove(string path, string name, CancellationToken cancellationToken = default);
    }
}
