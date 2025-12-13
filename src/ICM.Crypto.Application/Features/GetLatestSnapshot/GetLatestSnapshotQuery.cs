using MediatR;

namespace ICM.Crypto.Application.Features.GetLatestSnapshot;

public class GetLatestSnapshotQuery : IRequest<GetLatestSnapshotQueryResponse>
{
    public required string Coin { get; init; }
    public required string Chain { get; init; }
}

public sealed record GetLatestSnapshotQueryResponse(
    Guid Id,
    string Source,
    DateTime CreatedAtUtc,
    string Data);