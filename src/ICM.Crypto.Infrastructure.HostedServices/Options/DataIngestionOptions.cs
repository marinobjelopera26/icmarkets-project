namespace ICM.Crypto.Infrastructure.HostedServices.Options;

public sealed class DataIngestionOptions
{
    public const string SectionName = "DataIngestion";

    /// <summary>
    /// Indicates whether data ingestion hosted service
    /// is enabled. If set to false, data ingestion hosted
    /// service will not start any data ingestion cycles.
    /// </summary>
    public bool Enabled { get; init; } = true;
    
    /// <summary>
    /// Indicates how often the data ingestion hosted service
    /// should execute a new data ingestion cycle.
    /// </summary>
    public int PollingIntervalInSeconds { get; init; } = 60;
}