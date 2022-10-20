using FlightAgency.Contracts.Responses;
using LanguageExt;
using Microsoft.AspNetCore.Mvc;

namespace FlightAgency.WebApi.Common;

public static class ProgramHelper
{
    public static IActionResult MapToApiResponse<R>(this Either<string, R> either)
    {
        return either.Match<IActionResult>(
            Left: error => new BadRequestObjectResult(new ErrorResponse(error)),
            Right: result => new OkObjectResult(result)
        );
    }
}