using System.Text;

namespace ICM.Crypto.Domain.ValueObjects;

public sealed record RawJson
{
    public const int DefaultMaxBytes = 512_000; // ~500 KB

    public static RawJson Create(string value, int? maxBytes = null)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(value, nameof(value));

        var bytes = Encoding.UTF8.GetByteCount(value);
        var limit = maxBytes ?? DefaultMaxBytes;

        if (bytes > limit)
            throw new ArgumentException($"Raw JSON size {bytes} exceeds limit {limit} bytes.", nameof(value));

        return new RawJson(value);
    }

    private RawJson(string value)
    {
        Value = value;
    }

    public string Value { get; }

    public int ByteLength => Encoding.UTF8.GetByteCount(Value);
}
