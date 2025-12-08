namespace ICM.Crypto.Domain.ValueObjects;

public sealed record Source
{
    public static Source Create(string value)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(value, nameof(value));
        return new Source(value);
    }

    private Source(string value)
    {
        Value = value;
    }
    
    public string Value { get; }

    public override string ToString() => Value;
}