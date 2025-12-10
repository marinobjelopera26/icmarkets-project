using ICM.Crypto.Domain;

namespace ICM.Crypto.Application.Interfaces;

public interface IBlockchainSnapshotWriteRepository
{
    /// <summary>
    /// Adds a single <see cref="BlockchainSnapshot"/> to the database context.
    /// </summary>
    /// <param name="snapshot"><see cref="BlockchainSnapshot"/> aggregate.</param>
    /// <param name="cancellationToken"><see cref="CancellationToken"/>.</param>
    Task AddAsync(BlockchainSnapshot snapshot, CancellationToken cancellationToken);
}