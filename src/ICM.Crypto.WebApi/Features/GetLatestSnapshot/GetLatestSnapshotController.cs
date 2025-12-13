using Asp.Versioning;
using ICM.Crypto.Application.Features.GetLatestSnapshot;
using ICM.Crypto.WebApi.Versioning;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ICM.Crypto.WebApi.Features.GetLatestSnapshot;

[ApiController]
[ApiVersion(ApiVersions.V1_0)]
[Route("api/v{version:apiVersion}/blockchain/latest")]
public class GetLatestSnapshotController : ControllerBase
{
    private readonly IMediator _mediator;

    public GetLatestSnapshotController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetAsync(
        [FromQuery] GetLatestSnapshotQuery query, 
        CancellationToken cancellationToken)
    {
        var queryResponse = await _mediator.Send(query, cancellationToken);
        return Ok(queryResponse);
    }
}