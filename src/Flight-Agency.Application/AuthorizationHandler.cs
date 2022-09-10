using FlightAgency.Contracts.Requests.Authorization;
using FlightAgency.Infrastructure;
using FlightAgency.Models.User;
using LanguageExt;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using static LanguageExt.Prelude;

namespace FlightAgency.Application;

public interface IAuthorizationHandler
{
    public Task<Either<string, User>> LoginAsync(LoginRequest loginRequest);
    public Task<Either<string, User>> RegisterAsync(RegisterRequest createUserRequest);
}

public class AuthorizationHandler : IAuthorizationHandler
{
    private readonly ILogger Logger;
    private readonly UserContext UserContext;

    public AuthorizationHandler(UserContext userContext, ILogger<AuthorizationHandler> logger)
    {
        UserContext = userContext;
        Logger = logger;
    }

    public async Task<Either<string, User>> LoginAsync(LoginRequest request)
    {
        User? user = await UserContext.Users.FirstOrDefaultAsync(u => u.Email == request.Email);

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
        User? user = await UserContext.Users.FirstOrDefaultAsync(u => u.Email == request.Email);

        if (user != null)
        {
            Logger.LogError($"User with email {request.Email} already exists.");
            return Left<string, User>("User already exists.");
        }

        User newUser = new()
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