using Asp.Versioning;
using ICM.Crypto.Application.Features.DataIngestion;
using ICM.Crypto.WebApi.Versioning;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ICM.Crypto.WebApi.Features.DataIngestion;

[ApiController]
[ApiVersion(ApiVersions.V1_0)]
[Route("api/v{version:apiVersion}/blockchains/ingest")]
public sealed class BlockchainDataIngestionController : ControllerBase
{
    [HttpPost]
    [MapToApiVersion(ApiVersions.V1_0)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> PostAsync(
        [FromBody] IngestBlockchainDataRequest request,
        [FromServices] ISender mediator,
        CancellationToken cancellationToken)
    {
        var command = new IngestSnapshotsCommand(request.Blockchains);
        
        await mediator.Send(command, cancellationToken);

        return Ok("Successfully ingested blockchain snapshot data.");
    }
}