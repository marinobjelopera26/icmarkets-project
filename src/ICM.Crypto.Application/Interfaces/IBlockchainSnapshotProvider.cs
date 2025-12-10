using ICM.Crypto.Domain.ValueObjects;

namespace ICM.Crypto.Application.Interfaces;

public interface IBlockchainSnapshotProvider
{
    /// <summary>
    /// Retrieves the latest snapshot of a single blockchain.
    /// </summary>
    /// <param name="blockchain">Blockchain type.</param>
    /// <param name="cancellationToken"><see cref="CancellationToken"/>.</param>
    Task<BlockchainSnapshotDto> GetSnapshotAsync(
        Blockchain blockchain, CancellationToken cancellationToken = default);
}