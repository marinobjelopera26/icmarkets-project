using Asp.Versioning;
using ICM.Crypto.Application.Abstractions.Messaging;
using ICM.Crypto.Application.Features.DataIngestion;
using ICM.Crypto.WebApi.Versioning;
using Microsoft.AspNetCore.Mvc;

namespace ICM.Crypto.WebApi.Features.DataIngestion;

[ApiController]
[ApiVersion(ApiVersions.V1_0)]
[Route("api/v{version:apiVersion}/blockchains/ingest")]
public sealed class BlockchainDataIngestionController : ControllerBase
{
    [HttpPost]
    [MapToApiVersion(ApiVersions.V1_0)]
    public async Task<IActionResult> PostAsync(
        [FromBody] IngestBlockchainDataRequest request,
        [FromServices] ICommandHandler<IngestSnapshotsCommand> handler,
        CancellationToken cancellationToken)
    {
        var command = new IngestSnapshotsCommand(request.Blockchains);
        
        await handler.HandleAsync(command, cancellationToken);

        return Ok("Successfully ingested blockchain snapshot data.");
    }
}