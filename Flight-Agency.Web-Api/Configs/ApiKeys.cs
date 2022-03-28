namespace FlightAgency.WebApi.Configs.ApiKeys;

public interface IApiKeys
{
    string GoogleApiKey { get; }
}
public class ApiKeys : IApiKeys
{
    public string GoogleApiKey { get; set; } = Environment.GetEnvironmentVariable("GOOGLE_API_KEY") ?? "";
}