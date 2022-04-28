using System.Data;
using Authentication_Minimal_API.Core;
using Dapper;
using Npgsql;

namespace Authentication_Minimal_API.Modules.Authentication;

public class UserRepository : RepositoryBase, IUserRepository
{
    public UserRepository(IConfiguration configuration) : base(configuration)
    {
    }

    public async Task<User> Create(User user, CancellationToken cancellationToken)
    {
        using IDbConnection connection = new NpgsqlConnection(AppSettings!.DbConnectionString);

        var result = await connection.ExecuteScalarAsync<int>(
            @"insert into ""user"" (username, password_hash, password_salt, first_name, last_name, email, created_date) 
            values (@Username, @PasswordHash, @PasswordSalt, @FirstName, @LastName, @Email, timezone('utc', now()))
            returning id;",
            new { user.Username, user.PasswordHash, user.PasswordSalt, user.FirstName, user.LastName, user.Email });

        if (result <= 0)
            throw new RepositoryOperationException("Create returned invalid id");

        user.Id = result;

        return user;
    }

    public async Task<User> GetByUsername(string username, CancellationToken cancellationToken)
    {
        using IDbConnection connection = new NpgsqlConnection(AppSettings!.DbConnectionString);

        var user = await connection.QuerySingleOrDefaultAsync<User>(
            @"select id, username, password_hash as PasswordHash, password_salt as PasswordSalt, first_name as FirstName, last_name as LastName, email from ""user"" where username = @username;",
            new { username });

        return user;
    }

    public async Task UpdatePassword(User user, byte[] passwordHash, byte[] passwordSalt,
        CancellationToken cancellationToken)
    {
        using IDbConnection connection = new NpgsqlConnection(AppSettings!.DbConnectionString);

        var result = await connection.ExecuteAsync(
            @"update ""user"" 
            set password_hash = @PasswordHash, password_salt = @PasswordSalt
            where id = @Id;",
            new { user.Id, passwordHash, passwordSalt });

        if (result != 1)
            throw new RepositoryOperationException($"Update Password affected {result} rows");
    }
}