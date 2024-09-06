using FilmCompanyManagement.Server.EntityFrame;
using FilmCompanyManagement.Server.EntityFrame.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FilmCompanyManagement.Controllers
{
    [ApiController, Route("api/[controller]")]
    public class EquipmentLeasingController : ControllerBase
    {
        private readonly FCMDbContext _context;

        public EquipmentLeasingController(FCMDbContext context)
        {
            _context = context;
        }

        [HttpGet("unprocessed")]
        public async Task<IActionResult> GetUnprocessed()
        {
            var ret = await _context.EquipmentLeases.Where(i => i.OrderStatus == "unprocessed").Select(e => new
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
            var ret = await _context.EquipmentLeases.Where(i => i.OrderStatus == "processed").Select(e => new
            {
                projectId = e.Id,
                orderDate = e.Bill.Date,
                invoiceNumber = e.Bill.Id,
                amount = e.Bill.Amount,
                expenseType = e.Bill.Type,
                processedDate = e.OrderDate
            }).ToListAsync();
            return Ok(ret);
        }

        [HttpPost("markprocessed")]
        public async Task<IActionResult> MarkProcessed(ProcessingEquipmentLeasing proc)
        {
            string projectId = proc.projectId;
            string processedDate = proc.processedDate;

            EquipmentLease eql = await _context.EquipmentLeases.SingleAsync(i => i.Id == projectId);
            if (eql == null) return BadRequest(new
            {
                status = "failed",
                message = "Id doesnt exist"
            });

            eql.OrderStatus = "processed";
            eql.OrderDate = DateTime.Parse(processedDate);
            await _context.SaveChangesAsync();
            return Ok(new
            {
                status = "success"
            });
        }
    }

    public class ProcessingEquipmentLeasing
    {
        public string projectId { get; set; }
        public string processedDate { get; set; }
    }
}