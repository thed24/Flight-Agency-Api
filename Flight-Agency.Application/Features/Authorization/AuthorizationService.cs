using Flight_Agency_Domain;

namespace Flight_Agency_Api.Features.Authorization.Services
{
    public interface IAuthorizationService
    {
        Task<User> Login(LoginRequest loginRequest);
        Task<User> Register(CreateUserRequest createUserRequest);
    }
    public class AuthorizationService : IAuthorizationService
    {
        public UserContext UserContext;
        public AuthorizationService(UserContext userContext)
        {
            UserContext = userContext;
        }

        public async Task<User> Login(LoginRequest loginRequest)
        {
            var existingUser = UserContext.Users.FirstOrDefault(user => user.Email == loginRequest.Email);

            if (existingUser is not null && existingUser.Password == loginRequest.Password)
            {
                return existingUser;
            }
            else
            {
                return null;
            }
        }

        public async Task<User> Register(CreateUserRequest createUserRequest)
        {
            var existingUser = UserContext.Users.FirstOrDefault(user => user.Email == createUserRequest.Email);

            if (existingUser is null)
            {
                var user = new User(createUserRequest.Password, createUserRequest.Name, createUserRequest.Email);
                await UserContext.Users.AddAsync(user);
                await UserContext.SaveChangesAsync();

                return user;
            }

            return null;
        }
    }
}
