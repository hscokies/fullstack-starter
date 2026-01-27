using Application.Common;

namespace Application.Users.Create;

public record CreateUserCommand(string Email, string Password) : ICommand<Guid>
{
    public static string Path { get; } = "/users/create";
}
