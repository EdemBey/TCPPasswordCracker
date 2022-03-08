using System.Text;

namespace Client;

public static class StringUtilities
{
    public static string? Capitalize(string? str)
    {
        if (str == null) throw new ArgumentNullException(nameof(str));
        if (str.Trim().Length == 0) return str;
        var firstLetterUppercase = str[..1].ToUpper();
        var theRest = str[1..];
        return firstLetterUppercase + theRest;
    }
    public static string? Reverse(string? str)
    {
        if (str == null) throw new ArgumentNullException(nameof(str));
        if (str.Trim().Length == 0) return str;
        var reverseString = new StringBuilder();
        for (var i = 0; i < str.Length; i++)
        {
            reverseString.Append(str.ElementAt(str.Length - 1 - i));
        }
        return reverseString.ToString();
    }
}