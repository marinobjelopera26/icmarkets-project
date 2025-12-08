using System.Diagnostics;
using Microsoft.Extensions.Logging;

namespace ICM.Crypto.Infrastructure.BlockCypher;

public class LogRequestDurationHandler : DelegatingHandler
{
    private readonly ILogger _logger;

    public LogRequestDurationHandler(ILogger<LogRequestDurationHandler> logger)
    {
        _logger = logger;
    }
    
    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var stopwatch = Stopwatch.StartNew();
        var httpResponseMessage = await base.SendAsync(request, cancellationToken);
        stopwatch.Stop();

        var requestDuration = stopwatch.ElapsedMilliseconds;
        
        _logger.LogInformation("HTTP {Method} '{RequestUrl}' finished after {RequestDuration}ms.",
            request.Method.Method,
            request.RequestUri,
            requestDuration);
        
        return httpResponseMessage;
    }
}