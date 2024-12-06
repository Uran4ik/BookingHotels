using BookingHotels.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookingHotels.Controllers
{
    public class PaymentMethodsModel
    {
        public int PaymentMethodId { get; set; }
        public string MethodName { get; set; } = null!;
    }

    [Route("api/[controller]")]
    [ApiController]
    public class PaymentMethodsController : ControllerBase
    {
        public ReservationsHotelContext Context { get; }
        public PaymentMethodsController(ReservationsHotelContext context)
        {
            Context = context;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            List<PaymentMethod> pm = Context.PaymentMethods.ToList();
            return Ok(pm);
        }


        [HttpGet("GetByID")]
        public IActionResult GetById(int id)
        {
            PaymentMethod? pm = Context.PaymentMethods.Where(x => x.PaymentMethodId == id).FirstOrDefault();
            if (pm == null)
                return BadRequest("Такой метод оплаты не найден");
            return Ok(pm);
        }


        [HttpPost]
        public IActionResult Add(PaymentMethodsModel pm)
        {
            var paymentsAdd = new PaymentMethod()
            {
                MethodName = pm.MethodName,
            };
            Context.PaymentMethods.Add(paymentsAdd);
            Context.SaveChanges();
            return Ok();
        }


        [HttpPut]
        public IActionResult Update(PaymentMethodsModel pm)
        {
            PaymentMethod? us = Context.PaymentMethods.FirstOrDefault(x => x.PaymentMethodId == pm.PaymentMethodId);
            if (us != null)
            {
                us.PaymentMethodId = pm.PaymentMethodId;
                us.MethodName = pm.MethodName;

                Context.PaymentMethods.Update(us);
                Context.SaveChanges();
                return Ok(pm);
            };
            return BadRequest("Такой метод оплаты не найден");
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            PaymentMethod? pm = Context.PaymentMethods.Where(x => x.PaymentMethodId == id).FirstOrDefault();
            if (pm == null)
                return BadRequest("Такой метод оплаты не найден");
            Context.PaymentMethods.Remove(pm);
            Context.SaveChanges();
            return Ok(pm);
        }
    }
}
