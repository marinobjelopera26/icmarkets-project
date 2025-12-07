using ICM.Crypto.Domain.Enums;
using ICM.Crypto.Domain.Primitives;

namespace ICM.Crypto.Domain.ValueObjects;

public sealed record Chain
{
    public static Chain Create(Blockchain blockchain, Network network)
    {
        Validate(blockchain, network);
        return new Chain(blockchain, network);
    }

    private static void Validate(Blockchain blockchain, Network network)
    {
        if (network == Network.Test3 && blockchain != Blockchain.Btc)
        {
            throw new InvalidChainException(
                $"Network '{network:G}' is only valid for '{Enums.Blockchain.Btc:G}");
        }
    }

    private Chain(Blockchain blockchain, Network network)
    {
        Blockchain = blockchain;
        Network = network;
    }
    
    public Blockchain Blockchain { get; }
    public Network Network { get; }
}

public sealed class InvalidChainException(string message) 
    : DomainException(message);