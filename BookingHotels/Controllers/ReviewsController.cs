using BookingHotels.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace BookingHotels.Controllers
{
    public class ReviewsModel
    {
        public int ReviewId { get; set; }

        public int HotelId { get; set; }

        public int UserId { get; set; }

        public decimal Rating { get; set; }

        public string? Comment { get; set; }

        public DateTime? CreatedAt { get; set; }
    }



    [Route("api/[controller]")]
    [ApiController]
    public class ReviewsController : ControllerBase
    {
        public ReservationsHotelContext Context { get; }
        public ReviewsController(ReservationsHotelContext context)
        {
            Context = context;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            List<Review> review = Context.Reviews.ToList();
            return Ok(review);
        }


        [HttpGet("GetByID")]
        public IActionResult GetById(int id)
        {
            Review? review = Context.Reviews.Where(x => x.ReviewId == id).FirstOrDefault();
            if (review == null)
                return BadRequest("Такой отзыв не найден");
            return Ok(review);
        }


        [HttpPost]
        public IActionResult Add(ReviewsModel review)
        {
            bool hotelExists = Context.Hotels.Any(h => h.HotelId == review.HotelId);
            if (!hotelExists)
                return BadRequest("Отеля с таким id не существует");
            bool userExists = Context.Users.Any(h => h.UserId == review.UserId);
            if (!userExists)
                return BadRequest("Пользователя с таким id не существует");
            var reviewsAdd = new Review()
            {
                HotelId = review.HotelId,
                UserId = review.UserId,
                Rating = review.Rating,
                Comment = review.Comment,
                CreatedAt = review.CreatedAt,
            };
            Context.Reviews.Add(reviewsAdd);
            Context.SaveChanges();
            return Ok();
        }


        [HttpPut]
        public IActionResult Update(ReviewsModel review)
        {
            bool hotelExists = Context.Hotels.Any(h => h.HotelId == review.HotelId);
            if (!hotelExists)
                return BadRequest("Отеля с таким id не существует");
            bool userExists = Context.Users.Any(h => h.UserId == review.UserId);
            if (!userExists)
                return BadRequest("Пользователя с таким id не существует");

            Review? us = Context.Reviews.FirstOrDefault(x => x.ReviewId == review.ReviewId);
            if (us != null)
            {
                us.ReviewId = review.ReviewId;
                us.HotelId = review.HotelId;
                us.UserId = review.UserId;
                us.Rating = review.Rating;
                us.Comment = review.Comment;
                us.CreatedAt = review.CreatedAt;

                Context.Reviews.Update(us);
                Context.SaveChanges();
                return Ok(review);
            };
            return BadRequest("Такой отзыв не найден");
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            Review? review = Context.Reviews.Where(x => x.ReviewId == id).FirstOrDefault();
            if (review == null)
                return BadRequest("Такой отзыв не найден");
            Context.Reviews.Remove(review);
            Context.SaveChanges();
            return Ok(review);
        }
    }
}
