namespace Authentication_Minimal_API.Modules.Authentication;

public class User
{
    public long Id { get; set; }

    public string Username { get; set; } = string.Empty;
    public byte[] PasswordHash { get; set; }
    public byte[] PasswordSalt { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
}