namespace ICM.Crypto.IntegrationTests;

[TestFixture]
[Category("Integration")]
[Parallelizable(ParallelScope.None)]
internal sealed class SmokeTests
{
    [Test]
    public async Task VerifyWebApiIsHealthy()
    {
        // Arrange
        var client = new HttpClient
        {
            BaseAddress = new Uri(TestEnvironment.ApiBaseUrl)
        };

        // Act
        var response = await client.GetAsync("/health");
        
        // Assert
        response.EnsureSuccessStatusCode();
        var responseBody =  await response.Content.ReadAsStringAsync();
        Assert.That(responseBody, Is.EqualTo("Healthy"));
    }
}