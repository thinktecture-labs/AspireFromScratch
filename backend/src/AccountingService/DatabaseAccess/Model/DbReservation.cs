using System;
using Light.SharedCore.Entities;

namespace AccountingService.DatabaseAccess.Model;

public sealed class DbReservation : GuidEntity
{
    public required Guid AccountId { get; set; }

    public DbAccount Account { get; set; } = null!;

    public decimal Amount { get; set; }
}