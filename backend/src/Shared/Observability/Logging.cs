using Serilog;

namespace Shared.Observability;

public static class Logging
{
    public static ILogger CreateConsoleBootstrapLogger() =>
        new LoggerConfiguration()
           .WriteTo.Console(outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3} {SourceContext}] {Message:lj}{NewLine}{Exception}")
           .CreateBootstrapLogger();
}