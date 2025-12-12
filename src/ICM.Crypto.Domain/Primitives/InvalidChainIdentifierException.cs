namespace ICM.Crypto.Domain.Primitives;

public sealed class InvalidChainIdentifierException(string message) 
    : DomainValidationException(message);