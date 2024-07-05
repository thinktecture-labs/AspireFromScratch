using AccountingService.DatabaseAccess.Model;
using Microsoft.EntityFrameworkCore;

namespace AccountingService.DatabaseAccess;

public sealed class AccountingDbContext : DbContext
{
    public AccountingDbContext(DbContextOptions<AccountingDbContext> options) : base(options) { }

    public DbSet<DbAccount> Accounts => Set<DbAccount>();
    public DbSet<DbReservation> Reservations => Set<DbReservation>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<DbAccount>(
            options =>
            {
                options.Property(x => x.Id).ValueGeneratedNever();
                options.Property(x => x.Name).IsRequired().HasMaxLength(200);
            }
        );

        modelBuilder.Entity<DbReservation>(
            options =>
            {
                options.Property(x => x.Id).ValueGeneratedNever();
            }
        );
    }
}