using Application.Users.Create;
using SharpGrip.FluentValidation.AutoValidation.Endpoints.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.Users;

internal sealed class Create : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost(CreateUserCommand.Path,
                async (CreateUserCommand request, CreateUserHandler handler, CancellationToken ct) =>
                {
                    var result = await handler.Handle(request, ct);

                    return result.Match(
                        (val) => Results.Created(CreateUserCommand.Path, val),
                        CustomResults.Problem);
                })
            .WithTags(Tags.Users)
            .AddFluentValidationAutoValidation();
    }
}
