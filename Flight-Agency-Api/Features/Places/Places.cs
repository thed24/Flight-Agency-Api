using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Flight_Agency_Api.Features.Authorization.Services;
using Flight_Agency_Infrastructure.Common;
using Flight_Agency_Domain;
using GoogleMapsApi.Entities.PlacesNearBy.Request;
using GoogleMapsApi;
using GoogleMapsApi.Entities.PlacesNearBy.Response;

namespace Flight_Agency_Api;

public class Places
{
    [FunctionName("PlacesNearBy")]
    public async Task<IActionResult> GetPlaces(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "places/nearBy")]
        HttpRequest req,
        ILogger log)
    {
        var placesNearByRequest = Mapper.MapQueryParamsToObject<PlacesNearByRequest>(req.Query);
        if (placesNearByRequest is null) return new BadRequestResult();

        placesNearByRequest.ApiKey = "";
        PlacesNearByResponse placesNearBy = await GoogleMaps.PlacesNearBy.QueryAsync(placesNearByRequest);
        return new OkObjectResult(placesNearBy);
    }
}
