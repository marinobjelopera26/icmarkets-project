using ICM.Crypto.SharedKernel;
using Microsoft.Extensions.DependencyInjection;

namespace ICM.Crypto.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediator(typeof(DependencyInjection).Assembly);
        
        return services;
    }
}