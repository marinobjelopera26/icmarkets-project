using Microsoft.Extensions.DependencyInjection;

namespace ICM.Crypto.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        return services;
    }
}