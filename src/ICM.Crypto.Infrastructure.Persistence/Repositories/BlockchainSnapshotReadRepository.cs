using ICM.Crypto.Application.Interfaces;
using ICM.Crypto.Domain;
using ICM.Crypto.Domain.ValueObjects;
using ICM.Crypto.Infrastructure.Persistence.Entities;
using Microsoft.EntityFrameworkCore;

namespace ICM.Crypto.Infrastructure.Persistence.Repositories;

internal sealed class BlockchainSnapshotReadRepository : IBlockchainSnapshotReadRepository
{
    private readonly CryptoDbContext _dbContext;

    public BlockchainSnapshotReadRepository(CryptoDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<BlockchainSnapshot>> GetHistoryAsync(
        Blockchain blockchain,
        int page,
        int pageSize,
        CancellationToken cancellationToken = default)
    {
        var skip = (page - 1) * pageSize;
        var query = _dbContext.Set<BlockchainSnapshotEntity>()
            .AsNoTracking()
            .Where(s => s.Blockchain == blockchain.ToString())
            .OrderByDescending(s => s.CreatedAt)
            .Skip(skip)
            .Take(pageSize);

        var entities = await query.ToListAsync(cancellationToken);

        return entities
            .Select(entity =>
                BlockchainSnapshot.FromExisting(
                    SnapshotId.FromExisting(entity.Id),
                    blockchain,
                    Source.Create(entity.Source),
                    RawJson.Create(entity.RawJson),
                    entity.CreatedAt.UtcDateTime));
    }

    public Task<int> GetTotalCountAsync(Blockchain blockchain, CancellationToken cancellationToken = default)
    {
        return _dbContext
            .Set<BlockchainSnapshotEntity>()
            .Where(s => s.Blockchain == blockchain.ToString())
            .CountAsync(cancellationToken);
    }

    public async Task<BlockchainSnapshot> GetLatestSnapshotAsync(Blockchain blockchain,
        CancellationToken cancellationToken)
    {
        var entity = await _dbContext
            .Set<BlockchainSnapshotEntity>()
            .OrderByDescending(s => s.CreatedAt)
            .FirstAsync(cancellationToken);
        
        return BlockchainSnapshot.FromExisting(
            SnapshotId.FromExisting(entity.Id),
            blockchain,
            Source.Create(entity.Source),
            RawJson.Create(entity.RawJson),
            entity.CreatedAt.UtcDateTime);
    }
}