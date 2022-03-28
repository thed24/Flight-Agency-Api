using FlightAgency.WebApi.Configs.ApiKeys;
using Google.Cloud.SecretManager.V1;
using LanguageExt;

public static class ProgramHelper
{
    public static IApiKeys GetApiKeys(string project)
    {
        try
        {
            var client = SecretManagerServiceClient.Create();
            var result = client.AccessSecretVersion(project);
            return new ApiKeys() { GoogleApiKey = result.Payload.Data.ToStringUtf8() };
        }
        catch (Exception ex)
        {
            return new ApiKeys();
        }
    }

    public static IResult MapToApiResponse<E, R>(this Either<E, R> either) => either.Match(
            Left: (error) => Results.BadRequest(error),
            Right: (result) => Results.Ok(result)
        );
}