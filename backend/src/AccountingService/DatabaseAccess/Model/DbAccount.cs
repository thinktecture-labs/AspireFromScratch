using System;
using System.Collections.Generic;
using Light.SharedCore.Entities;

namespace AccountingService.DatabaseAccess.Model;

public sealed class DbAccount : GuidEntity
{
    public required string Name { get; set; }

    public decimal Balance { get; set; }

    public List<DbReservation> Reservations { get; set; } = null!;

    public bool TryReserve(Guid correlationId, decimal amount)
    {
        foreach (var reservation in Reservations)
        {
            if (reservation.Id == correlationId)
            {
                return true;
            }
        }

        var currentActualBalance = CalculateActualBalanceInternal();
        if (currentActualBalance < amount)
        {
            return false;
        }

        var newReservation = new DbReservation { Id = correlationId, AccountId = Id, Amount = amount };
        Reservations.Add(newReservation);
        return true;
    }

    private decimal CalculateActualBalanceInternal()
    {
        var balance = Balance;
        foreach (var reservation in Reservations)
        {
            balance -= reservation.Amount;
        }

        return balance;
    }

    public bool TryRemoveReservation(Guid correlationId)
    {
        for (var i = 0; i < Reservations.Count; i++)
        {
            var reservation = Reservations[i];
            if (reservation.Id == correlationId)
            {
                Reservations.RemoveAt(i);
                return true;
            }
        }

        return false;
    }

    public bool TryCommitReservation(Guid correlationId)
    {
        for (var i = 0; i < Reservations.Count; i++)
        {
            var reservation = Reservations[i];
            if (reservation.Id == correlationId)
            {
                Balance -= reservation.Amount;
                Reservations.RemoveAt(i);
                return true;
            }
        }

        return false;
    }
}