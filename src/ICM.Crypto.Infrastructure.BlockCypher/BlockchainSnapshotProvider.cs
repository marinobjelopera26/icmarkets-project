using ICM.Crypto.Application.Interfaces;
using ICM.Crypto.Domain.ValueObjects;

namespace ICM.Crypto.Infrastructure.BlockCypher;

internal sealed class BlockchainSnapshotProvider : IBlockchainSnapshotProvider
{
    private readonly IBlockCypherService _blockCypherService;

    public BlockchainSnapshotProvider(IBlockCypherService blockCypherService)
    {
        _blockCypherService = blockCypherService;
    }
    
    public async Task<BlockchainSnapshotDto> GetSnapshotAsync(
        Blockchain blockchain, CancellationToken cancellationToken = default)
    {
        var request = new GetBlockchainRequestDto(
            blockchain.Coin.ToString("G"),
            blockchain.Chain.ToString("G"));
        
        var response = await _blockCypherService.GetBlockchainAsync(request, cancellationToken);

        return new BlockchainSnapshotDto(
            Source: Constants.BlockCypher,
            response.Coin,
            response.Chain,
            response.RawJson);
    }

    public async Task<IReadOnlyCollection<BlockchainSnapshotDto>> GetSnapshotsAsync(
        IEnumerable<Blockchain> blockchains, CancellationToken cancellationToken = default)
    {
        var tasks = blockchains
            .Select(bc => GetSnapshotAsync(bc, cancellationToken));

        var responses = await Task.WhenAll(tasks);
        
        return responses
            .Select(r => 
                new BlockchainSnapshotDto(
                    r.Source, 
                    r.Coin, 
                    r.Chain, 
                    r.RawJson))
            .ToList()
            .AsReadOnly();
    }
}