using System;
using System.IO;

namespace Transport.Application.Files
{
    public class StorageFile : IDisposable
    {
        public StorageFile(string name, long length, string contentType, Stream contentStream)
        {
            Name = name;
            Length = length;
            ContentType = contentType;
            ContentStream = contentStream;
        }

        public string Name { get; }
        public long Length { get; }
        public string ContentType { get; }
        public Stream ContentStream { get; }

        public void Dispose()
        {
            ContentStream.Dispose();
        }
    }
}
