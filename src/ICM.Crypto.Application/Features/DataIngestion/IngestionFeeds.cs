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