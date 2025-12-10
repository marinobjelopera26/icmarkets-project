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

    /// <summary>
    /// Retrieves the latest snapshots for multiple <paramref name="blockchains"/>.
    /// </summary>
    /// <param name="blockchains">Collection of blockchains to retrieve the snapshots for.</param>
    /// <param name="cancellationToken"><see cref="CancellationToken"/>.</param>
    Task<IReadOnlyCollection<BlockchainSnapshotDto>> GetSnapshotsAsync(
        IEnumerable<Blockchain> blockchains, CancellationToken cancellationToken = default);
}