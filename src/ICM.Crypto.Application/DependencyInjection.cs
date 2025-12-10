using ICM.Crypto.Application.Abstractions.Messaging;
using Microsoft.Extensions.DependencyInjection;

namespace ICM.Crypto.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        // Automatically registers all query and command handler implementations from this assembly
        services.Scan(scan =>
            scan.FromAssemblies(typeof(DependencyInjection).Assembly)
                .AddClasses(classes => classes.AssignableTo(typeof(IQueryHandler<,>)), publicOnly: false)
                    .AsImplementedInterfaces()
                    .WithTransientLifetime()
                .AddClasses(classes => classes.AssignableTo(typeof(ICommandHandler<>)), publicOnly: false)
                    .AsImplementedInterfaces()
                    .WithTransientLifetime()
                .AddClasses(classes => classes.AssignableTo(typeof(ICommandHandler<,>)), publicOnly: false)
                    .AsImplementedInterfaces()
                    .WithTransientLifetime());
        
        return services;
    }
}