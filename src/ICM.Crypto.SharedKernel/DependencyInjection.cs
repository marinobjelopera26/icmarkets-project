using System.Reflection;
using ICM.Crypto.SharedKernel.Mediator;
using Microsoft.Extensions.DependencyInjection;

namespace ICM.Crypto.SharedKernel;

public static class DependencyInjection
{
    public static IServiceCollection AddMediator(this IServiceCollection services, Assembly? assembly = null)
    {
        assembly ??= Assembly.GetCallingAssembly();

        services.AddTransient<ISender, Sender>();

        var allHandlerTypes = GetHandlerTypes(typeof(IQueryHandler<,>))
            .Concat(GetHandlerTypes(typeof(ICommandHandler<>)))
            .Concat(GetHandlerTypes(typeof(ICommandHandler<,>)));
        
        foreach (var queryHandleType in allHandlerTypes)
            services.AddTransient(queryHandleType.Interface, queryHandleType.Implementation);

        return services;

        IEnumerable<(Type Interface, Type Implementation)> GetHandlerTypes(Type genericHandlerType)
        {
            return assembly
                .GetTypes()
                .Where(type => type is { IsAbstract: false, IsInterface: false })
                .SelectMany(type => type.GetInterfaces()
                    .Where(i => i.IsGenericType && i.GetGenericTypeDefinition() == genericHandlerType)
                    .Select(i => (i, type)));
        }
    }
}