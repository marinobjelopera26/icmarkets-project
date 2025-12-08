namespace ICM.Crypto.Application.Interfaces;

public interface IBlockchainSnapshotProvider
{
    /// <summary>
    /// Retrieves a snapshot of a single blockchain.
    /// </summary>
    /// <param name="descriptor">Blockchain type descriptor.</param>
    /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
    Task<BlockchainSnapshotDto> GetSnapshotAsync(
        BlockchainDescriptor descriptor, CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Retrieves snapshots of all specified blockchains described by the
    /// <paramref name="descriptors"/> param.
    /// </summary>
    /// <param name="descriptors">Blockain type descriptors.</param>
    /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
    Task<IReadOnlyList<BlockchainSnapshotDto>> GetSnapshotsAsync(
        IEnumerable<BlockchainDescriptor> descriptors, CancellationToken cancellationToken = default);
}