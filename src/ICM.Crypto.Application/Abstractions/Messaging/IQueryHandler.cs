namespace ICM.Crypto.Application.Abstractions.Messaging;

/// <summary>
/// Represents a query handler that handles the <typeparamref name="TQuery"/> query
/// which generates a result of <typeparamref name="TResult"/> type.
/// </summary>
/// <typeparam name="TQuery">Query type.</typeparam>
/// <typeparam name="TResult">Query result type.</typeparam>
public interface IQueryHandler<in TQuery, TResult>
    where TQuery : IQuery<TResult>
{
    Task<TResult> HandleAsync(TQuery query, CancellationToken cancellationToken = default);
}