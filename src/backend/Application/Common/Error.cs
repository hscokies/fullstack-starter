using Microsoft.AspNetCore.Identity;

namespace Application.Common;

public record Error(string Code, string Description, ErrorType Type)
{
    public static readonly Error None = new(string.Empty, string.Empty, ErrorType.Failure);

    public static Error Failure(string code, string description)
    {
        return new Error(code, description, ErrorType.Failure);
    }

    public static Error NotFound(string code, string description)
    {
        return new Error(code, description, ErrorType.NotFound);
    }

    public static Error Problem(string code, string description)
    {
        return new Error(code, description, ErrorType.Problem);
    }

    public static Error Conflict(string code, string description)
    {
        return new Error(code, description, ErrorType.Conflict);
    }
}

public record ValidationError(IEnumerable<Error> Errors) : Error("Validation.General", "One or more validation errors occurred", ErrorType.Validation);

public enum ErrorType
{
    Failure = 0,
    Validation = 1,
    Problem = 2,
    NotFound = 3,
    Conflict = 4,
}
