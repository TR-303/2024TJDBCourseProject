using FilmCompanyManagement.Server.EntityFrame;
using FilmCompanyManagement.Server.EntityFrame.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FilmCompanyManagement.Controllers
{
    [ApiController, Route("api/[controller]")]
    public class ExternalInvestmentsController : ControllerBase
    {
        private readonly FCMDbContext _context;

        public ExternalInvestmentsController(FCMDbContext context)
        {
            _context = context;
        }

        [HttpGet("unprocessed")]
        public async Task<IActionResult> GetUnprocessed()
        {
            var ret = await _context.Investments.Where(i => i.BillStatus == "unprocessed").Select(i => new
            {
                investmentId = i.Id,
                customerId = i.Customer.Id,
                orderDate = i.Date,
                invoiceNumber = i.Bill.Id,
                amount = i.Bill.Amount,
                expenseType = i.Bill.Type
            }).ToListAsync();
            return Ok(ret);
        }

        [HttpGet("processed")]
        public async Task<IActionResult> GetProcessed()
        {
            var ret = await _context.Investments.Where(i => i.BillStatus == "processed").Select(i => new
            {
                orderId = i.Bill.Id,
                orderDate = i.Date,
                amount = i.Bill.Amount,
                expenseType = i.Bill.Type,
                processedDate = i.Date
            }).ToListAsync();
            return Ok(ret);
        }

        [HttpPost("markprocessed")]
        public async Task<IActionResult> MarkProcessed(ProcessingInvestment proc)
        {
            string investmentId = proc.investmentId;
            string processedDate = proc.processedDate;

            Investment inv = await _context.Investments.SingleAsync(i => i.Id == investmentId);
            if (inv == null) return BadRequest(new
            {
                status = "failed",
                message = "Id doesnt exist"
            });

            inv.BillStatus = "processed";
            inv.Date = DateTime.Parse(processedDate);
            inv.Bill.Account.Balance += inv.Bill.Amount;
            await _context.SaveChangesAsync();

            return Ok(new
            {
                status = "success"
            });
        }
    }

    public class ProcessingInvestment
    {
        public string investmentId { get; set; }
        public string processedDate { get; set; }
    }
}