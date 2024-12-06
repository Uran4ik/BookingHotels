using BookingHotels.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookingHotels.Controllers
{
    public class HotelSevicesModel
    {
        public int ServiceId { get; set; }

        public int HotelId { get; set; }

        public string ServiceName { get; set; } = null!;

        public string? Description { get; set; }
    }

    [Route("api/[controller]")]
    [ApiController]
    public class HotelSevicesController : ControllerBase
    {
        public ReservationsHotelContext Context { get; }
        public HotelSevicesController(ReservationsHotelContext context)
        {
            Context = context;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            List<HotelService> hs = Context.HotelServices.ToList();
            return Ok(hs);
        }


        [HttpGet("GetByID")]
        public IActionResult GetById(int id)
        {
            HotelService? hs = Context.HotelServices.Where(x => x.ServiceId == id).FirstOrDefault();
            if (hs == null)
                return BadRequest("Такой сервис отеля не найден");
            return Ok(hs);
        }


        [HttpPost]
        public IActionResult Add(HotelSevicesModel hs)
        {
            bool hotelExists = Context.Hotels.Any(h => h.HotelId == hs.HotelId);
            if (!hotelExists)
                return BadRequest("Отеля с таким id не существует");
            var hotelServicesAdd = new HotelService()
            {
                HotelId = hs.HotelId,
                ServiceName = hs.ServiceName,
                Description = hs.Description,
            };
            Context.HotelServices.Add(hotelServicesAdd);
            Context.SaveChanges();
            return Ok();
        }


        [HttpPut]
        public IActionResult Update(HotelSevicesModel hs)
        {
            bool hotelExists = Context.Hotels.Any(h => h.HotelId == hs.HotelId);
            if (!hotelExists)
                return BadRequest("Отеля с таким id не существует");

            HotelService? us = Context.HotelServices.FirstOrDefault(x => x.ServiceId == hs.ServiceId);
            if (us != null)
            {
                us.HotelId = hs.HotelId;
                us.ServiceName = hs.ServiceName;
                us.Description = hs.Description;

                Context.HotelServices.Update(us);
                Context.SaveChanges();
                return Ok(hs);
            };
            return BadRequest("Такой сервис отеля не найден");
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            HotelService? hs = Context.HotelServices.Where(x => x.ServiceId == id).FirstOrDefault();
            if (hs == null)
                return BadRequest("Такой сервис отеля не найден");
            Context.HotelServices.Remove(hs);
            Context.SaveChanges();
            return Ok(hs);
        }
    }
}
