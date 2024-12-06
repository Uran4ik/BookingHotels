﻿using System;
using System.Collections.Generic;

namespace BookingHotels.Models;

public partial class Review
{
    public int ReviewId { get; set; }

    public int HotelId { get; set; }

    public int UserId { get; set; }

    public decimal Rating { get; set; }

    public string? Comment { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual Hotel Hotel { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
