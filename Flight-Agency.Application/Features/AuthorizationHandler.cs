using FlightAgency.Contracts.Requests.Authorization;
using FlightAgency.Infrastructure;
using FlightAgency.Models;
using LanguageExt;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using static LanguageExt.Prelude;

namespace FlightAgency.Application.Features.Authorization.AuthorizationHandler;

public interface IAuthorizationHandler
{
    public Task<Either<string, User>> LoginAsync(LoginRequest loginRequest);
    public Task<Either<string, User>> RegisterAsync(RegisterRequest createUserRequest);
}

public class AuthorizationHandler : IAuthorizationHandler
{
    private readonly UserContext UserContext;
    private readonly ILogger Logger;

    public AuthorizationHandler(UserContext userContext, ILogger<AuthorizationHandler> logger)
    {
        UserContext = userContext;
        Logger = logger;
    }

    public async Task<Either<string, User>> LoginAsync(LoginRequest request)
    {
        var user = await UserContext.Users.FirstOrDefaultAsync(u => u.Email == request.Email);

        if (user is null)
        {
            Logger.LogError($"User with email {request.Email} was not found.");
            return Left<string, User>("User not found.");
        }

        if (!user.Password.Equals(request.Password))
        {
            Logger.LogError($"User {user.Email} failed to login.");
            return Left<string, User>("Password is incorrect.");
        }

        return Right<string, User>(user);
    }

    public async Task<Either<string, User>> RegisterAsync(RegisterRequest request)
    {
        var user = await UserContext.Users.FirstOrDefaultAsync(u => u.Email == request.Email);

        if (user != null)
        {
            Logger.LogError($"User with email {request.Email} already exists.");
            return Left<string, User>("User already exists.");
        }

        var newUser = new User()
        {
            Email = request.Email,
            Name = request.Name,
            Password = request.Password
        };

        await UserContext.Users.AddAsync(newUser);
        await UserContext.SaveChangesAsync();

        return Right<string, User>(newUser);
    }
}
