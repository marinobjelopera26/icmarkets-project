using Asp.Versioning;
using Asp.Versioning.ApiExplorer;
using ICM.Crypto.Application;
using ICM.Crypto.Infrastructure.BlockCypher;
using ICM.Crypto.Infrastructure.HostedServices;
using ICM.Crypto.Infrastructure.Persistence;
using ICM.Crypto.WebApi.Logging;
using ICM.Crypto.WebApi.Swagger;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Serilog;
using Swashbuckle.AspNetCore.SwaggerGen;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
Log.Logger = SerilogLoggerConfiguration
    .CreateLoggerConfiguration()
    .CreateLogger();

builder.Host.UseSerilog(Log.Logger, dispose: true);

builder.Services.AddControllers();

builder.Services.AddCors(options =>
{
    var allowedOrigins = builder.Configuration
        .GetSection("Cors:AllowedOrigins")
        .Get<string[]>() ?? [];
    
    var allowedMethods = builder.Configuration
        .GetSection("Cors:AllowedMethods")
        .Get<string[]>() ?? [];
    
    options.AddPolicy("DefaultPolicy", policy =>
    {
        policy
            .WithOrigins(allowedOrigins)
            .WithMethods(allowedMethods)
            .AllowAnyHeader()
            .SetPreflightMaxAge(TimeSpan.FromMinutes(30));
    });
});

builder.Services
    .AddApiVersioning(options =>
    {
        options.DefaultApiVersion = new ApiVersion(1, 0);
        options.AssumeDefaultVersionWhenUnspecified = true;
        options.ReportApiVersions = true;

        options.ApiVersionReader = ApiVersionReader.Combine(
            new UrlSegmentApiVersionReader(),
            new HeaderApiVersionReader("X-Api-Version"));
    })
    .AddApiExplorer(options =>
    {
        options.GroupNameFormat = "'v'VVV";
        options.SubstituteApiVersionInUrl = true;
    });

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();

builder.Services
    .AddApplication()
    .AddBlockCypher()
    .AddPersistence(builder.Configuration)
    .AddDataIngestionHostedService(builder.Configuration);

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<CryptoDbContext>();
    await db.Database.MigrateAsync();
}

app.UseSerilogRequestLogging();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    var provider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();
    
    app.UseSwagger();
    app.UseSwaggerUI(setup =>
    {
        foreach (var description in provider.ApiVersionDescriptions)
        {
            setup.SwaggerEndpoint(
                url: $"/swagger/{description.GroupName}/swagger.json",
                name: $"ICM Crypto API {description.GroupName.ToUpperInvariant()}");
        }
    });
}

app.UseHttpsRedirection();

app.UseCors("DefaultPolicy");

app.UseAuthorization();

app.MapControllers();

try
{
    Log.Information("Starting web host ({Environment})", app.Environment.EnvironmentName);
    await app.RunAsync();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Host terminated unexpectedly");
}
finally
{
    Log.CloseAndFlush();
}