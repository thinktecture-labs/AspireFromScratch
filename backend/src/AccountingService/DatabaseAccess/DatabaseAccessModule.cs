using System.Threading.Tasks;
using Light.GuardClauses;
using Light.GuardClauses.Exceptions;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shared;

namespace AccountingService.DatabaseAccess;

public static class DatabaseAccessModule
{
    public static IServiceCollection AddDatabaseAccess(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString(Constants.AccountingDb);
        if (connectionString.IsNullOrWhiteSpace())
        {
            throw new InvalidConfigurationException("The connection string for AccountingDb is missing");
        }
        
        return services.AddDbContext<AccountingDbContext>(options => options.UseNpgsql(connectionString));
    }
    
    public static async Task MigrateDatabaseAsync(this IApplicationBuilder app)
    {
        await using var scope = app.ApplicationServices.CreateAsyncScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<AccountingDbContext>();
        await dbContext.Database.MigrateAsync();
    }
}