using Microsoft.AspNetCore.Builder;
using Serilog;

namespace AccountingService.CompositionRoot;

public static class Middleware
{
    public static WebApplication ConfigureMiddleware(this WebApplication app)
    {
        app.UseSerilogRequestLogging();
        app.UseRouting();
        return app;
    }
}