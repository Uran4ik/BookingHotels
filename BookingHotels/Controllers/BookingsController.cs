using BookingHotels.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookingHotels.Controllers
{
    public class BookingModel
    {
        public int BookingId { get; set; }

        public int UserId { get; set; }

        public int RoomId { get; set; }

        public DateTime CheckIn { get; set; }

        public DateTime CheckOut { get; set; }

        public decimal TotalPrice { get; set; }
    }


    [Route("api/[controller]")]
    [ApiController]
    public class BookingsController : ControllerBase
    {
        public ReservationsHotelContext Context { get; }
        public BookingsController(ReservationsHotelContext context)
        {
            Context = context;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            List<Booking> bookings = Context.Bookings.ToList();
            return Ok(bookings);
        }


        [HttpGet("GetByID")]
        public IActionResult GetById(int id)
        {
            Booking? bookings = Context.Bookings.Where(x => x.BookingId == id).FirstOrDefault();
            if (bookings == null)
                return BadRequest("Такая бронь не найдена");
            return Ok(bookings);
        }


        [HttpPost]
        public IActionResult Add(BookingModel bookings)
        {
            bool userExists = Context.Users.Any(h => h.UserId == bookings.UserId);
            if (!userExists)
                return BadRequest("Пользователя с таким id не существует");
            bool roomExists = Context.Rooms.Any(h => h.RoomId == bookings.RoomId);
            if (!roomExists)
                return BadRequest("Комнаты с таким id не существует");
            var bookingsAdd = new Booking()
            {
                UserId = bookings.UserId,
                RoomId = bookings.RoomId,
                CheckIn = bookings.CheckIn,
                CheckOut = bookings.CheckOut,
                TotalPrice = bookings.TotalPrice,
            };
            Context.Bookings.Add(bookingsAdd);
            Context.SaveChanges();
            return Ok();
        }


        [HttpPut]
        public IActionResult Update(BookingModel bookings)
        {
            bool userExists = Context.Users.Any(h => h.UserId == bookings.UserId);
            if (!userExists)
                return BadRequest("Пользователя с таким id не существует");
            bool roomExists = Context.Rooms.Any(h => h.RoomId == bookings.RoomId);
            if (!roomExists)
                return BadRequest("Комнаты с таким id не существует");

            Booking? us = Context.Bookings.FirstOrDefault(x => x.BookingId == bookings.BookingId);
            if (us != null)
            {
                us.UserId = bookings.UserId;
                us.RoomId = bookings.RoomId;
                us.CheckIn = bookings.CheckIn;
                us.CheckOut = bookings.CheckOut;
                us.TotalPrice = bookings.TotalPrice;

                Context.Bookings.Update(us);
                Context.SaveChanges();
                return Ok(bookings);
            };
            return BadRequest("Такая бронь не найдена");
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            Booking? booking = Context.Bookings.Where(x => x.BookingId == id).FirstOrDefault();
            if (booking == null)
                return BadRequest("Такая бронь не найдена");
            Context.Bookings.Remove(booking);
            Context.SaveChanges();
            return Ok(booking);
        }
    }
}
