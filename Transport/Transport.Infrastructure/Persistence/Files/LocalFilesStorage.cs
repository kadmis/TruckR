using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Transport.Application.Files;

namespace Transport.Infrastructure.Persistence.Files
{
    public class LocalFilesStorage : IFilesStorage
    {
        private readonly LocalFilesConfiguration _configuration;

        public LocalFilesStorage(LocalFilesConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<StorageFile> Read(string path, string name, CancellationToken cancellationToken = default)
        {
            var info = new FileInfo(FileFullPath(path, name));

            if (!info.Exists)
                return null;

            return await Task.FromResult(new StorageFile(info.Name, info.Length, _configuration.DefaultContentType, info.OpenRead()));
        }

        public Task<bool> Remove(string path, string name, CancellationToken cancellationToken = default)
        {
            var info = new FileInfo(FileFullPath(path, name));

            if (!info.Exists)
                return Task.FromResult(false);

            var directory = new DirectoryInfo(FileDirectoryPath(path));

            info.Delete();
            directory.Delete();

            return Task.FromResult(true);
        }

        public async Task<bool> Save(string path, string name, long fileLength, string contentType, Stream fileStream, CancellationToken cancellationToken = default)
        {
            var filePath = FileFullPath(path, name);
            Directory.CreateDirectory(filePath);

            using var writer = new FileStream(filePath, FileMode.Create, FileAccess.Write);

            byte[] buffer = new byte[512 * 1024];
            int bytesRead;

            while ((bytesRead = await fileStream.ReadAsync(buffer.AsMemory(0, buffer.Length), cancellationToken)) > 0)
            {
                await writer.WriteAsync(buffer.AsMemory(0, bytesRead), cancellationToken);
            }

            return true;
        }

        private string FileFullPath(string path, string name)
        {
            return Path.Combine(FileDirectoryPath(path), name);
        }
        private string FileDirectoryPath(string path)
        {
            return Path.Combine(_configuration.FilesPath, path);
        }
    }
}
