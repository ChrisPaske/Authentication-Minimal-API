using Microsoft.AspNetCore.Mvc;

namespace Authentication_Minimal_API.Modules.Authentication;

public class AuthenticationModule : IModule
{
    public IServiceCollection RegisterModule(IServiceCollection services)
    {
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IUserAccountService, UserAccountService>();
        return services;
    }

    public IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder endpoints)
    {
        endpoints.MapPost("/changepassword",
            async ([FromServices] IUserAccountService userAccountService,
                ChangePasswordRequest changePasswordRequest,
                CancellationToken cancellationToken) =>
            {
                await userAccountService.ChangePassword(changePasswordRequest, cancellationToken);
                return Results.Ok();
            });

        endpoints.MapPost("/register",
            async ([FromServices] IUserAccountService userAccountService,
                RegisterUserRequest registerUserRequest,
                CancellationToken cancellationToken) =>
            {
                await userAccountService.Register(registerUserRequest, cancellationToken);
                return Results.Ok();
            });

        endpoints.MapPost("/login",
            async ([FromServices] IUserAccountService userAccountService,
                LoginUserRequest loginUserRequest,
                CancellationToken cancellationToken) =>
            {
                var token = await userAccountService.Login(loginUserRequest, cancellationToken);
                return token is null ? Results.Unauthorized() : Results.Ok(token);
            });

        return endpoints;
    }
}