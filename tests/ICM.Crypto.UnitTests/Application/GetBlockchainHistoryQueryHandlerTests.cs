using ICM.Crypto.Application.Features.BlockchainHistory;
using ICM.Crypto.Application.Interfaces;
using ICM.Crypto.Domain.ValueObjects;
using NSubstitute;

namespace ICM.Crypto.UnitTests.Application;

[TestFixture, TestOf(typeof(GetBlockchainHistoryQueryHandler))]
internal sealed class GetBlockchainHistoryQueryHandlerTests
{
    private GetBlockchainHistoryQueryHandler subject;
    private IBlockchainSnapshotReadRepository readRepositoryMock;
    
    [SetUp]
    public void Setup()
    {
        readRepositoryMock = Substitute.For<IBlockchainSnapshotReadRepository>();
        subject = new GetBlockchainHistoryQueryHandler(readRepositoryMock);
    }

    [Test]
    public async Task Handle_InvokesReadRepositoryForHistoryAndTotalCount()
    {
        // Arrange
        readRepositoryMock
            .GetHistoryAsync(Arg.Any<Blockchain>(), Arg.Any<int>(), Arg.Any<int>(), Arg.Any<CancellationToken>())
            .Returns([]);
        
        readRepositoryMock
            .GetTotalCountAsync(Arg.Any<Blockchain>(), Arg.Any<CancellationToken>())
            .Returns(0);
        
        var query = new GetBlockchainHistoryQuery
        {
            Coin = "Btc",
            Chain = "Main",
            Page = 1,
            PageSize = 10
        };
        
        // Act
        var actual = await subject.Handle(query, CancellationToken.None);
        
        // Assert
        Assert.That(actual, Is.Not.Null);
        
        await readRepositoryMock
            .Received(1)
            .GetHistoryAsync(Arg.Any<Blockchain>(), Arg.Any<int>(), Arg.Any<int>(), Arg.Any<CancellationToken>());
        
        await readRepositoryMock
            .Received(1)
            .GetTotalCountAsync(Arg.Any<Blockchain>(), Arg.Any<CancellationToken>());
    }
}