using BookingHotels.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookingHotels.Controllers
{
    public class RoomsModel
    {
        public int RoomId { get; set; }

        public int HotelId { get; set; }

        public string Type { get; set; } = null!;

        public decimal PricePerNight { get; set; }

        public int MaxOccupancy { get; set; }

        public string? Description { get; set; }
    }


    [Route("api/[controller]")]
    [ApiController]
    public class RoomsController : ControllerBase
    {
        public ReservationsHotelContext Context { get; }
        public RoomsController(ReservationsHotelContext context)
        {
            Context = context;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            List<Room> rooms = Context.Rooms.ToList();
            return Ok(rooms);
        }


        [HttpGet("GetByID")]
        public IActionResult GetById(int id)
        {
            Room? room = Context.Rooms.Where(x => x.RoomId == id).FirstOrDefault();
            if (room == null)
                return BadRequest("Такая комната не найдена");
            return Ok(room);
        }


        [HttpPost]
        public IActionResult Add(RoomsModel room)
        {
            bool hotelExists = Context.Hotels.Any(h => h.HotelId == room.HotelId);
            if (!hotelExists)
                return BadRequest("Отеля с таким id не существует");
            var roomsAdd = new Room()
            {
                HotelId = room.HotelId,
                Type = room.Type,
                PricePerNight = room.PricePerNight,
                MaxOccupancy = room.MaxOccupancy,
                Description = room.Description,
            };
            Context.Rooms.Add(roomsAdd);
            Context.SaveChanges();
            return Ok();
        }


        [HttpPut]
        public IActionResult Update(RoomsModel room)
        {
            bool hotelExists = Context.Hotels.Any(h => h.HotelId == room.HotelId);
            if (!hotelExists)
                return BadRequest("Отеля с таким id не существует");

            Room? us = Context.Rooms.FirstOrDefault(x => x.RoomId == room.RoomId);
            if (us != null)
            {
                us.HotelId = room.HotelId;
                us.Type = room.Type;
                us.PricePerNight = room.PricePerNight;
                us.MaxOccupancy = room.MaxOccupancy;
                us.Description = room.Description;

                Context.Rooms.Update(us);
                Context.SaveChanges();
                return Ok(room);
            };
            return BadRequest("Такая комната не найдена");
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            Room? room = Context.Rooms.Where(x => x.RoomId == id).FirstOrDefault();
            if (room == null)
                return BadRequest("Такая комната не найдена");
            Context.Rooms.Remove(room);
            Context.SaveChanges();
            return Ok(room);
        }
    }
}
