using Flight_Agency_Domain;

namespace Flight_Agency_Api.Features.Authorization.Services
{
    public interface ITripsService
    {
        Task<Trip> CreateTrip(int userId, CreateTripRequest createTripRequest);
        Task<Trip> UpdateTrip(int userId, UpdateTripRequest updateTripRequest);
        Task<List<Trip>> GetTrips(int userId);
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
            var newTrip = new Trip()
            {
                Destination = createTripRequest.Destination,
                Stops = createTripRequest.Stops
            };

            existingUser.Trips.Add(newTrip);
            await UserContext.SaveChangesAsync();

            return newTrip;
        }

        public async Task<List<Trip>> GetTrips(int userId)
        {
            var existingUser = await UserContext.Users.FindAsync(userId);
            return existingUser.Trips;
        }

        public async Task<Trip> UpdateTrip(int userId, UpdateTripRequest updateTripRequest)
        {
            var updatedTrip = new Trip()
            {
                Id = updateTripRequest.Id,
                Destination = updateTripRequest.Destination,
                Stops = updateTripRequest.Stops
            };

            UserContext.Trips.Update(updatedTrip);
            await UserContext.SaveChangesAsync();

            return updatedTrip;
        }
    }
}
