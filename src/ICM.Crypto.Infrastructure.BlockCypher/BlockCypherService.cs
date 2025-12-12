namespace ICM.Crypto.Infrastructure.BlockCypher;

internal sealed class BlockCypherService : IBlockCypherService
{
    private readonly HttpClient _httpClient;

    public BlockCypherService(HttpClient httpHttpClient)
    {
        _httpClient = httpHttpClient;
    }
    
    public async Task<BlockchainResponse> GetBlockchainAsync(
        GetBlockchainRequestDto request, CancellationToken cancellationToken = default)
    {
        using var response = await _httpClient.GetAsync(request.RequestPath, HttpCompletionOption.ResponseContentRead, cancellationToken);
        response.EnsureSuccessStatusCode();
        
        var rawResponseBody = await response.Content.ReadAsStringAsync(cancellationToken);
        
        return new BlockchainResponse(
            request.Coin,
            request.Chain,
            RawJson: rawResponseBody);
    }
}