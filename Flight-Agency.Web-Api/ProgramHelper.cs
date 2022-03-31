using LanguageExt;

public static class ProgramHelper
{
    public static IResult MapToApiResponse<E, R>(this Either<E, R> either) => either.Match(
            Left: (error) => Results.BadRequest(error),
            Right: (result) => Results.Ok(result)
        );
}