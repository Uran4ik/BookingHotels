using System;
using System.Collections.Generic;

namespace BookingHotels.Models;

public partial class HotelService
{
    public int ServiceId { get; set; }

    public int HotelId { get; set; }

    public string ServiceName { get; set; } = null!;

    public string? Description { get; set; }

    public virtual Hotel Hotel { get; set; } = null!;
}
