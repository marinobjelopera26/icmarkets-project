using ICM.Crypto.Application.Interfaces;
using ICM.Crypto.Domain.ValueObjects;
using MediatR;

namespace ICM.Crypto.Application.Features.GetLatestSnapshot;

internal sealed class GetLatestSnapshotQueryHandler : IRequestHandler<GetLatestSnapshotQuery, GetLatestSnapshotQueryResponse>
{
    private readonly IBlockchainSnapshotReadRepository _readRepository;

    public GetLatestSnapshotQueryHandler(IBlockchainSnapshotReadRepository readRepository)
    {
        _readRepository = readRepository;
    }

    public async Task<GetLatestSnapshotQueryResponse> Handle(
        GetLatestSnapshotQuery request, CancellationToken cancellationToken)
    {
        var blockchain = Blockchain.Create.From(request.Coin, request.Chain);
        var latestSnapshot = await _readRepository.GetLatestSnapshotAsync(blockchain, cancellationToken);

        return new GetLatestSnapshotQueryResponse(
            latestSnapshot.Id.Value, latestSnapshot.Source.Value, latestSnapshot.CreatedAtUtc, latestSnapshot.RawJson.Value);
    }
}