namespace ICM.Crypto.Application.Interfaces;

public sealed record BlockchainSnapshotDto(
    string Source,
    string Coin,
    string Chain,
    string RawJson,
    DateTimeOffset CreatedAtUtc);