namespace ICM.Crypto.Infrastructure.Persistence.Entities;

internal sealed class BlockchainSnapshotEntity
{
    public Guid Id { get; set; }
    public string ChainKey { get; set; } = null!;
    public string Blockchain { get; set; } = null!;
    public string Network { get; set; } = null!;
    public string Source { get; set; } = null!;
    public string RawJson { get; set; } = null!;
    public int HttpStatus { get; set; }
    
    /// <summary>
    /// NOTE: Mapped to timestamptz column type in Postgres.
    /// </summary>
    public DateTimeOffset CreatedAtUtc { get; set; }
}