using ICM.Crypto.Infrastructure.Persistence;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestPlatform.TestHost;

namespace ICM.Crypto.FunctionalTests;

internal class WebAppFactory : WebApplicationFactory<Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            var descriptor = services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<CryptoDbContext>));
            if (descriptor is not null)
                services.Remove(descriptor);

            var dbContextDescriptor = services.SingleOrDefault(d => d.ServiceType == typeof(CryptoDbContext));
            if (dbContextDescriptor is not null)
                services.Remove(dbContextDescriptor);

            services.AddDbContext<CryptoDbContext>(options =>
                options.UseInMemoryDatabase("FunctionalTestsDb"));
        });
    }
}