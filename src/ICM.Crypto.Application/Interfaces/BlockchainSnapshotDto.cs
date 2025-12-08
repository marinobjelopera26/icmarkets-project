namespace ICM.Crypto.Application.Interfaces;

public record BlockchainSnapshotDto(
    string Source,
    string Coin,
    string Chain,
    int HttpStatusCode,
    string RawJson);