using ICM.Crypto.Application.Interfaces;
using ICM.Crypto.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ICM.Crypto.Infrastructure.Persistence;

public static class DependencyInjection
{
    public static IServiceCollection AddPersistence(
        this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("Default");

        services.AddDbContext<CryptoDbContext>(options =>
        {
            options.UseNpgsql(connectionString, npgsql =>
            {
                npgsql.MigrationsAssembly(typeof(CryptoDbContext).Assembly.FullName);
            });
        }, contextLifetime: ServiceLifetime.Scoped);

        services.AddScoped<IBlockchainSnapshotWriteRepository, BlockchainSnapshotWriteRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        return services;
    }
}