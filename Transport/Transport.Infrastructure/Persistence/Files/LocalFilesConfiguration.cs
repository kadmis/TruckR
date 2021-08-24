using System.IO;

namespace Transport.Infrastructure.Persistence.Files
{
    public class LocalFilesConfiguration
    {
        private readonly string _filesPath = "application-files";
        private readonly string _defaultContentType = "application/octet-stream";

        private readonly string _basePath;

        public string FilesPath => Path.Combine(_basePath, _filesPath);
        public string DefaultContentType => _defaultContentType;

        public LocalFilesConfiguration(string basePath)
        {
            _basePath = basePath;
        }
    }
}
