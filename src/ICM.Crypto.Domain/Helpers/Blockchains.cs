using ICM.Crypto.Domain.Enums;
using ICM.Crypto.Domain.ValueObjects;

namespace ICM.Crypto.Domain.Helpers;

public static class Blockchains
{
    public static readonly Blockchain BtcMain = Blockchain.Create.From(Coin.Btc, Chain.Main);
    public static readonly Blockchain BtcTest3 = Blockchain.Create.From(Coin.Btc, Chain.Test3);
    public static readonly Blockchain EthMain = Blockchain.Create.From(Coin.Eth, Chain.Main);
    public static readonly Blockchain DashMain = Blockchain.Create.From(Coin.Dash, Chain.Main);
    public static readonly Blockchain LtcMain = Blockchain.Create.From(Coin.Ltc, Chain.Main);
}