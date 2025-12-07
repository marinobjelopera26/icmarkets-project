using ICM.Crypto.Domain.ValueObjects;

namespace ICM.Crypto.Domain;

public sealed class BlockchainSnapshot
{
    #region Factory Method
    
    public static BlockchainSnapshot Create(
        Chain chain,
        SourceUrl sourceUrl,
        HttpStatus httpStatus,
        int durationMs,
        RawJson rawJson,
        DateTime createdAtUtcUtc,
        SnapshotId? id = null)
        => new(
            id ?? SnapshotId.CreateNew(),
            chain,
            sourceUrl,
            httpStatus,
            durationMs,
            rawJson,
            createdAtUtcUtc);

    #endregion
    
    #region Constructor

    private BlockchainSnapshot(
        SnapshotId id,
        Chain chain,
        SourceUrl sourceUrl,
        HttpStatus httpStatus,
        int durationMs,
        RawJson rawJson,
        DateTime createdAtUtc)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(durationMs, nameof(durationMs));
        if (createdAtUtc.Kind != DateTimeKind.Utc)
            throw new ArgumentException("CreatedAtUtc must be a UTC date-time.", nameof(createdAtUtc));

        Id = id;
        Chain = chain;
        SourceUrl = sourceUrl;
        HttpStatus = httpStatus;
        DurationMs = durationMs;
        RawJson = rawJson;
        CreatedAtUtc = createdAtUtc;
    }
    
    #endregion
    
    #region Properties
    
    public SnapshotId Id { get; }
    public Chain Chain { get; }
    public SourceUrl SourceUrl { get; }
    public HttpStatus HttpStatus { get; }
    public int DurationMs { get; }
    public RawJson RawJson { get; }
    public DateTime CreatedAtUtc { get; }
    
    #endregion
}