namespace ICM.Crypto.Application.Interfaces;

public interface IBlockchainSnapshotProvider
{
    /// <summary>
    /// Retrieves the latest snapshot of a single blockchain.
    /// </summary>
    /// <param name="descriptor">Blockchain type descriptor.</param>
    /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
    Task<BlockchainSnapshotDto> GetSnapshotAsync(
        BlockchainDescriptor descriptor, CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Retrieves latest snapshots of all specified blockchains described by the
    /// <paramref name="descriptors"/> param.
    /// </summary>
    /// <param name="descriptors">Blockchain type descriptors.</param>
    /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
    Task<IReadOnlyList<BlockchainSnapshotDto>> GetSnapshotsAsync(
        IEnumerable<BlockchainDescriptor> descriptors, CancellationToken cancellationToken = default);
}