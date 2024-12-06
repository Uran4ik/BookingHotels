using System;
using System.Collections.Generic;

namespace BookingHotels.Models;

public partial class Discount
{
    public int DiscountId { get; set; }

    public int? HotelId { get; set; }

    public int? RoomId { get; set; }

    public decimal DiscountPercentage { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public virtual Hotel? Hotel { get; set; }

    public virtual Room? Room { get; set; }
}
