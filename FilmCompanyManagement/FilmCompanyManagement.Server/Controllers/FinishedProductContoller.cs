using FilmCompanyManagement.Server.EntityFrame;
using FilmCompanyManagement.Server.EntityFrame.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FilmCompanyManagement.Controllers
{
    [ApiController,Route("api/[controller]")]
    public class FinishedProductController:ControllerBase
    {
        private readonly FCMDbContext _context;

        public FinishedProductController(FCMDbContext context)
        {
            _context = context;
        }

        [HttpGet("unprocessed")]
        public async Task<IActionResult> GetUnprocessed()
        {
            var ret=await _context.FinishedProducts.Where(i=>i.OrderStatus=="unprocessed").Select(e => new
            {
                orderId = e.Id,
                orderDate = e.Bill.Date,
                invoiceNumber = e.Bill.Id,
                amount = e.Bill.Amount,
                expenseType = e.Bill.Type
            }).ToListAsync();
            return Ok(ret);
        }

        [HttpGet("processed")]
        public async Task<IActionResult> GetProcessed()
        {
            var ret = await _context.FinishedProducts.Where(i => i.OrderStatus == "processed").Select(e => new
            {
                orderId = e.Id,
                orderDate = e.Bill.Date,
                invoiceNumber = e.Bill.Id,
                amount = e.Bill.Amount,
                expenseType = e.Bill.Type,
                processedDate = e.Date
            }).ToListAsync();
            return Ok(ret);
        }

        [HttpPost("markprocessed")]
        public async Task<IActionResult> MarkProcessed(ProcessingFinishedProduct proc )
        {
            string id = proc.id;
            string processedDate = proc.processedDate;

            FinishedProduct fnp = await _context.FinishedProducts.SingleAsync(i => i.Id == id);
            if (fnp == null) return BadRequest(new
            {
                status = "failed",
                message = "Id doesnt exist"
            });

            fnp.OrderStatus = "processed";
            fnp.Date = DateTime.Parse(processedDate);
            await _context.SaveChangesAsync();
            return Ok(new
            {
                status = "success"
            });
        }
    }

    public class ProcessingFinishedProduct
    {
        public string id { get; set; }
        public string processedDate { get; set; }
    }
}