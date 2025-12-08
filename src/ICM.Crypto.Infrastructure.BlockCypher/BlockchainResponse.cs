namespace ICM.Crypto.Infrastructure.BlockCypher;

internal sealed record BlockchainResponse(
    string SourceUrl,
    string Coin,
    string Chain,
    int HttpStatusCode,
    long DurationMs,
    string RawJson);