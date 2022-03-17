namespace Flight_Agency_Domain
{
    public record LoginRequest(
        string Password,
        string Email
    );
}