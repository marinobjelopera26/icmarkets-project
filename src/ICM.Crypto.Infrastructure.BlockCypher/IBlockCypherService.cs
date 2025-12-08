using ICM.Crypto.Application;

namespace ICM.Crypto.Infrastructure.BlockCypher;

internal interface IBlockCypherService
{
    /// <summary>
    /// Retrieves information about a specific blockchain from the BlockCypher blockchain API. 
    /// </summary>
    /// <param name="descriptor">Blockchain type descriptor.</param>
    /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
    /// <returns></returns>
    Task<BlockchainResponse> GetBlockchainInfoAsync(BlockchainDescriptor descriptor, CancellationToken cancellationToken = default);
}