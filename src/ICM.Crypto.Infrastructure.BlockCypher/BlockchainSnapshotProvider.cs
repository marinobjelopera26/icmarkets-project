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
        var response = await _blockCypherService.GetBlockchainAsync(descriptor, cancellationToken);

        return new BlockchainSnapshotDto(
            Source: Constants.BlockCypher,
            response.Coin,
            response.Chain,
            response.RawJson);
    }

    public async Task<IReadOnlyList<BlockchainSnapshotDto>> GetSnapshotsAsync(
        IEnumerable<BlockchainDescriptor> descriptors, CancellationToken cancellationToken = default)
    {
        var tasks = descriptors
            .Select(c => _blockCypherService.GetBlockchainAsync(c, cancellationToken));

        var results = await Task.WhenAll(tasks);

        return results
            .Select(r => 
                new BlockchainSnapshotDto(
                    Source: Constants.BlockCypher,
                    r.Coin,
                    r.Chain,
                    r.RawJson))
            .ToArray();
    }
}