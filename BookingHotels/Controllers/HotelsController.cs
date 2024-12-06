using BookingHotels.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookingHotels.Controllers
{
    public class HotelModel
    {
        public int HotelId { get; set; }

        public string Name { get; set; } = null!;

        public string Address { get; set; } = null!;

        public string City { get; set; } = null!;

        public decimal? Rating { get; set; }

        public string? Description { get; set; }

        public string? MainImage { get; set; }
    }



    [Route("api/[controller]")]
    [ApiController]
    public class HotelsController : ControllerBase
    {
        public ReservationsHotelContext Context { get; }
        public HotelsController(ReservationsHotelContext context)
        {
            Context = context;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            List<Hotel> hotels = Context.Hotels.ToList();
            return Ok(hotels);
        }


        [HttpGet("GetByID")]
        public IActionResult GetById(int id)
        {
            Hotel? hotel= Context.Hotels.Where(x => x.HotelId == id).FirstOrDefault();
            if (hotel == null)
                return BadRequest("Not Found");
            return Ok(hotel);
        }


        [HttpPost]
        public IActionResult Add(HotelModel hotel)
        {
            var hotelsAdd = new Hotel()
            {
                Name = hotel.Name,
                Address = hotel.Address,
                City = hotel.City,
                Rating = hotel.Rating,
                Description = hotel.Description,
                MainImage = hotel.MainImage,
            };
            Context.Hotels.Add(hotelsAdd);
            Context.SaveChanges();
            return Ok();
        }


        [HttpPut]
        public IActionResult Update(HotelModel hotel)
        {
            Hotel? us = Context.Hotels.FirstOrDefault(x => x.HotelId == hotel.HotelId);
            if (us != null)
            {
                us.Name = hotel.Name;
                us.Address = hotel.Address;
                us.City = hotel.City;
                us.Rating = hotel.Rating;
                us.Description = hotel.Description;
                us.MainImage = hotel.MainImage;

                Context.Hotels.Update(us);
                Context.SaveChanges();
                return Ok(hotel);
            };
            return BadRequest("Not Found");
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            Hotel? hotel = Context.Hotels.Where(x => x.HotelId == id).FirstOrDefault();
            if (hotel == null)
                return BadRequest("Not Found");
            Context.Hotels.Remove(hotel);
            Context.SaveChanges();
            return Ok(hotel);
        }
    }
}
