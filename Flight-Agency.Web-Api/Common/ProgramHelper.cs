using LanguageExt;
using Microsoft.AspNetCore.Mvc;

namespace FlightAgency.WebApi.Common;

public static class ProgramHelper
{
    public static IActionResult MapToApiResponse<E, R>(this Either<E, R> either) => either.Match<IActionResult>(
            Left: (error) => new BadRequestObjectResult(error),
            Right: (result) => new OkObjectResult(result)
        );
}