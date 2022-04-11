namespace FlightAgency.Application.Features.Places.Requests;

public record GetPlacesNearbyRequest
(
    double Lat, double Lng, int Radius, string Keyword, int Zoom
);
