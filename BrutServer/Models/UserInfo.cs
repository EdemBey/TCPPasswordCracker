namespace BrutServer.Models;
[Serializable]
public class UserInfo
{
    public string Username { get; }
    public byte[] EncryptedPassword { get; }
    public UserInfo(string username, string encryptedPasswordBase64)
    {
        Username = username ?? throw new ArgumentNullException(nameof(username));
        EncryptedPassword = Convert.FromBase64String(encryptedPasswordBase64) ?? throw new ArgumentNullException(nameof(encryptedPasswordBase64));;
    }
    public override string ToString()
    {
        return $"{Username} {EncryptedPassword}";
    }
}