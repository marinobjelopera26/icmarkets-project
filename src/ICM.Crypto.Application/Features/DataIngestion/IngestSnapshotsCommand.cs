using ICM.Crypto.Application.Abstractions.Messaging;

namespace ICM.Crypto.Application.Features.DataIngestion;

[Flags]
public enum IngestionFeeds
{
    BtcMain = 1,
    BtcTest3 = 2,
    EthMain = 4,
    DashMain = 8,
    LtcMain = 16,
    All = BtcMain | BtcTest3 | EthMain | DashMain | LtcMain,
}

/// <summary>
/// Command that executes the data ingestion of the latest blockchain snapshots.
/// </summary>
/// <param name="Feeds">
/// Configure for which feeds (sources) the snapshots shall be ingested.
/// </param>
public sealed record IngestSnapshotsCommand(
    IngestionFeeds Feeds = IngestionFeeds.All) : ICommand;