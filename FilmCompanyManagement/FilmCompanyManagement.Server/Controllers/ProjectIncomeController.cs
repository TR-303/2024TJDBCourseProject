using FilmCompanyManagement.Server.EntityFrame;
using FilmCompanyManagement.Server.EntityFrame.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FilmCompanyManagement.Controllers
{
    [ApiController, Route("api/[controller]")]
    public class ProjectIncomeController : ControllerBase
    {
        private readonly FCMDbContext _context;

        public ProjectIncomeController(FCMDbContext context)
        {
            _context = context;
        }

        [HttpGet("unprocessed")]
        public async Task<IActionResult> GetUnprocessed()
        {
            var ret = await _context.Projects.Where(i => i.OrderStatus == "unprocessed").Select(e => new
            {
                projectId = e.Id,
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
            var ret = await _context.Projects.Where(i => i.OrderStatus == "processed").Select(e => new
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
        public async Task<IActionResult> MarkProcessed(ProcessingProjecIncome proc)
        {
            string id = proc.id;
            string processedDate = proc.processedDate;

            Project prj = await _context.Projects.SingleAsync(i => i.Id == id);
            if (prj == null) return BadRequest(new
            {
                status = "failed",
                message = "Id doesnt exist"
            });

            prj.OrderStatus = "processed";
            prj.Date = DateTime.Parse(processedDate);
            await _context.SaveChangesAsync();
            return Ok(new
            {
                status = "success"
            });
        }
    }

    public class ProcessingProjecIncome
    {
        public string id { get; set; }
        public string processedDate { get; set; }
    }
}