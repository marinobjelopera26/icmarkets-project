using Microsoft.Extensions.DependencyInjection;

namespace ICM.Crypto.SharedKernel.Mediator;

internal sealed class Sender(IServiceProvider serviceProvider) : ISender
{
    public Task<TResponse> Send<TResponse>(IQuery<TResponse> query, CancellationToken cancellationToken)
    {
        var handlerType = typeof(IQueryHandler<,>).MakeGenericType(query.GetType(), typeof(TResponse));
        dynamic handler = serviceProvider.GetRequiredService(handlerType);
        return handler.Handle((dynamic)query, cancellationToken);
    }

    public Task<TResponse> Send<TResponse>(ICommand command, CancellationToken cancellationToken)
    {
        var handlerType = typeof(ICommandHandler<>).MakeGenericType(command.GetType());
        dynamic handler = serviceProvider.GetRequiredService(handlerType);
        return handler.Handle((dynamic)command, cancellationToken);
    }

    public Task<TResponse> Send<TResponse>(ICommand<TResponse> command, CancellationToken cancellationToken)
    {
        var handlerType = typeof(ICommandHandler<,>).MakeGenericType(command.GetType(), typeof(TResponse));
        dynamic handler = serviceProvider.GetRequiredService(handlerType);
        return handler.Handle((dynamic)command, cancellationToken);
    }
}