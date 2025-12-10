namespace ICM.Crypto.Infrastructure.BlockCypher;

internal interface IBlockCypherService
{
    /// <summary>
    /// Retrieves information about a specific blockchain from BlockCypher Blockchain API. 
    /// </summary>
    /// <param name="request">Request DTO.</param>
    /// <param name="cancellationToken"><see cref="CancellationToken"/>.</param>
    /// <returns></returns>
    Task<BlockchainResponse> GetBlockchainAsync(GetBlockchainRequestDto request, CancellationToken cancellationToken = default);
}