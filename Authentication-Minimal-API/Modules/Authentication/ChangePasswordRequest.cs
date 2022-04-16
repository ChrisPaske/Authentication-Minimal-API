namespace Authentication_Minimal_API.Modules.Authentication;

public class ChangePasswordRequest
{
    public string Username { get; set; }
    public string CurrentPassword { get; set; }
    public string NewPassword { get; set; }
}