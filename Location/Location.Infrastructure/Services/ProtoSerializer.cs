using ProtoBuf;
using System.IO;

namespace Location.Infrastructure.Services
{
    public static class ProtoSerializer
    {
        public static byte[] Serialize<T>(T item) where T : class
        {
            using var stream = new MemoryStream();
            Serializer.Serialize(stream, item);

            return stream.ToArray();
        }
    }
}
