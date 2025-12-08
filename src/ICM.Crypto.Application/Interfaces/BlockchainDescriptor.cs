using ICM.Crypto.Domain.Enums;

namespace ICM.Crypto.Application.Interfaces;

/// <summary>
/// Describes the specific blockchain by defining the coin and the chain.
/// </summary>
public readonly struct BlockchainDescriptor(
    Coin coin,
    Chain chain)
{
    public Coin  Coin { get; } = coin;
    public Chain Chain { get; } = chain;
}