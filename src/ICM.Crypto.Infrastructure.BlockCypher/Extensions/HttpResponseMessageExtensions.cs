namespace ICM.Crypto.Infrastructure.BlockCypher.Extensions;

internal static class HttpResponseMessageExtensions
{
    public static long? GetRequestDurationMs(this HttpResponseMessage response)
    {
        if (!response.Headers.TryGetValues("X-Client-Duration-Ms", out var headerValues)) 
            return null;
        
        var headerStringValue = headerValues.FirstOrDefault();
        return long.TryParse(headerStringValue, out var durationMs)
            ? durationMs
            : null;
    }
}