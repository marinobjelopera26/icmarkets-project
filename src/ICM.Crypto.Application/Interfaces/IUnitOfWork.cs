namespace ICM.Crypto.Application.Interfaces;

public interface IUnitOfWork
{
    /// <summary>
    /// Persists the pending changes to the backing database.
    /// </summary>
    /// <param name="cancellationToken"><see cref="CancellationToken"/>.</param>
    /// <returns>The count of modified rows.</returns>
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}