using FilmCompanyManagement.Server.EntityFrame;
using FilmCompanyManagement.Server.EntityFrame.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FilmCompanyManagement.Controllers
{
    [ApiController,Route("api/[controller]")]
    public class ExternalInvestmentController:ControllerBase
    {
        private readonly FCMDbContext _context;

        public ExternalInvestmentController(FCMDbContext context)
        {
            _context = context;
        }

        [HttpGet("unprocessed")]
        public async Task<IActionResult> GetUnprocessed()
        {
            var ret=await _context.Investments.Where(i=>i.BillStatus=="unprocessed").ToListAsync();
            return Ok(ret);
        }

        [HttpGet("processed")]
        public async Task<IActionResult> GetProcessed()
        {
            var ret = await _context.Investments.Where(i => i.BillStatus == "processed").ToListAsync();
            return Ok(ret);
        }

        [HttpPost]
        public async Task<IActionResult> MarkAsProcessed([FromBody]string investmentId)
        {
            Investment inv = await _context.Investments.SingleAsync(i => i.Id == investmentId);
            if (inv == null) return BadRequest(new
            {
                status = "failed",
                message="Id doesnt exist"
            });

            inv.BillStatus = "processed";
            inv.Bill.Account.Balance += inv.Bill.Amount;
            await _context.SaveChangesAsync();
            return Ok(new
            {
                status="success"
            });
        }
    }
}