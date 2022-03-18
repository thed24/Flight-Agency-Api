using Microsoft.AspNetCore.Mvc;
using Flight_Agency_Infrastructure.Common;
using Google.Cloud.SecretManager.V1;
using GoogleMapsApi.Entities.PlacesNearBy.Response;
using GoogleMapsApi;
using GoogleMapsApi.Entities.PlacesNearBy.Request;

namespace Flight_Agency_Api;

[ApiController]
public class Places : ControllerBase
{
    private string key;
    public Places()
    {
        var client = SecretManagerServiceClient.Create();
        var result = client.AccessSecretVersion("projects/620313617886/secrets/google-api-key");
        key = result.Payload.Data.ToStringUtf8();
    }

    [HttpGet(Name = "GetPlaces")]
    [Route("places/nearBy")]
    public async Task<IActionResult> GetPlaces(
        [FromBody] PlacesNearByRequest placesNearByRequest)
    {
        placesNearByRequest.ApiKey = key;
        PlacesNearByResponse placesNearBy = await GoogleMaps.PlacesNearBy.QueryAsync(placesNearByRequest);
        return new OkObjectResult(placesNearBy);
    }
}
