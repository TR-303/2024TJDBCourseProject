using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FilmCompanyManagement.Server.EntityFrame;
using FilmCompanyManagement.Server.EntityFrame.Models;

namespace FilmCompanyManagement.Controllers
{
    [ApiController, Route("api/worker")]
    public class WorkerController : ControllerBase
    {

        private readonly FCMDbContext _context;

        public WorkerController(FCMDbContext context)
        {
            _context = context;
        }

        [HttpPost("expense")]
        public async Task<ActionResult> InsertExpense(expenseRequest request)
        {
            var bill = new Bill
            {
                AssignDate = Convert.ToDateTime(request.date),
                Amount = request.amount
            };
            _context.Bills.Add(bill);
            await _context.SaveChangesAsync();
            return Ok(1);
        }
    }

    public class workerRequest
    {
        public string id { get; set; }
    }

    public class expenseRequest
    {
        public string id { get; set; }
        public string date { get; set; }
        public string description { get; set; }
        public decimal amount { get; set; }
    }
}
