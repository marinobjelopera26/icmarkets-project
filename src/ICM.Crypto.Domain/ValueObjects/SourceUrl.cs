namespace ICM.Crypto.Domain.ValueObjects;

public sealed record SourceUrl
{
    public static SourceUrl Create(string value)
    {
        if (!Uri.TryCreate(value, UriKind.Absolute, out var parsedUri) ||
            (parsedUri.Scheme != Uri.UriSchemeHttp && parsedUri.Scheme != Uri.UriSchemeHttps))
        {
            throw new ArgumentException("");
        }

        return new SourceUrl(parsedUri.ToString());
    }

    private SourceUrl(string value)
    {
        Value = value;
    }
    
    public string Value { get; }

    public override string ToString() => Value;
}