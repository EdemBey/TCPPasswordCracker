
using System.Security.Cryptography;
using BrutServer.Models;

namespace BrutServer;

public static class PasswordFileHandler
{
    private static readonly Converter<char, byte> Converter = CharToByte;
    public static IEnumerable<UserInfo> ReadPasswordFile(string filename)
    {
        var result = new List<UserInfo>();
        var fs = new FileStream(filename, FileMode.Open, FileAccess.Read);
        using var sr = new StreamReader(fs);
        while (!sr.EndOfStream)
        {
            var line = sr.ReadLine();
            var parts = line!.Split(":".ToCharArray());
            var userInfo = new UserInfo(parts[0], parts[1]);
            result.Add(userInfo);
        }
        return result;
    }
    public static Converter<char, byte> GetConverter()
    {
        return Converter;
    }
    private static byte CharToByte(char ch)
    {
        return Convert.ToByte(ch);
    }
    public static T[][]? Split<T>(this T[] array, int size)
    {
        var len = array.Length;
        while (array.Length%size != 0)
            --size;
        var firstDimensionLength = array.Length / size;
        var buffer = new T[size][];
        var arrays = new T[firstDimensionLength];
        for (var i = 0; i < size; i++)
        {
            buffer[i] = new T[firstDimensionLength];
            for (var j = 0; j < firstDimensionLength; j++)
            {
                buffer[i][j] = array[i * firstDimensionLength + j];
            }
        }
        return buffer;
    }
}