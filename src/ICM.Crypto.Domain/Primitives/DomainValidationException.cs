namespace ICM.Crypto.Domain.Primitives;

public abstract class DomainValidationException : DomainException
{
    protected DomainValidationException(string message) 
        : base(message)
    {
    }
}