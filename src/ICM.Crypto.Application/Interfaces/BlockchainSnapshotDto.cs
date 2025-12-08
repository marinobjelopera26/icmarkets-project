namespace ICM.Crypto.Application.Interfaces;

public record BlockchainSnapshotDto(
    string SourceUrl,
    string Coin,
    string Chain,
    int HttpStatusCode,
    long DurationMs,
    string RawJson);