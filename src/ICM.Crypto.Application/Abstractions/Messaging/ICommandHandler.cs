namespace ICM.Crypto.Application.Abstractions.Messaging;

/// <summary>
/// Represents a command handler for a <typeparamref name="TCommand"/> command that
/// does not generator a result.
/// </summary>
/// <typeparam name="TCommand">Command type.</typeparam>
public interface ICommandHandler<in TCommand>
    where TCommand : ICommand
{
    Task HandleAsync(TCommand command, CancellationToken cancellationToken = default);
}

/// <summary>
/// Represents a command handler for a <typeparamref name="TCommand"/> command that
/// generates a result of <typeparamref name="TResult"/> type.
/// </summary>
/// <typeparam name="TCommand">Command type.</typeparam>
/// <typeparam name="TResult">Command result type.</typeparam>
public interface ICommandHandler<in TCommand, TResult>
    where TCommand : ICommand<TResult>
{
    Task<TResult> HandleAsync(TCommand command, CancellationToken cancellationToken = default);
}