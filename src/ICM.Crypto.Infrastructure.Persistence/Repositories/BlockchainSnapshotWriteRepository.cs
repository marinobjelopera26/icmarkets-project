using ICM.Crypto.Application.Interfaces;
using ICM.Crypto.Domain;
using ICM.Crypto.Infrastructure.Persistence.Entities;

namespace ICM.Crypto.Infrastructure.Persistence.Repositories;

internal sealed class BlockchainSnapshotWriteRepository : IBlockchainSnapshotWriteRepository
{
    private readonly CryptoDbContext _dbContext;

    public BlockchainSnapshotWriteRepository(CryptoDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddAsync(BlockchainSnapshot snapshot, CancellationToken cancellationToken)
    {
        var entity = new BlockchainSnapshotEntity
        {
            Id = snapshot.Id.Value,
            Blockchain = snapshot.Blockchain.ToString(),
            Source = snapshot.Source.Value,
            RawJson = snapshot.RawJson.Value,
            CreatedAt = snapshot.CreatedAtUtc,
        };

        await _dbContext
            .Set<BlockchainSnapshotEntity>()
            .AddAsync(entity, cancellationToken);
    }
}