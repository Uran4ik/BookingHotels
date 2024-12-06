using System;
using System.Collections.Generic;

namespace BookingHotels.Models;

public partial class Room
{
    public int RoomId { get; set; }

    public int HotelId { get; set; }

    public string Type { get; set; } = null!;

    public decimal PricePerNight { get; set; }

    public int MaxOccupancy { get; set; }

    public string? Description { get; set; }

    public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();

    public virtual ICollection<Discount> Discounts { get; set; } = new List<Discount>();

    public virtual Hotel Hotel { get; set; } = null!;
}
