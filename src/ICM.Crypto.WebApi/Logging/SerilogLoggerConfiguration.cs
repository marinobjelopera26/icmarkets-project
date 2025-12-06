using Serilog;
using Serilog.Enrichers.Span;
using Serilog.Events;
using Serilog.Sinks.SystemConsole.Themes;

namespace ICM.Crypto.WebApi.Logging;

internal static class SerilogLoggerConfiguration
{
    public static LoggerConfiguration CreateLoggerConfiguration()
    {
        var loggerConfig = new LoggerConfiguration()
            .MinimumLevel.Information()
            .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
            .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Warning)
            .MinimumLevel.Override("System", LogEventLevel.Warning)
            .Enrich.FromLogContext()
            .Enrich.WithMachineName()
            .Enrich.WithProcessId()
            .Enrich.WithThreadId()
            .Enrich.WithSpan();

        loggerConfig
            .WriteTo.Console(
                theme: AnsiConsoleTheme.Code,
                outputTemplate: "[{Timestamp:HH:mm:ss.zzz} {Level:u3}] {CorrelationId} {SourceContext} {Message:lj}{NewLine}{Exception}")
            .WriteTo.File(
                path: "logs/icm-crypto.log",
                rollingInterval: RollingInterval.Day,
                retainedFileCountLimit: 5,
                shared: true);
        
        return loggerConfig;
    }
}