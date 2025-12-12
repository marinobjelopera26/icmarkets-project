
using ICM.Crypto.Application.Features.DataIngestion;
using ICM.Crypto.Infrastructure.HostedServices.Options;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace ICM.Crypto.Infrastructure.HostedServices.DataIngestion;

public class BlockchainSnapshotDataIngestionService : BackgroundService
{
    private const int MinimumPollingIntervalInSeconds = 30;

    private readonly IServiceScopeFactory _scopeFactory;
    private readonly DataIngestionOptions _options;
    private readonly ILogger _logger;

    private volatile int _isRunning;

    public BlockchainSnapshotDataIngestionService(
        IServiceScopeFactory scopeFactory,
        IOptions<DataIngestionOptions> options,
        ILogger<BlockchainSnapshotDataIngestionService> logger)
    {
        _scopeFactory = scopeFactory;
        _options = options.Value;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        if (!_options.Enabled)
        {
            _logger.LogInformation($"{nameof(BlockchainSnapshotDataIngestionService)} service is disabled via configuration.");
            return;
        }

        var pollingIntervalInSecs = TimeSpan.FromSeconds(
            Math.Max(MinimumPollingIntervalInSeconds, _options.PollingIntervalInSeconds));

        var timer = new PeriodicTimer(pollingIntervalInSecs);
        _logger.LogInformation("Background blockchain snapshot ingestion has started.");

        await RunDataIngestionCycleAsync(stoppingToken);

        while (await timer.WaitForNextTickAsync(stoppingToken))
        {
            await RunDataIngestionCycleAsync(stoppingToken);
        }
    }

    private async Task RunDataIngestionCycleAsync(CancellationToken cancellationToken)
    {
        if (Interlocked.Exchange(ref _isRunning, 1) == 1)
        {
            _logger.LogWarning("Previous data ingestion cycle is still in progress; skipping this tick.");
            return;
        }

        try
        {
            await IngestDataOnceAsync(cancellationToken);
        }
        catch (OperationCanceledException) when (cancellationToken.IsCancellationRequested)
        {
            _logger.LogInformation("Current data ingestion cycle was canceled.");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unhandled exception occurred during data ingestion cycle.");
            throw;
        }
        finally
        {
            Interlocked.Exchange(ref _isRunning, 0);
        }
    }

    private async Task IngestDataOnceAsync(CancellationToken cancellationToken)
    {
        var startUtc = DateTime.UtcNow;
        _logger.LogInformation("Starting new data ingestion cycle at {Start:O}.", startUtc);
        
        using var scope = _scopeFactory.CreateScope();
        var mediator = scope.ServiceProvider.GetRequiredService<ISender>();
        
        var command = new IngestSnapshotsCommand(IngestionFeeds.All);
        await mediator.Send(command, cancellationToken);
    }
}