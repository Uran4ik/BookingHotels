using System;
using System.Collections.Generic;

namespace BookingHotels.Models;

public partial class Transaction
{
    public int TransactionId { get; set; }

    public int BookingId { get; set; }

    public int PaymentMethodId { get; set; }

    public decimal Amount { get; set; }

    public string Status { get; set; } = null!;

    public DateTime? TransactionDate { get; set; }

    public virtual Booking Booking { get; set; } = null!;

    public virtual PaymentMethod PaymentMethod { get; set; } = null!;
}
