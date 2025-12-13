using ICM.Crypto.Domain;
using ICM.Crypto.Domain.Helpers;
using ICM.Crypto.Domain.ValueObjects;

namespace ICM.Crypto.UnitTests.Domain;

[TestFixture, TestOf(typeof(BlockchainSnapshot))]
internal sealed class BlockchainSnapshotTests
{
    private readonly Source Source = Source.Create("TestSource");
    private readonly RawJson Json = RawJson.Create("{\"test\": \"I'm in a unit test\" }");

    [Test]
    public void CreateNew_InitializesIdAndCreatedAtUtc_Properties()
    {
        // Act
        var actual = BlockchainSnapshot.CreateNew(Blockchains.BtcMain, Source, Json);
        
        // Assert
        using(Assert.EnterMultipleScope())
        {
            Assert.That(actual.Id.Value, Is.Not.EqualTo(Guid.Empty));
            Assert.That(actual.CreatedAtUtc, Is.Not.Default);
        }
    }
}