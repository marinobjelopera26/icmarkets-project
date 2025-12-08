using Microsoft.EntityFrameworkCore;

namespace ICM.Crypto.Infrastructure.Persistence;

public sealed class CryptoDbContext : DbContext
{
    public CryptoDbContext(DbContextOptions<CryptoDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(CryptoDbContext).Assembly);
    }
}