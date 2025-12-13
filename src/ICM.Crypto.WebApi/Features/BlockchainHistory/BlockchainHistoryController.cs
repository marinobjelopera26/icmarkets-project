using Asp.Versioning;
using ICM.Crypto.Application.Features.BlockchainHistory;
using ICM.Crypto.WebApi.Versioning;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ICM.Crypto.WebApi.Features.BlockchainHistory;

[ApiController]
[ApiVersion(ApiVersions.V1_0)]
[Route("api/v{version:apiVersion}/blockchain/history")]
public class BlockchainHistoryController : ControllerBase
{
    private readonly ISender _mediator;

    public BlockchainHistoryController(ISender mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [MapToApiVersion(ApiVersions.V1_0)]
    [ProducesResponseType<GetBlockchainHistoryQueryResponse>(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAsync(
        [FromQuery] GetBlockchainHistoryQuery query,
        CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(query, cancellationToken);
        return Ok(response);
    }
}