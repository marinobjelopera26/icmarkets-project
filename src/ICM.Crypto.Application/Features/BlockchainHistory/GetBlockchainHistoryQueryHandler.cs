using ICM.Crypto.Application.Interfaces;
using ICM.Crypto.Domain.ValueObjects;
using MediatR;

namespace ICM.Crypto.Application.Features.BlockchainHistory;

internal sealed class GetBlockchainHistoryQueryHandler : IRequestHandler<GetBlockchainHistoryQuery, GetBlockchainHistoryQueryResponse>
{
    private readonly IBlockchainSnapshotReadRepository _readRepository;

    public GetBlockchainHistoryQueryHandler(
        IBlockchainSnapshotReadRepository readRepository)
    {
        _readRepository = readRepository;
    }
    
    public async Task<GetBlockchainHistoryQueryResponse> Handle(GetBlockchainHistoryQuery query, CancellationToken cancellationToken)
    {
        var blockchain = Blockchain.Create.From(query.Coin, query.Chain);
        
        var history = await _readRepository.GetHistoryAsync(
            blockchain, 
            query.Page, 
            query.PageSize, 
            cancellationToken);

        var totalCount = await _readRepository.GetTotalCountAsync(blockchain, cancellationToken);

        return new GetBlockchainHistoryQueryResponse(
            query.Page, 
            query.PageSize, 
            totalCount, 
            history.Select(item => 
                new BlockchainHistoryItem(item.RawJson.Value, item.CreatedAtUtc)).ToList());
    }
}