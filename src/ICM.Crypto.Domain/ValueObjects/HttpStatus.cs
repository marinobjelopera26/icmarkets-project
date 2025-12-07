namespace ICM.Crypto.Domain.ValueObjects;

public sealed record HttpStatus
{
    public static HttpStatus Create(int value)
    {
        if (value is < 100 or > 599)
        {
            throw new ArgumentOutOfRangeException(
                nameof(value), "HTTP status code must be between 100 and 599.");
        }

        return new HttpStatus(value);
    }

    private HttpStatus(int value)
    {
        Value = value;
    }

    public int Value { get; }
}