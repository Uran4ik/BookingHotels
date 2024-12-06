using BookingHotels.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookingHotels.Controllers
{
    public class FavoriteModel
    {
        public int FavoriteId { get; set; }
        public int UserId { get; set; }
        public int HotelId { get; set; }
    }

    [Route("api/[controller]")]
    [ApiController]
    public class FavoritesController : ControllerBase
    {
        public ReservationsHotelContext Context { get; }
        public FavoritesController(ReservationsHotelContext context)
        {
            Context = context;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            List<Favorite> favorite = Context.Favorites.ToList();
            return Ok(favorite);
        }


        [HttpGet("GetByID")]
        public IActionResult GetById(int id)
        {
            Favorite? favorite = Context.Favorites.Where(x => x.FavoriteId == id).FirstOrDefault();
            if (favorite == null)
                return BadRequest("Такие фавориты не найдены");
            return Ok(favorite);
        }


        [HttpPost]
        public IActionResult Add(FavoriteModel favorite)
        {
            bool hotelExists = Context.Hotels.Any(h => h.HotelId == favorite.HotelId);
            if (!hotelExists)
                return BadRequest("Отеля с таким id не существует");
            bool userExists = Context.Users.Any(h => h.UserId == favorite.UserId);
            if (!userExists)
                return BadRequest("Пользователей с таким id не существует");
            var favoritsAdd = new Favorite()
            {
                HotelId = favorite.HotelId,
                UserId = favorite.UserId,
            };
            Context.Favorites.Add(favoritsAdd);
            Context.SaveChanges();
            return Ok();
        }


        [HttpPut]
        public IActionResult Update(FavoriteModel favorite)
        {
            bool hotelExists = Context.Hotels.Any(h => h.HotelId == favorite.HotelId);
            if (!hotelExists)
                return BadRequest("Отеля с таким id не существует");
            bool userExists = Context.Users.Any(h => h.UserId == favorite.UserId);
            if (!userExists)
                return BadRequest("Пользователей с таким id не существует");

            Favorite? us = Context.Favorites.FirstOrDefault(x => x.FavoriteId == favorite.FavoriteId);
            if (us != null)
            {
                us.HotelId = favorite.HotelId;
                us.UserId = favorite.UserId;

                Context.Favorites.Update(us);
                Context.SaveChanges();
                return Ok(favorite);
            };
            return BadRequest("Такие фавориты не найдены");
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            Favorite? favorite = Context.Favorites.Where(x => x.FavoriteId == id).FirstOrDefault();
            if (favorite == null)
                return BadRequest("Такие фавориты не найдены");
            Context.Favorites.Remove(favorite);
            Context.SaveChanges();
            return Ok(favorite);
        }
    }
}
