namespace ICM.Crypto.Infrastructure.Persistence.Entities;

internal sealed class BlockchainSnapshotEntity
{
    public Guid Id { get; set; }
    public string Blockchain { get; set; } = null!;
    public string Source { get; set; } = null!;
    public string RawJson { get; set; } = null!;
    
    /// <summary>
    /// NOTE: Mapped to timestamptz column type in Postgres.
    /// </summary>
    public DateTimeOffset CreatedAtUtc { get; set; }
}