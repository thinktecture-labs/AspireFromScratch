using System.Threading.Tasks;
using Aspire.Hosting;
using Projects;
using Shared;

namespace AspireAppHost;

public static class Program
{
    public static async Task Main(string[] args)
    {
        var builder = DistributedApplication.CreateBuilder(args);

        var postgresServer = builder.AddPostgres(Constants.PostgresServerName);
        var accountingDb = postgresServer.AddDatabase(Constants.AccountingDb);

        builder
           .AddProject<AccountingService>(Constants.AccountingServiceName)
           .WithReference(accountingDb);

        await builder.Build().RunAsync();
    }
}