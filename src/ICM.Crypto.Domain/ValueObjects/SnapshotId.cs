namespace ICM.Crypto.Domain.ValueObjects;

public sealed record SnapshotId
{
    public static SnapshotId CreateNew() => new(Guid.NewGuid());

    private SnapshotId(Guid value)
    {
        Value = value;
    }
    
    public Guid Value { get; }
    
    public override string ToString() => Value.ToString("D");
}