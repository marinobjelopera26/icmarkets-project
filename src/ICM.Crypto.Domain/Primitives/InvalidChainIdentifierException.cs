namespace ICM.Crypto.Domain.Primitives;

public sealed class InvalidChainIdentifierException : DomainException
{
    public InvalidChainIdentifierException(string message)
        : base(message)
    {
    }
}