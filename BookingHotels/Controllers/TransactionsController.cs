using BookingHotels.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookingHotels.Controllers
{
    public class TransactionModel
    {
        public int TransactionId { get; set; }

        public int BookingId { get; set; }

        public int PaymentMethodId { get; set; }

        public decimal Amount { get; set; }

        public string Status { get; set; } = null!;

        public DateTime? TransactionDate { get; set; }
    }


    [Route("api/[controller]")]
    [ApiController]
    public class TransactionsController : ControllerBase
    {
        public ReservationsHotelContext Context { get; }
        public TransactionsController(ReservationsHotelContext context)
        {
            Context = context;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            List<Transaction> tran = Context.Transactions.ToList();
            return Ok(tran);
        }


        [HttpGet("GetByID")]
        public IActionResult GetById(int id)
        {
            Transaction? tran = Context.Transactions.Where(x => x.TransactionId == id).FirstOrDefault();
            if (tran == null)
                return BadRequest("Такая транзакция не найдена");
            return Ok(tran);
        }


        [HttpPost]
        public IActionResult Add(TransactionModel tran)
        {
            bool bookingExists = Context.Bookings.Any(h => h.BookingId == tran.BookingId);
            if (!bookingExists)
                return BadRequest("Брони с таким id не существует");
            bool paymentExists = Context.PaymentMethods.Any(h => h.PaymentMethodId == tran.PaymentMethodId);
            if (!paymentExists)
                return BadRequest("Метода оплаты с таким id не существует");
            var transAdd = new Transaction()
            {
                BookingId = tran.BookingId,
                PaymentMethodId = tran.PaymentMethodId,
                Amount = tran.Amount,
                Status = tran.Status,
                TransactionDate = tran.TransactionDate,
            };
            Context.Transactions.Add(transAdd);
            Context.SaveChanges();
            return Ok();
        }


        [HttpPut]
        public IActionResult Update(TransactionModel tran)
        {
            bool bookingExists = Context.Bookings.Any(h => h.BookingId == tran.BookingId);
            if (!bookingExists)
                return BadRequest("Брони с таким id не существует");
            bool paymentExists = Context.PaymentMethods.Any(h => h.PaymentMethodId == tran.PaymentMethodId);
            if (!paymentExists)
                return BadRequest("Метода оплаты с таким id не существует");

            Transaction? us = Context.Transactions.FirstOrDefault(x => x.TransactionId == tran.TransactionId);
            if (us != null)
            {
                us.BookingId = tran.BookingId;
                us.PaymentMethodId = tran.PaymentMethodId;
                us.Amount = tran.Amount;
                us.Status = tran.Status;
                us.TransactionDate = tran.TransactionDate;

                Context.Transactions.Update(us);
                Context.SaveChanges();
                return Ok(tran);
            };
            return BadRequest("Такая транзакция не найдена");
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            Transaction? tran = Context.Transactions.Where(x => x.TransactionId == id).FirstOrDefault();
            if (tran == null)
                return BadRequest("Такая транзакция не найдена");
            Context.Transactions.Remove(tran);
            Context.SaveChanges();
            return Ok(tran);
        }
    }
}
