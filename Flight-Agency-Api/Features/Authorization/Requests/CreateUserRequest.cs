namespace Flight_Agency_Domain
{
    public record CreateUserRequest(
        string Password,
        string Name,
        string Email
    );
}