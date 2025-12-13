using System.Net;
using DotNet.Testcontainers.Builders;
using DotNet.Testcontainers.Containers;
using DotNet.Testcontainers.Networks;
using Testcontainers.PostgreSql;

namespace ICM.Crypto.IntegrationTests;

[SetUpFixture]
public class IntegrationTestFixture
{
    private IContainer _apiContainer;
    private PostgreSqlContainer _pgContainer;
    private INetwork _network;

    private const ushort ApiPort = 8080;
    private const ushort PgPort = 5432;
    
    private const string PgUsername = "postgres";
    private const string PgPassword = "postgres";
    private const string PgDatabase = "integrationtests";
    
    [OneTimeSetUp]
    public async Task GlobalSetup()
    {
        _network = new NetworkBuilder()
            .WithName($"it-{Guid.NewGuid():N}")
            .Build();
        
        await _network.CreateAsync();
        
        _pgContainer = new PostgreSqlBuilder()
            .WithImage("postgres:16-alpine")
            .WithNetwork(_network)
            .WithNetworkAliases("postgres")
            .WithDatabase(PgDatabase)
            .WithUsername(PgUsername)
            .WithPassword(PgPassword)
            .WithPortBinding(PgPort, true)
            .WithWaitStrategy(Wait.ForUnixContainer()
                .UntilInternalTcpPortIsAvailable(PgPort))
            .Build();

        await _pgContainer.StartAsync();
        
        _apiContainer = new ContainerBuilder()
            .WithImage("icm/blockchain.api:integration")
            .WithNetwork(_network)
            .WithNetworkAliases("webapi")
            .WithEnvironment("ASPNETCORE_URLS", $"http://0.0.0.0:{ApiPort}")
            .WithEnvironment("DOTNET_ENVIRONMENT", "IntegrationTests")
            .WithEnvironment("ConnectionStrings__Default", $"Host=postgres;Port={PgPort};Username={PgUsername};Password={PgPassword};Database={PgDatabase}")
            .WithPortBinding(ApiPort, true)
            .WithWaitStrategy(
                Wait.ForUnixContainer()
                    .UntilInternalTcpPortIsAvailable(ApiPort)
                    .UntilHttpRequestIsSucceeded(
                        r => r.ForPort(ApiPort).ForPath("/health").ForStatusCode(HttpStatusCode.OK), 
                        modifier => modifier.WithTimeout(TimeSpan.FromSeconds(60)).WithInterval(TimeSpan.FromSeconds(2))))
            .Build();

        await _apiContainer.StartAsync();

        TestEnvironment.ApiBaseUrl = $"http://{_apiContainer.Hostname}:{_apiContainer.GetMappedPublicPort(ApiPort)}";
        TestEnvironment.Api = _apiContainer;
        TestEnvironment.Database = _pgContainer;
    }
    
    [OneTimeTearDown]
    public async Task GlobalTeardown()
    {
        await _apiContainer.StopAsync();
        await _pgContainer.StopAsync();
        await _network.DeleteAsync();
        
        await _apiContainer.DisposeAsync();
        await _pgContainer.DisposeAsync();
        await _network.DisposeAsync();
    }
}