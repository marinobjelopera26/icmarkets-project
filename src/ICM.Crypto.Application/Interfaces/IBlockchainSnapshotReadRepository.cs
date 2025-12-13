using ICM.Crypto.Domain;
using ICM.Crypto.Domain.ValueObjects;

namespace ICM.Crypto.Application.Interfaces;

public interface IBlockchainSnapshotReadRepository
{
    /// <summary>
    /// Retrieves the historic blockchain data from the backing store for the provided
    /// <paramref name="blockchain"/>. Use <paramref name="page"/> and <paramref name="pageSize"/>
    /// to limit the number of returned results. Data is ordered by creation date, descending.
    /// </summary>
    /// <param name="blockchain"><see cref="Blockchain"/>.</param>
    /// <param name="page">Page number.</param>
    /// <param name="pageSize">Page size.</param>
    /// <param name="cancellationToken"><see cref="CancellationToken"/>.</param>
    Task<IEnumerable<BlockchainSnapshot>> GetHistoryAsync(
        Blockchain blockchain,
        int page,
        int pageSize,
        CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Retrieves the total count of snapshots in the backing store for the
    /// provided <paramref name="blockchain"/>.
    /// </summary>
    /// <param name="blockchain"><see cref="Blockchain"/>.</param>
    /// <param name="cancellationToken"><see cref="CancellationToken"/>.</param>
    Task<int> GetTotalCountAsync(Blockchain blockchain, CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves the latest stored snapshot from the backing store for the
    /// provided <paramref name="blockchain"/>.
    /// </summary>
    /// <param name="blockchain"><see cref="Blockchain"/>,</param>
    /// <param name="cancellationToken"><see cref="CancellationToken"/>.</param>
    Task<BlockchainSnapshot> GetLatestSnapshotAsync(Blockchain blockchain, CancellationToken cancellationToken);
}