using ICM.Crypto.Application.Interfaces;

namespace ICM.Crypto.Infrastructure.Persistence;

internal sealed class UnitOfWork : IUnitOfWork
{
    private readonly CryptoDbContext _dbContext;

    public UnitOfWork(CryptoDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public Task<int> SaveChangesAsync(CancellationToken cancellationToken)
    {
        return _dbContext.SaveChangesAsync(cancellationToken);
    }
}