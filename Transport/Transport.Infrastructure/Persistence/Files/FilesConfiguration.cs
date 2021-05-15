using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Transport.Infrastructure.Persistence.Files
{
    public class FilesConfiguration
    {
        private readonly string _filesPath = "files";
        private readonly string _defaultContentType = "application/octet-stream";

        private readonly string _basePath;

        public string FilesPath => Path.Combine(_basePath, _filesPath);
        public string DefaultContentType => _defaultContentType;

        public FilesConfiguration(string basePath)
        {
            _basePath = basePath;
        }
    }
}
