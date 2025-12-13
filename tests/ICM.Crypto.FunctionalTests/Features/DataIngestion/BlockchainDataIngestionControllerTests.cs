using System.Text;
using ICM.Crypto.Infrastructure.Persistence;
using ICM.Crypto.Infrastructure.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ICM.Crypto.FunctionalTests.Features.DataIngestion;

internal sealed class BlockchainDataIngestionControllerTests : FunctionalTestFixtureBase
{
    private const string EndpointUrl = "api/v1/blockchain/ingest";

    [SetUp]
    public void Setup()
    {
        using var scope = Factory.Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<CryptoDbContext>();
        dbContext.Database.EnsureDeleted();
        dbContext.Database.EnsureCreated();
    }

    [Test]
    public async Task POST_SuccessfullyIngestsBlockchainSnapshotData()
    {
        const int ExpectedRowCount = 5;
        
        // Arrange
        const string jsonContent = "{ \"blockchains\": \"all\" }";
        var requestBody = new StringContent(jsonContent, Encoding.UTF8, "application/json");

        // Act
        var response = await Client.PostAsync(EndpointUrl, requestBody);

        // Assert
        response.EnsureSuccessStatusCode();
        var responseContent = await response.Content.ReadAsStringAsync();
        Assert.That(responseContent, Is.EqualTo("Successfully ingested blockchain snapshot data."));
        
        // Verify that rows were inserted to the database
        using var  scope = Factory.Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<CryptoDbContext>();
        var entities = await dbContext.Set<BlockchainSnapshotEntity>()
            .AsNoTracking()
            .ToListAsync();
        
        Assert.That(entities, Is.Not.Null);
        Assert.That(entities, Has.Count.EqualTo(ExpectedRowCount));
    }
}