namespace ICM.Crypto.FunctionalTests;

[TestFixture]
internal abstract class FunctionalTestFixtureBase
{
    protected WebAppFactory Factory;
    protected HttpClient Client;
    
    [OneTimeSetUp]
    public virtual void OneTimeSetup()
    {
        Factory = new WebAppFactory();
        Client = Factory.CreateClient();
    }
    
    [OneTimeTearDown]
    public virtual void OneTimeTearDown()
    {
        Factory.Dispose();
        Client.Dispose();
    }
}