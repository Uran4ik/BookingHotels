using BookingHotels.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookingHotels.Controllers
{
    public class DiscountsModel
    {
        public int DiscountId { get; set; }

        public int? HotelId { get; set; }

        public int? RoomId { get; set; }

        public decimal DiscountPercentage { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }
    }

    [Route("api/[controller]")]
    [ApiController]
    public class DiscountsController : ControllerBase
    {
        public ReservationsHotelContext Context { get; }
        public DiscountsController(ReservationsHotelContext context)
        {
            Context = context;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            List<Discount> discount = Context.Discounts.ToList();
            return Ok(discount);
        }


        [HttpGet("GetByID")]
        public IActionResult GetById(int id)
        {
            Discount? discount = Context.Discounts.Where(x => x.DiscountId == id).FirstOrDefault();
            if (discount == null)
                return BadRequest("Такая скидка не найдена");
            return Ok(discount);
        }


        [HttpPost]
        public IActionResult Add(DiscountsModel discount)
        {
            bool hotelExists = Context.Hotels.Any(h => h.HotelId == discount.HotelId);
            if (!hotelExists)
                return BadRequest("Отеля с таким id не существует");
            bool roomExists = Context.Rooms.Any(h => h.RoomId == discount.RoomId);
            if (!roomExists)
                return BadRequest("Комнаты с таким id не существует");
            var discountsAdd = new Discount()
            {
                HotelId = discount.HotelId,
                RoomId = discount.RoomId,
                DiscountPercentage = discount.DiscountPercentage,
                StartDate = discount.StartDate,
                EndDate = discount.EndDate,
            };
            Context.Discounts.Add(discountsAdd);
            Context.SaveChanges();
            return Ok();
        }


        [HttpPut]
        public IActionResult Update(DiscountsModel discount)
        {
            bool hotelExists = Context.Hotels.Any(h => h.HotelId == discount.HotelId);
            if (!hotelExists)
                return BadRequest("Отеля с таким id не существует");
            bool roomExists = Context.Rooms.Any(h => h.RoomId == discount.RoomId);
            if (!roomExists)
                return BadRequest("Комнаты с таким id не существует");

            Discount? us = Context.Discounts.FirstOrDefault(x => x.DiscountId== discount.DiscountId);
            if (us != null)
            {
                us.HotelId = discount.HotelId;
                us.RoomId = discount.RoomId;
                us.DiscountPercentage = discount.DiscountPercentage;
                us.StartDate = discount.StartDate;
                us.EndDate = discount.EndDate;

                Context.Discounts.Update(us);
                Context.SaveChanges();
                return Ok(discount);
            };
            return BadRequest("Такая скидка не найдена");
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            Discount? discount = Context.Discounts.Where(x => x.DiscountId == id).FirstOrDefault();
            if (discount == null)
                return BadRequest("Такая скидка не найдена");
            Context.Discounts.Remove(discount);
            Context.SaveChanges();
            return Ok(discount);
        }
    }
}
