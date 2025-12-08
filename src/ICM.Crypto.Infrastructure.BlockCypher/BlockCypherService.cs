using ICM.Crypto.Application.Interfaces;

namespace ICM.Crypto.Infrastructure.BlockCypher;

internal sealed class BlockCypherService : IBlockCypherService
{
    private readonly HttpClient _httpClient;

    public BlockCypherService(HttpClient httpHttpClient)
    {
        _httpClient = httpHttpClient;
    }
    
    public async Task<BlockchainResponse> GetBlockchainAsync(BlockchainDescriptor descriptor, 
        CancellationToken cancellationToken = default)
    {
        var requestPath = CreateRequestPath();

        using var response = await _httpClient.GetAsync(requestPath, HttpCompletionOption.ResponseContentRead, cancellationToken);
        var rawResponseBody = await response.Content.ReadAsStringAsync(cancellationToken);
        
        return new BlockchainResponse(
            SourceUrl: new Uri(_httpClient.BaseAddress!, requestPath).ToString(),
            descriptor.Coin.ToString("G"),
            descriptor.Chain.ToString("G"),
            HttpStatusCode: (int)response.StatusCode,
            RawJson: rawResponseBody);

        string CreateRequestPath()
        {
            return descriptor.Coin.ToString().ToLowerInvariant() + 
                   '/' + 
                   descriptor.Chain.ToString().ToLowerInvariant();
        }
    }
}