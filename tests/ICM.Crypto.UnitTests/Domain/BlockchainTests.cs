using ICM.Crypto.Domain.Enums;
using ICM.Crypto.Domain.ValueObjects;

namespace ICM.Crypto.UnitTests.Domain;

[TestFixture, TestOf(typeof(Blockchain))]
internal sealed class BlockchainTests
{
    [TestCase(Coin.Btc, Chain.Main, "btc.main")]
    [TestCase(Coin.Btc, Chain.Test3, "btc.test3")]
    [TestCase(Coin.Eth, Chain.Main, "eth.main")]
    [TestCase(Coin.Dash, Chain.Main, "dash.main")]
    [TestCase(Coin.Ltc, Chain.Main, "ltc.main")]
    public void ToString_ReturnsCorrectFormat(Coin coin, Chain chain, string expected)
    {
        // Arrange
        var blockchain = Blockchain.Create.From(coin, chain);
        
        // Act
        var actual = blockchain.ToString();
        
        // Assert
        Assert.That(actual, Is.EqualTo(expected));
    }
}