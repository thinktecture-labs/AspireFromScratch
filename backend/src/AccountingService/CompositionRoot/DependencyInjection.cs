using AccountingService.DatabaseAccess;
using Microsoft.AspNetCore.Builder;
using Shared.Observability;

namespace AccountingService.CompositionRoot;

public static class DependencyInjection
{
    public static WebApplicationBuilder ConfigureServices(this WebApplicationBuilder builder)
    {
        builder.ConfigureDefaultLogging();

        builder
           .Services
           .AddDatabaseAccess(builder.Configuration)
           .AddOpenTelemetryMetricsAndTracing();
        return builder;
    }
}