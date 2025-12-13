using ICM.Crypto.Domain.Enums;
using ICM.Crypto.Domain.Primitives;

namespace ICM.Crypto.Domain.ValueObjects;

public sealed record Blockchain
{
    public static readonly BlockchainFactory Create = new();

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

    #region Factory

    public sealed class BlockchainFactory
    {
        private const string DefaultDelimiter = "-";

        public Blockchain From(Coin coin, Chain chain)
        {
            Validate();

            return new Blockchain(coin, chain);

            void Validate()
            {
                if (chain == Chain.Test3 && coin != Coin.Btc)
                {
                    throw new InvalidCoinChainCombinationException(
                        $"Chain '{chain:G}' is only valid for '{Coin.Btc:G}");
                }
            }
        }

        public Blockchain From(string coin, string chain)
        {
            if (!Enum.TryParse<Coin>(coin, ignoreCase: true, out var parsedCoin))
                throw new InvalidCoinIdentifierException($"Invalid coin identifier: '{coin}'.");

            if (!Enum.TryParse<Chain>(chain, ignoreCase: true, out var parsedChain))
                throw new InvalidChainIdentifierException($"Invalid chain identifier: '{chain}'.");

            return From(parsedCoin, parsedChain);
        }
    }

    #endregion
}