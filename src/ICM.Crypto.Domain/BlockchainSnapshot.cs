using ICM.Crypto.Domain.ValueObjects;

namespace ICM.Crypto.Domain;

public sealed class BlockchainSnapshot
{
    #region Factory Method
    
    public static BlockchainSnapshot Create(
        Blockchain blockchain,
        SourceUrl sourceUrl,
        HttpStatus httpStatus,
        long durationMs,
        RawJson rawJson,
        SnapshotId? id = null)
        => new(
            id ?? SnapshotId.CreateNew(),
            blockchain,
            sourceUrl,
            httpStatus,
            durationMs,
            rawJson,
            createdAtUtc: DateTime.UtcNow);

    #endregion
    
    #region Constructor

    private BlockchainSnapshot(
        SnapshotId id,
        Blockchain blockchain,
        SourceUrl sourceUrl,
        HttpStatus httpStatus,
        long durationMs,
        RawJson rawJson,
        DateTime createdAtUtc)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(durationMs, nameof(durationMs));
        if (createdAtUtc.Kind != DateTimeKind.Utc)
            throw new ArgumentException("CreatedAtUtc must be a UTC date-time.", nameof(createdAtUtc));

        Id = id;
        Blockchain = blockchain;
        SourceUrl = sourceUrl;
        HttpStatus = httpStatus;
        DurationMs = durationMs;
        RawJson = rawJson;
        CreatedAtUtc = createdAtUtc;
    }
    
    #endregion
    
    #region Properties
    
    public SnapshotId Id { get; }
    public Blockchain Blockchain { get; }
    public SourceUrl SourceUrl { get; }
    public HttpStatus HttpStatus { get; }
    public long DurationMs { get; }
    public RawJson RawJson { get; }
    public DateTime CreatedAtUtc { get; }
    
    #endregion
}