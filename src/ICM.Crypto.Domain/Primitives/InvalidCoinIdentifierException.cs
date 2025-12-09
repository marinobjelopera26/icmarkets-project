namespace ICM.Crypto.Domain.Primitives;

public sealed class InvalidCoinIdentifierException : DomainException
{
    public InvalidCoinIdentifierException(string message) : base(message)
    {
    }
}