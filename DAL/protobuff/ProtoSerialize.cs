using ProtoBuf;
using System.IO;

internal static class ProtoSerializer
{
    internal static byte[] ProtoSerialize<T>(T record) where T : class
    {
        if (record == null) return null;

        try
        {
            using (var stream = new MemoryStream())
            {
                Serializer.Serialize(stream, record);
                return stream.ToArray();
            }
        }
        catch
        {
            // Log error  
            throw;
        }
    }
}