namespace ICM.Crypto.Infrastructure.BlockCypher;

internal sealed record BlockchainResponse(
    string Coin,
    string Chain,
    string RawJson);