using System.Net.Http.Json;
using ICM.Crypto.Application.Features.BlockchainHistory;
using ICM.Crypto.Application.Interfaces;
using ICM.Crypto.Domain;
using ICM.Crypto.Domain.Helpers;
using ICM.Crypto.Domain.ValueObjects;
using ICM.Crypto.Infrastructure.Persistence;
using Microsoft.Extensions.DependencyInjection;

namespace ICM.Crypto.FunctionalTests.Features.BlockchainHistory;

internal sealed class BlockchainHistoryControllerTests : FunctionalTestFixtureBase
{
    [SetUp]
    public void Setup()
    {
        using var scope = Factory.Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<CryptoDbContext>();
        dbContext.Database.EnsureDeleted();
        dbContext.Database.EnsureCreated();
    }

    [Test]
    public async Task GET_ReturnsExpectedBlockchainHistoryQueryResponse()
    {
        // Arrange
        const int Page = 1;
        const int PageSize = 5;
        const string Json = "{}";
        
        using var scope = Factory.Services.CreateScope();
        var writeRepository = scope.ServiceProvider.GetRequiredService<IBlockchainSnapshotWriteRepository>();
        var unitOfWork  = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();

        var snapshot = BlockchainSnapshot.CreateNew(
            Blockchains.BtcMain,
            Source.Create("BlockCypher"),
            RawJson.Create(Json));

        await writeRepository.AddAsync(snapshot, CancellationToken.None);
        await unitOfWork.SaveChangesAsync(CancellationToken.None);

        // Act
        var response = await Client.GetAsync($"/api/v1.0/blockchain/history?coin=btc&chain=main&page={Page}&pageSize={PageSize}");
        
        // Assert
        Assert.That(response.IsSuccessStatusCode);
        
        var queryResponse = await response.Content.ReadFromJsonAsync<GetBlockchainHistoryQueryResponse>();
        Assert.That(queryResponse, Is.Not.Null);
        using (Assert.EnterMultipleScope())
        {
            Assert.That(queryResponse.Page, Is.EqualTo(Page));
            Assert.That(queryResponse.PageSize, Is.EqualTo(PageSize));
            Assert.That(queryResponse.Items, Has.Count.EqualTo(1));
        }
        Assert.That(queryResponse.Items[0].Data, Is.EqualTo(Json));
    }
}