namespace BrutServer.Models;

public class UserInfoClearText
{
    private string UserName { get;}
    private string Password { get;}
    public UserInfoClearText(string username, string? password)
    {
        UserName = username ?? throw new ArgumentNullException(nameof(username));
        Password = password ?? throw new ArgumentNullException(nameof(password));
    }
    public override string ToString()
    {
        return UserName + ": " + Password;
    }
}