namespace ICM.Crypto.Infrastructure.BlockCypher;

internal sealed record BlockchainResponse(
    string Coin,
    string Chain,
    int HttpStatusCode,
    string RawJson);