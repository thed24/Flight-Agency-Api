
using FlightAgency.Contracts.Requests.Authorization;
using FlightAgency.Infrastructure;
using FlightAgency.Models;
using LanguageExt;
using Microsoft.EntityFrameworkCore;

namespace FlightAgency.Application.Features.Authorization.AuthorizationHandler;

public interface IAuthorizationHandler
{
    public Task<Either<string, User>> LoginAsync(LoginRequest loginRequest);
    public Task<Either<string, User>> RegisterAsync(RegisterRequest createUserRequest);
}

public class AuthorizationHandler : IAuthorizationHandler
{
    public UserContext UserContext;
    public AuthorizationHandler(UserContext userContext)
    {
        UserContext = userContext;
    }

    public async Task<Either<string, User>> LoginAsync(LoginRequest request)
    {
        var user = await UserContext.Users.FirstOrDefaultAsync(u => u.Email == request.Email);

        if (user is null)
        {
            return Prelude.Left<string, User>("User not found.");
        }

        if (!user.Password.Equals(request.Password))
        {
            return Prelude.Left<string, User>("Password is incorrect.");
        }

        return Prelude.Right<string, User>(user);
    }

    public async Task<Either<string, User>> RegisterAsync(RegisterRequest request)
    {
        var user = await UserContext.Users.FirstOrDefaultAsync(u => u.Email == request.Email);

        if (user != null)
        {
            return Prelude.Left<string, User>("User already exists.");
        }

        var newUser = new User(request.Email, request.Name, request.Password);
        await UserContext.Users.AddAsync(newUser);
        await UserContext.SaveChangesAsync();

        return Prelude.Right<string, User>(newUser);
    }
}
