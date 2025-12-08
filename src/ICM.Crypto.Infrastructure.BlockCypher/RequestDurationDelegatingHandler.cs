using System.Diagnostics;
using Microsoft.Extensions.Logging;

namespace ICM.Crypto.Infrastructure.BlockCypher;

public class RequestDurationDelegatingHandler : DelegatingHandler
{
    private readonly ILogger _logger;

    public RequestDurationDelegatingHandler(ILogger<RequestDurationDelegatingHandler> logger)
    {
        _logger = logger;
    }
    
    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var stopwatch = Stopwatch.StartNew();
        var httpResponseMessage = await base.SendAsync(request, cancellationToken);
        stopwatch.Stop();

        var requestDuration = stopwatch.ElapsedMilliseconds;
        
        httpResponseMessage.Headers.Add("X-Client-Duration-Ms", requestDuration.ToString());
        
        _logger.LogInformation("HTTP request to '{RequestUrl}' finished after {RequestDuration}ms.",
            httpResponseMessage.RequestMessage!.RequestUri,
            requestDuration);
        
        return httpResponseMessage;
    }
}