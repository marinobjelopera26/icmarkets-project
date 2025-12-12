using ICM.Crypto.Application.Features.DataIngestion;
using ICM.Crypto.Application.Interfaces;
using ICM.Crypto.Domain;
using ICM.Crypto.Domain.ValueObjects;
using Microsoft.Extensions.Logging;
using NSubstitute;

namespace ICM.Crypto.UnitTests.Application;

[TestFixture, TestOf(typeof(IngestSnapshotsCommandHandler))]
internal sealed class IngestSnapshotsCommandHandlerTests
{
    private IngestSnapshotsCommandHandler subject;
    private IBlockchainSnapshotProvider providerMock;
    private IBlockchainSnapshotWriteRepository writeRepositoryMock;
    private IUnitOfWork unitOfWorkMock;
    private ILogger<IngestSnapshotsCommandHandler> loggerMock;

    [SetUp]
    public void SetUp()
    {
        providerMock = Substitute.For<IBlockchainSnapshotProvider>();
        writeRepositoryMock = Substitute.For<IBlockchainSnapshotWriteRepository>();
        unitOfWorkMock = Substitute.For<IUnitOfWork>();
        loggerMock = Substitute.For<ILogger<IngestSnapshotsCommandHandler>>();

        subject = new IngestSnapshotsCommandHandler(
            providerMock,
            writeRepositoryMock,
            unitOfWorkMock,
            loggerMock);
    }

    [Test]
    public async Task Handle_WhenSnapshotProviderReturnsNoData_DoesNotInvokeWriteRepository()
    {
        // Arrange
        providerMock
            .GetSnapshotsAsync(Arg.Any<IEnumerable<Blockchain>>(), Arg.Any<CancellationToken>())
            .Returns([]);

        var command = new IngestSnapshotsCommand(IngestionFeeds.BtcMain);

        // Act
        await subject.Handle(command, CancellationToken.None);

        await writeRepositoryMock.DidNotReceive().AddAsync(
            Arg.Any<BlockchainSnapshot>(), Arg.Any<CancellationToken>());
    }

    [Test]
    public async Task Handle_WhenSnapshotProviderReturnsNoData_DoesNotInvokeUnitOfWork()
    {
        // Arrange
        providerMock
            .GetSnapshotsAsync(Arg.Any<IEnumerable<Blockchain>>(), Arg.Any<CancellationToken>())
            .Returns([]);

        var command = new IngestSnapshotsCommand(IngestionFeeds.BtcMain);

        // Act
        await subject.Handle(command, CancellationToken.None);

        await unitOfWorkMock.DidNotReceive().SaveChangesAsync(Arg.Any<CancellationToken>());
    }

    [Test]
    public async Task Handle_WhenSnapshotProviderReturnsData_InvokesWriteRepository()
    {
        // Arrange
        const int ExpectedNumberOfInvocations = 2;
        
        var snapshot_1 = new BlockchainSnapshotDto("MySource", "Btc", "Main", "{}");
        var snapshot_2 = new BlockchainSnapshotDto("MySource", "Btc", "Test3", "{}");
        
        providerMock
            .GetSnapshotsAsync(Arg.Any<IEnumerable<Blockchain>>(), Arg.Any<CancellationToken>())
            .Returns([snapshot_1, snapshot_2]);

        var command = new IngestSnapshotsCommand(IngestionFeeds.BtcMain);
        
        // Act
        await subject.Handle(command, CancellationToken.None);

        // Assert
        await writeRepositoryMock.Received(ExpectedNumberOfInvocations).AddAsync(Arg.Any<BlockchainSnapshot>(), Arg.Any<CancellationToken>());
    }
    
    [Test]
    public async Task Handle_WhenSnapshotProviderReturnsData_InvokesUnitOfWork()
    {
        // Arrange
        var snapshot_1 = new BlockchainSnapshotDto("MySource", "Btc", "Main", "{}");
        
        providerMock
            .GetSnapshotsAsync(Arg.Any<IEnumerable<Blockchain>>(), Arg.Any<CancellationToken>())
            .Returns([snapshot_1]);

        var command = new IngestSnapshotsCommand(IngestionFeeds.BtcMain);
        
        // Act
        await subject.Handle(command, CancellationToken.None);

        // Assert
        await unitOfWorkMock.Received(1).SaveChangesAsync(Arg.Any<CancellationToken>());
    }
}