using Flight_Agency_Api.Features.Authorization.Services;
using Flight_Agency_Domain;
using Flight_Agency_Infrastructure;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

[assembly: FunctionsStartup(typeof(Flight_Agency_Api.Startup))]

namespace Flight_Agency_Api
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.AddDbContext<UserContext>();
            builder.Services.AddSingleton<IAuthorizationService, AuthorizationService>();
            builder.Services.AddSingleton<ITripsService, TripsService>();
        }
    }
}