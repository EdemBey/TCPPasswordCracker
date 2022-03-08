
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
}