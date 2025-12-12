namespace ICM.Crypto.Domain.Primitives;

public sealed class InvalidCoinChainCombinationException(string message) 
    : DomainValidationException(message);