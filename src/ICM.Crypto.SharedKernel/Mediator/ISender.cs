namespace ICM.Crypto.SharedKernel.Mediator;

public interface ISender
{
    Task<TResponse> Send<TResponse>(IQuery<TResponse> query, CancellationToken cancellationToken);
    Task<TResponse> Send<TResponse>(ICommand command, CancellationToken cancellationToken);
    Task<TResponse> Send<TResponse>(ICommand<TResponse> command, CancellationToken cancellationToken);
}