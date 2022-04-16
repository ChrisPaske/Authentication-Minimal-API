namespace Authentication_Minimal_API.Modules.Authentication;

public interface IUserAccountService
{
    Task ChangePassword(ChangePasswordRequest changePasswordRequest, CancellationToken cancellationToken);
    Task<string?> Login(LoginUserRequest loginUserRequest, CancellationToken cancellationToken);
    Task Register(RegisterUserRequest registerUserRequest, CancellationToken cancellationToken);
}