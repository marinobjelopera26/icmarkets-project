using System.Net;
using ICM.Crypto.Application.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Http.Resilience;
using Polly;

namespace ICM.Crypto.Infrastructure.BlockCypher;

public static class DependencyInjection
{
    public static IServiceCollection AddBlockCypher(this IServiceCollection services)
    {
        services.AddTransient<LogRequestDurationHandler>();

        services
            .AddHttpClient<IBlockCypherService, BlockCypherService>(
                configureClient: static client =>
                {
                    client.BaseAddress = new Uri("https://api.blockcypher.com/v1/");
                    client.Timeout = TimeSpan.FromSeconds(15);
                })
            .AddHttpMessageHandler<LogRequestDurationHandler>()
            .AddResilienceHandler(
                "BlockCypherPipeline",
                static builder =>
                {
                    builder.AddRetry(new HttpRetryStrategyOptions
                    {
                        BackoffType = DelayBackoffType.Exponential,
                        MaxRetryAttempts = 3,
                        Delay = TimeSpan.FromSeconds(1),
                        UseJitter = true,
                        ShouldHandle = args => 
                            args.Outcome switch
                            {  
                                { Exception: HttpRequestException } => PredicateResult.True(),
                                { Result.StatusCode: HttpStatusCode.TooManyRequests } => PredicateResult.False(),
                                { Result.StatusCode: HttpStatusCode.RequestTimeout } => PredicateResult.True(),
                                { Result.StatusCode: >= HttpStatusCode.InternalServerError } => PredicateResult.True(),
                                _ => PredicateResult.False()
                            }
                    });
                });

        services.AddSingleton<IBlockchainSnapshotProvider, BlockchainSnapshotProvider>();
        
        return services;
    }
}