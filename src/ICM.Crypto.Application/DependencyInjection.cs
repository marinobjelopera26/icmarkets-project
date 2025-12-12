using Microsoft.Extensions.DependencyInjection;
﻿using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace ICM.Crypto.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddValidatorsFromAssembly(typeof(DependencyInjection).Assembly, includeInternalTypes: true);
        
        services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly);
        });
        
        return services;
    }
}