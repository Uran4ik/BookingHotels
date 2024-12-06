using BookingHotels.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookingHotels.Controllers
{
    public class UsersModel
    {
        public int UserId { get; set; }

        public string Name { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string? Phone { get; set; }

        public string PasswordHash { get; set; } = null!;

        public DateTime? CreatedAt { get; set; }
    }



    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        public ReservationsHotelContext Context { get; }
        public UsersController(ReservationsHotelContext context)
        {
            Context = context;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            List<User> users = Context.Users.ToList();
            return Ok(users);
        }


        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            User? user = Context.Users.Where(x => x.UserId == id).FirstOrDefault();
            if (user == null)
                return BadRequest("Not Found");
            return Ok(user);
        }


        [HttpPost]
        public IActionResult Add (UsersModel user)
        {
            var userAdd = new User()
            {
                Name = user.Name,
                Email = user.Email,
                Phone = user.Phone,
                PasswordHash = user.PasswordHash,
                CreatedAt = user.CreatedAt,
            };
            Context.Users.Add(userAdd);
            Context.SaveChanges();
            return Ok();
        }


        [HttpPut]
        public IActionResult Update(UsersModel user)
        {
            User? us = Context.Users.FirstOrDefault(x => x.UserId == user.UserId);
            if (us != null)
            {
                us.Name = user.Name;
                us.Email = user.Email;
                us.Phone = user.Phone;
                us.PasswordHash = user.PasswordHash;
                us.CreatedAt = user.CreatedAt;
                
                Context.Users.Update(us);
                Context.SaveChanges();
                return Ok(user);
            };
            return BadRequest("Not Found");
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            User? user = Context.Users.Where(x => x.UserId == id).FirstOrDefault();
            if (user == null)
                return BadRequest("Not Found");
            Context.Users.Remove(user);
            Context.SaveChanges();  
            return Ok(user);
        }
    }
}