using FilmCompanyManagement.Server.EntityFrame;
using FilmCompanyManagement.Server.EntityFrame.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FilmCompanyManagement.Server.Controllers
{
    [ApiController, Route("api/[controller]")]
    public class FinanceController : ControllerBase
    {
        private readonly FCMDbContext _context;

        public FinanceController(FCMDbContext context)
        {
            _context = context;
        }

        [HttpGet("investment/{status}")]
        public async Task<IActionResult> GetInvestment(bool status)
        {
            var ret = await _context.Investments.Include(i => i.Bill).Include(i => i.Customer).Where(i => i.Bill.Status == status).Select(i => new
            {
                investmentId = i.Id,
                customerName = i.Customer == null ? null : i.Customer.CustomerName,
                orderId = i.Bill.Id,
                orderDate = i.Bill.AssignDate,
                amount = i.Bill.Amount
            }).ToListAsync();
            return Ok(ret);
        }

        [HttpPost("investment/process")]
        public async Task<IActionResult> ProcessInvestment(ProcessRequest request)
        {
            Investment tar = await _context.Investments.Include(i => i.Bill).Include(i => i.Customer).SingleAsync(i=>i.Id==request.Id);
            if (tar == null) return BadRequest("Invalid Id");

            tar.Bill.Status = true;
            tar.Bill.ProcessedDate=DateTime.Parse(request.ProcessedDate);
            await _context.SaveChangesAsync();
            return Ok();
        }
    }

    public class ProcessRequest
    {
        public int Id { get; set; }
        public string ProcessedDate { get; set; }
    }
}