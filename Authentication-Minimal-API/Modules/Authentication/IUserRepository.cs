namespace Authentication_Minimal_API.Modules.Authentication;

public interface IUserRepository
{
    Task<User> Create(User user, CancellationToken cancellationToken);
    Task<User?> GetByUsername(string username, CancellationToken cancellationToken);
    Task UpdatePassword(User user, byte[] passwordHash, byte[] passwordSalt, CancellationToken cancellationToken);
}