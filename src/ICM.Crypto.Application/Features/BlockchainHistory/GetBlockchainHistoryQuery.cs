using ICM.Crypto.Domain.ValueObjects;
using MediatR;

namespace ICM.Crypto.Application.Features.BlockchainHistory;

public sealed class GetBlockchainHistoryQuery : IRequest<GetBlockchainHistoryQueryResponse>
{
    public required string Coin { get; init; }
    public required string Chain { get; init; }
    public required int Page { get; init; }
    public required int PageSize { get; init; }
}

public sealed record GetBlockchainHistoryQueryResponse(
    int Page,
    int PageSize,
    int Total,
    IReadOnlyList<BlockchainHistoryItem> Items);

public sealed record BlockchainHistoryItem(string Data, DateTime CreatedAtUtc);