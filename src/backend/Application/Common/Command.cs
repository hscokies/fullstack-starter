namespace Application.Common;

public interface ICommand;

public interface ICommand<TResponse> : ICommand;

public interface IBaseCommandHandler;

public interface ICommandHandler<in TCommand> : IBaseCommandHandler
    where TCommand : ICommand
{
    Task<Result> Handle(TCommand command, CancellationToken ct);
}

public interface ICommandHandler<in TCommand, TResponse> : IBaseCommandHandler
    where TCommand : ICommand<TResponse>
{
    Task<Result<TResponse>> Handle(TCommand command, CancellationToken ct);
}