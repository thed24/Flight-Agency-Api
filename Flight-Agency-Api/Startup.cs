using Flight_Agency_Api.Features.Authorization.Services;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;

[assembly: FunctionsStartup(typeof(Flight_Agency_Api.Startup))]

namespace Flight_Agency_Api
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            var context = new UserContext();
            context.Database.EnsureCreated();

            builder.Services.AddSingleton(context);
            builder.Services.AddSingleton<IAuthorizationService, AuthorizationService>();
            builder.Services.AddSingleton<ITripsService, TripsService>();
        }
    }
}