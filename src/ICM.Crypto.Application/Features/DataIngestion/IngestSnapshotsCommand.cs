using MediatR;

namespace ICM.Crypto.Application.Features.DataIngestion;

/// <summary>
/// Command that executes the data ingestion of the latest blockchain snapshots.
/// </summary>
/// <param name="Feeds">
/// Configure for which feeds (blockchains) the snapshots shall be ingested.
/// </param>
public sealed record IngestSnapshotsCommand(IngestionFeeds Feeds) : IRequest;