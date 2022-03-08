using System.Runtime.Serialization.Formatters.Binary;

namespace BrutServer.Models;

public static class DataHelp
{
    public static byte[] ObjectToByteArray(object obj)
    {
        var bf = new BinaryFormatter();
        using var ms = new MemoryStream();
#pragma warning disable CS0618
        bf.Serialize(ms, obj);
#pragma warning restore CS0618
        return ms.ToArray();
    }
    public static Object ByteArrayToObject(byte[] arrBytes)
    {
        using var memStream = new MemoryStream();
        var binForm = new BinaryFormatter();
        memStream.Write(arrBytes, 0, arrBytes.Length);
        memStream.Seek(0, SeekOrigin.Begin);
#pragma warning disable CS0618
        var obj = binForm.Deserialize(memStream);
#pragma warning restore CS0618
        return obj;
    }
}