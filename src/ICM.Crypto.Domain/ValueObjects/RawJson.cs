namespace ICM.Crypto.Domain.ValueObjects;

public sealed record RawJson
{
    public static RawJson Create(string value)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(value, nameof(value));
        return new RawJson(value);
    }

    private RawJson(string value)
    {
        Value = value;
    }

    public string Value { get; }
}
