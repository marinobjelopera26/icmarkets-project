namespace ICM.Crypto.Domain.Primitives;

public sealed class InvalidCoinIdentifierException(string message) 
    : DomainValidationException(message);