using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Authentication_Minimal_API.Core;
using Microsoft.IdentityModel.Tokens;

namespace Authentication_Minimal_API.Modules.Authentication;

public class UserAccountService : ApplicationServiceBase, IUserAccountService
{
    private readonly IUserRepository _userRepository;

    public UserAccountService(IConfiguration configuration, IUserRepository userRepository) : base(configuration)
    {
        ArgumentNullException.ThrowIfNull(userRepository, nameof(IUserRepository));

        _userRepository = userRepository;
    }

    public async Task ChangePassword(ChangePasswordRequest changePasswordRequest, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(changePasswordRequest, nameof(ChangePasswordRequest));
        ValidatePasswordRequirements(changePasswordRequest.NewPassword);

        var user = await _userRepository.GetByUsername(changePasswordRequest.Username, cancellationToken);
        if (user is null)
            throw new ArgumentException("Invalid username");
        if (!VerifyPasswordHash(changePasswordRequest.CurrentPassword, user.PasswordHash, user.PasswordSalt))
            throw new ArgumentException("Current password is invalid");

        CreatePasswordHash(changePasswordRequest.NewPassword, out var passwordHash, out var passwordSalt);

        await _userRepository.UpdatePassword(user, passwordHash, passwordSalt, cancellationToken);
    }

    public async Task<string> Login(LoginUserRequest loginUserRequest, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(loginUserRequest, nameof(LoginUserRequest));

        var user = await _userRepository.GetByUsername(loginUserRequest.Username, cancellationToken);

        if (user is null || !VerifyPasswordHash(loginUserRequest.Password, user.PasswordHash, user.PasswordSalt))
            return null;

        return CreateToken(user);
    }

    public async Task Register(RegisterUserRequest registerUserRequest, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(registerUserRequest, nameof(RegisterUserRequest));
        ValidatePasswordRequirements(registerUserRequest.Password);

        CreatePasswordHash(registerUserRequest.Password, out var passwordHash, out var passwordSalt);

        await _userRepository.Create(
            new User
            {
                Username = registerUserRequest.Username,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                FirstName = registerUserRequest.FirstName,
                LastName = registerUserRequest.LastName,
                Email = registerUserRequest.Email
            }, cancellationToken);
    }

    private string CreateToken(User user)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(AppSettings.AuthSecurityKey!));

        var claims = new List<Claim>
        {
            new(ClaimTypes.Name, user.Username),
            new(ClaimTypes.GivenName, user.FirstName),
            new(ClaimTypes.Surname, user.LastName),
            new(ClaimTypes.Email, user.Email),
            new(ClaimTypes.Sid, user.Id.ToString())
        };

        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

        var token = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.Now.AddHours(AppSettings.AuthExpirationHours),
            signingCredentials: credentials);

        var jwt = new JwtSecurityTokenHandler().WriteToken(token);

        return jwt;
    }

    private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
    {
        using (var hmac = new HMACSHA512())
        {
            passwordSalt = hmac.Key;
            passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
        }
    }

    private static void ValidatePasswordRequirements(string password)
    {
    }

    private static bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
    {
        using (var hmac = new HMACSHA512(passwordSalt))
        {
            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            return computedHash.SequenceEqual(passwordHash);
        }
    }
}