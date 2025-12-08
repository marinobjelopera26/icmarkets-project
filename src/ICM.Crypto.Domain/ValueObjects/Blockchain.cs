using ICM.Crypto.Domain.Enums;
using ICM.Crypto.Domain.Primitives;

namespace ICM.Crypto.Domain.ValueObjects;

public sealed record Blockchain
{
    public static Blockchain Create(Coin coin, Chain chain)
    {
        Validate(coin, chain);
        return new Blockchain(coin, chain);
    }

    private static void Validate(Coin coin, Chain chain)
    {
        if (chain == Chain.Test3 && coin != Coin.Btc)
        {
            throw new InvalidChainException(
                $"Network '{chain:G}' is only valid for '{Enums.Coin.Btc:G}");
        }
    }

    private Blockchain(Coin coin, Chain chain)
    {
        Coin = coin;
        Chain = chain;
    }
    
    public Coin Coin { get; }
    public Chain Chain { get; }

    /// <summary>
    /// E.g. 'btc.main', 'eth.main', 'btc.test3'.
    /// </summary>
    public override string ToString() =>
        $"{Coin.ToString().ToLowerInvariant()}.{Chain.ToString().ToLowerInvariant()}";
}

public sealed class InvalidChainException(string message) 
    : DomainException(message);