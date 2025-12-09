using ICM.Crypto.Domain.ValueObjects;

namespace ICM.Crypto.Domain;

public sealed class BlockchainSnapshot
{
    #region Factory Method
    
    public static BlockchainSnapshot CreateNew(
        Blockchain blockchain,
        Source source,
        RawJson rawJson)
        => new(
            id: SnapshotId.CreateNew(),
            blockchain,
            source,
            rawJson,
            createdAtUtc: DateTime.UtcNow);

    #endregion
    
    #region Constructor

    private BlockchainSnapshot(
        SnapshotId id,
        Blockchain blockchain,
        Source source,
        RawJson rawJson,
        DateTime createdAtUtc)
    {
        if (createdAtUtc.Kind != DateTimeKind.Utc)
            throw new ArgumentException("CreatedAtUtc must be a UTC date-time.", nameof(createdAtUtc));

        Id = id;
        Blockchain = blockchain;
        Source = source;
        RawJson = rawJson;
        CreatedAtUtc = createdAtUtc;
    }
    
    #endregion
    
    #region Properties
    
    public SnapshotId Id { get; }
    public Blockchain Blockchain { get; }
    public Source Source { get; }
    public RawJson RawJson { get; }
    public DateTime CreatedAtUtc { get; }
    
    #endregion
}