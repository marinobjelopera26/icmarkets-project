using ICM.Crypto.Application.Interfaces;

namespace ICM.Crypto.Infrastructure.BlockCypher;

internal sealed class BlockchainSnapshotProvider : IBlockchainSnapshotProvider
{
    private readonly IBlockCypherService _blockCypherService;

    public BlockchainSnapshotProvider(IBlockCypherService blockCypherService)
    {
        _blockCypherService = blockCypherService;
    }
    
    public async Task<BlockchainSnapshotDto> GetSnapshotAsync(BlockchainDescriptor descriptor, CancellationToken cancellationToken = default)
    {
        var response = await _blockCypherService.GetBlockchainInfoAsync(descriptor, cancellationToken);

        return new BlockchainSnapshotDto(
            response.SourceUrl,
            response.Coin,
            response.Chain,
            response.HttpStatusCode,
            response.DurationMs,
            response.RawJson);
    }

    public async Task<IReadOnlyList<BlockchainSnapshotDto>> GetSnapshotsAsync(
        IEnumerable<BlockchainDescriptor> descriptors, CancellationToken cancellationToken = default)
    {
        var tasks = descriptors
            .Select(c => _blockCypherService.GetBlockchainInfoAsync(c, cancellationToken));

        var results = await Task.WhenAll(tasks);

        return results
            .Select(r => 
                new BlockchainSnapshotDto(
                    r.SourceUrl,
                    r.Coin,
                    r.Chain,
                    r.HttpStatusCode,
                    r.DurationMs,
                    r.RawJson))
            .ToArray();
    }
}