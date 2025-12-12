using ICM.Crypto.Application.Interfaces;
using ICM.Crypto.Domain;
using ICM.Crypto.Domain.Helpers;
using ICM.Crypto.Domain.ValueObjects;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ICM.Crypto.Application.Features.DataIngestion;

internal sealed class IngestSnapshotsCommandHandler : IRequestHandler<IngestSnapshotsCommand, Unit>
{
    private readonly IBlockchainSnapshotProvider _provider;
    private readonly IBlockchainSnapshotWriteRepository _writeRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger _logger;

    public IngestSnapshotsCommandHandler(
        IBlockchainSnapshotProvider provider,
        IBlockchainSnapshotWriteRepository writeRepository,
        IUnitOfWork unitOfWork,
        ILogger<IngestSnapshotsCommandHandler> logger)
    {
        _provider = provider;
        _writeRepository = writeRepository;
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<Unit> Handle(IngestSnapshotsCommand command, CancellationToken cancellationToken = default)
    {
        IReadOnlyCollection<BlockchainSnapshotDto> latestSnapshots;
        try
        {
            latestSnapshots = await _provider.GetSnapshotsAsync(
                GetBlockchainsForIngestion(command.Feeds), cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to retrieve latest blockchain snapshots from the provider.");
            throw;
        }

        foreach (var snapshot in latestSnapshots)
        {
            try
            {
                var blockchain = Blockchain.Create.From(snapshot.Coin, snapshot.Chain);
                var blockchainSnapshot = BlockchainSnapshot.CreateNew(
                    blockchain,
                    Source.Create(snapshot.Source),
                    RawJson.Create(snapshot.RawJson));

                await _writeRepository.AddAsync(blockchainSnapshot, cancellationToken);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to create aggregate or persist a blockchain snapshot for {Coin}-{Chain}",
                    snapshot.Coin, snapshot.Chain);
                throw;
            }
        }

        try
        {
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            _logger.LogInformation("Blockchain data ingestion completed successfully.");
            
            return Unit.Value;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to persist the ingested blockchain data.");
            throw;
        }
    }

    private static IEnumerable<Blockchain> GetBlockchainsForIngestion(IngestionFeeds feeds)
    {
        if (feeds.HasFlag(IngestionFeeds.BtcMain))
            yield return Blockchains.BtcMain;
        if (feeds.HasFlag(IngestionFeeds.BtcTest3))
            yield return Blockchains.BtcTest3;
        if (feeds.HasFlag(IngestionFeeds.EthMain))
            yield return Blockchains.EthMain;
        if (feeds.HasFlag(IngestionFeeds.DashMain))
            yield return Blockchains.DashMain;
        if (feeds.HasFlag(IngestionFeeds.LtcMain))
            yield return Blockchains.LtcMain;
    }
}