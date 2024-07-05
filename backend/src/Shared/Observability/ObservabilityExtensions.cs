using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using OpenTelemetry.Metrics;
using OpenTelemetry.Trace;
using Serilog;

namespace Shared.Observability;

public static class ObservabilityExtensions
{
    public static void ConfigureDefaultLogging(this WebApplicationBuilder builder)
    {
        builder.Logging.ClearProviders().AddOpenTelemetry(
            options =>
            {
                options.IncludeFormattedMessage = true;
                options.IncludeScopes = true;
            }
        );
        builder.Host.UseSerilog(
            (context, loggerConfiguration) => loggerConfiguration.ReadFrom.Configuration(context.Configuration),
            writeToProviders: true
        );
    }

    public static IServiceCollection AddOpenTelemetryMetricsAndTracing(
        this IServiceCollection services) =>
        services
           .AddOpenTelemetry()
           .WithMetrics(metrics =>
                {
                    metrics
                       .AddAspNetCoreInstrumentation()
                       .AddHttpClientInstrumentation()
                       .AddRuntimeInstrumentation();
                }
            )
           .WithTracing(tracing => tracing.AddAspNetCoreInstrumentation())
           .Services;
}