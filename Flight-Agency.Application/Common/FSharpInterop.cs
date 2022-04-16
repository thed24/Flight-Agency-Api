using LanguageExt;
using Microsoft.Extensions.Logging;
using Microsoft.FSharp.Collections;
using Microsoft.FSharp.Core;

namespace FlightAgency.Application.Common;

public static class FSharpInterop
{
    public static Either<L, R> ToEither<L, R>(this FSharpResult<R, L> result)
        => result.IsOk
            ? Prelude.Right<L, R>(result.ResultValue)
            : Prelude.Left<L, R>(result.ErrorValue);

    public static FSharpList<T> ToFSharpList<T>(this IEnumerable<T> list)
        => ListModule.OfSeq(list);
}
