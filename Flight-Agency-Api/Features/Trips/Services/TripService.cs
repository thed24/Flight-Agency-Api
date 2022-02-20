using Flight_Agency_Domain;
using System.Threading.Tasks;

namespace Flight_Agency_Api.Features.Authorization.Services
{
    public interface ITripsService
    {
        Task<Trip> CreateTrip(int userId, CreateTripRequest createTripRequest);
        Task<Trip> UpdateTrip(int userId, UpdateTripRequest updateTripRequest);
    }

    public class TripsService : ITripsService
    {
        public UserContext UserContext;

        public TripsService(UserContext userContext)
        {
            UserContext = userContext;
        }

        public async Task<Trip> CreateTrip(int userId, CreateTripRequest createTripRequest)
        {
            var existingUser = await UserContext.Users.FindAsync(userId);
            var newTrip = new Trip(createTripRequest.Name, createTripRequest.Destination, createTripRequest.Departure, createTripRequest.Arrival);

            existingUser.Trip = newTrip;
            await UserContext.SaveChangesAsync();

            return newTrip;
        }

        public async Task<Trip> UpdateTrip(int userId, UpdateTripRequest updateTripRequest)
        {
            var updatedTrip = new Trip(updateTripRequest.Name, updateTripRequest.Destination, updateTripRequest.Departure, updateTripRequest.Arrival)
            {
                Id = updateTripRequest.Id
            };

            UserContext.Trips.Update(updatedTrip);
            await UserContext.SaveChangesAsync();

            return updatedTrip;
        }
    }
}
