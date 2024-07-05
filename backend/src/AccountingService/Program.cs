using System;
using System.Threading.Tasks;
using AccountingService.CompositionRoot;
using AccountingService.DatabaseAccess;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;
using Serilog;
using Shared.Observability;

namespace AccountingService;

public static class Program
{
    public static async Task<int> Main(string[] args)
    {
        Log.Logger = Logging.CreateConsoleBootstrapLogger();

        try
        {
            var app = WebApplication
               .CreateBuilder(args)
               .ConfigureServices()
               .Build()
               .ConfigureMiddleware();

            await app.MigrateDatabaseAsync();
            await app.RunAsync();
            return 0;
        }
        catch (HostAbortedException e)
        {
            Log.Debug(e, "Host was aborted, most likely because of dotnet ef");
            return 0;
        }
        catch (Exception e)
        {
            Log.Fatal(e, "Could not run Accounting Service");
            return 1;
        }
        finally
        {
            await Log.CloseAndFlushAsync();
        }
    }
}
