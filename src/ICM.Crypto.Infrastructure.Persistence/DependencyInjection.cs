using Microsoft.Extensions.DependencyInjection;

namespace ICM.Crypto.Infrastructure.Persistence;

public static class DependencyInjection
{
    public static IServiceCollection AddPersistence(this IServiceCollection services)
    {
        return services;
    }
}