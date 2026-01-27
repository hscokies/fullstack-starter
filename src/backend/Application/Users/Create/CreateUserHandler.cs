using Application.Common;
using Infrastructure.Persistence.Entities;
using Microsoft.AspNetCore.Identity;

namespace Application.Users.Create;

public class CreateUserHandler(UserManager<User> userManager) : ICommandHandler<CreateUserCommand, Guid>
{

    public async Task<Result<Guid>> Handle(CreateUserCommand command, CancellationToken ct)
    {
        var user = new User
        {
            Id = Guid.CreateVersion7(),
            Email = command.Email,
        };

        var result = await userManager.CreateAsync(user, command.Password);
        if (result.Succeeded)
        {
            return user.Id;
        }

        var identityErrors = result.Errors.Select(x => new Error(x.Code, x.Description, ErrorType.Validation));
        return new ValidationError(identityErrors);
    }
}
