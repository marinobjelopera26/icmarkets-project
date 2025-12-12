using System.Text.Json.Serialization;
using ICM.Crypto.Application.Features.DataIngestion;

namespace ICM.Crypto.WebApi.Features.DataIngestion;

public sealed class IngestBlockchainDataRequest
{
    /// <summary>
    /// Feeds (blockchains) to ingest snapshot data for represented by an enumeration.
    /// Multiple enumeration flags can be used simultaneously.
    /// <example>
    /// {
    ///     "blockchains": "BtcMain, BtcTest3, LtcMain"
    /// }
    /// </example>
    /// <example>
    /// {
    ///     "blockchains": "All"
    /// }
    /// </example>
    /// </summary>
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public IngestionFeeds Blockchains { get; init; }
}