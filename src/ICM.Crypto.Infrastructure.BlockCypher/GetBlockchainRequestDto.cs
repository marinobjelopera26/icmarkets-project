namespace ICM.Crypto.Infrastructure.BlockCypher;

internal record GetBlockchainRequestDto(string Coin, string Chain)
{
    public string RequestPath =>
        $"{Coin.ToLowerInvariant()}/{Chain.ToLowerInvariant()}";
}