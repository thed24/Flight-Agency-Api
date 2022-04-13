
using FlightAgency.Application.Common;
using FlightAgency.Contracts.Requests.Authorization;
using FlightAgency.Domain;
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
        var users = (await UserContext.Users.ToListAsync()).ToFSharpList();
        var result = UserAggregateRoot.Login(request.email, request.password, users);
        return result.ToEither();
    }

    public async Task<Either<string, User>> RegisterAsync(RegisterRequest request)
    {
        var users = (await UserContext.Users.ToListAsync()).ToFSharpList();
        var result = UserAggregateRoot.Register(request.email, request.name, request.password, users);

        if (result.IsOk)
        {
            var user = result.ResultValue;
            await UserContext.Users.AddAsync(user);
            await UserContext.SaveChangesAsync();
        }

        return result.ToEither();
    }
}
