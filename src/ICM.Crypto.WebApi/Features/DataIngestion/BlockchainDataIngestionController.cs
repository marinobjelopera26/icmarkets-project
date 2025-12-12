using Asp.Versioning;
using ICM.Crypto.Application.Features.DataIngestion;
using ICM.Crypto.WebApi.Versioning;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ICM.Crypto.WebApi.Features.DataIngestion;

[ApiController]
[ApiVersion(ApiVersions.V1_0)]
[Route("api/v{version:apiVersion}/blockchain/ingest")]
public sealed class BlockchainDataIngestionController : ControllerBase
{
    private readonly ISender _mediator;

    public BlockchainDataIngestionController(ISender mediator)
    {
        _mediator = mediator;
    }
    
    [HttpPost]
    [MapToApiVersion(ApiVersions.V1_0)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> PostAsync(
        [FromBody] IngestSnapshotsCommand command, CancellationToken cancellationToken)
    {
        await _mediator.Send(command, cancellationToken);

        return Ok("Successfully ingested blockchain snapshot data.");
    }
}