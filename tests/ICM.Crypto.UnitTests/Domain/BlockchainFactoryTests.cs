using ICM.Crypto.Domain.Enums;
using ICM.Crypto.Domain.Primitives;
using ICM.Crypto.Domain.ValueObjects;

namespace ICM.Crypto.UnitTests.Domain;

[TestFixture, TestOf(typeof(Blockchain.BlockchainFactory))]
internal sealed class BlockchainFactoryTests
{
    private static readonly IEnumerable<(Coin, Chain)> ValidCombinations =
    [
        (Coin.Btc, Chain.Main),
        (Coin.Btc, Chain.Test3),
        (Coin.Eth, Chain.Main),
        (Coin.Dash, Chain.Main),
        (Coin.Ltc, Chain.Main),
    ];

    private static readonly IEnumerable<(Coin, Chain)> InvalidCombinations =
    [
        (Coin.Eth, Chain.Test3),
        (Coin.Dash, Chain.Test3),
        (Coin.Ltc, Chain.Test3),
    ];

    #region From_EnumValues Tests

    [TestCaseSource(nameof(ValidCombinations))]
    public void From_WhenCoinAndChainAreValidCombination_DoesNotThrow((Coin Coin, Chain Chain) combination)
    {
        // Act & Assert
        Assert.DoesNotThrow(() => Blockchain.Create.From(combination.Coin, combination.Chain));
    }

    [TestCaseSource(nameof(ValidCombinations))]
    public void From_WhenCoinAndChainAreValidCombination_CreatesBlockchainInstance((Coin Coin, Chain Chain) combination)
    {
        // Act & Assert
        var actual = Blockchain.Create.From(combination.Coin, combination.Chain);

        Assert.That(actual, Is.Not.Null);
        using (Assert.EnterMultipleScope())
        {
            Assert.That(actual.Coin, Is.EqualTo(combination.Coin));
            Assert.That(actual.Chain, Is.EqualTo(combination.Chain));
        }
    }

    [TestCaseSource(nameof(InvalidCombinations))]
    public void From_WhenCoinAndChainAreInvalidCombination_ThrowsException((Coin Coin, Chain Chain) combination)
    {
        // Act & Assert
        Assert.Throws<InvalidCoinChainCombinationException>(() =>
            Blockchain.Create.From(combination.Coin, combination.Chain));
    }

    #endregion

    #region From_StringValues Tests

    [TestCase("Btc", "Main")]
    [TestCase("bTc", "tEst3")]
    [TestCase("ETH", "MAIN")]
    [TestCase("dash", "MAIN")]
    [TestCase("LTC", "main")]
    public void From_WithValidCoinChainIdentifier_DoesNotThrow(string coin, string chain)
    {
        // Act & Assert
        Assert.DoesNotThrow(() => Blockchain.Create.From(coin, chain));
    }

    [TestCase("Btc", "Main")]
    [TestCase("bTc", "tEst3")]
    [TestCase("ETH", "MAIN")]
    [TestCase("dash", "MAIN")]
    [TestCase("LTC", "main")]
    public void From_WithValidCoinChainIdentifier_CreatesBlockchainInstance(string coin, string chain)
    {
        // Arrange
        var expectedCoin = Enum.Parse<Coin>(coin, ignoreCase: true);
        var expectedChain = Enum.Parse<Chain>(chain, ignoreCase: true);

        // Act & Assert
        var actual = Blockchain.Create.From(coin, chain);

        Assert.That(actual, Is.Not.Null);
        using (Assert.EnterMultipleScope())
        {
            Assert.That(actual.Coin, Is.EqualTo(expectedCoin));
            Assert.That(actual.Chain, Is.EqualTo(expectedChain));
        }
    }
    
    [TestCase("NET")]
    [TestCase("DOGE")]
    [TestCase("bitcoin")]
    public void From_WhenCoinIdentifierIsInvalid_ThrowsException(string coin)
    {
        const string Chain = "main";
        
        // Act & Assert
        var exception = Assert.Throws<InvalidCoinIdentifierException>(() => Blockchain.Create.From(coin, Chain));
        Assert.That(exception.Message, Contains.Substring($"Invalid coin identifier: '{coin}'."));
    }
    
    [TestCase("MyChain")]
    [TestCase("CHAIN")]
    [TestCase("m_ain")]
    public void From_WhenChainIdentifierIsInvalid_ThrowsException(string chain)
    {
        const string Coin = "BTC";
        
        // Act & Assert
        var exception = Assert.Throws<InvalidChainIdentifierException>(() => Blockchain.Create.From(Coin, chain));
        Assert.That(exception.Message, Contains.Substring($"Invalid chain identifier: '{chain}'."));
    }

    #endregion
}