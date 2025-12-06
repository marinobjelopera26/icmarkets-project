using ICM.Crypto.Application;
using ICM.Crypto.Infrastructure.BlockCypher;
using ICM.Crypto.Infrastructure.Persistence;
using ICM.Crypto.WebApi.Logging;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
Log.Logger = SerilogLoggerConfiguration
    .CreateLoggerConfiguration()
    .CreateLogger();

builder.Host.UseSerilog(Log.Logger, dispose: true);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services
    .AddApplication()
    .AddBlockCypher()
    .AddPersistence();

var app = builder.Build();

app.UseSerilogRequestLogging();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

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