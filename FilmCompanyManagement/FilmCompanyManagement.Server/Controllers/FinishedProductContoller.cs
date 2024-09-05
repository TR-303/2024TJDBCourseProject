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
            var ret=await _context.FinishedProducts.Where(i=>i.OrderStatus=="unprocessed").ToListAsync();
            return Ok(ret);
        }

        [HttpGet("processed")]
        public async Task<IActionResult> GetProcessed()
        {
            var ret = await _context.FinishedProducts.Where(i => i.OrderStatus == "processed").ToListAsync();
            return Ok(ret);
        }

        [HttpPost]
        public async Task<IActionResult> MarkAsProcessed([FromBody] string id)
        {
            FinishedProduct fnp = await _context.FinishedProducts.SingleAsync(i => i.Id == id);
            if (fnp == null) return BadRequest(new
            {
                status = "failed",
                message = "Id doesnt exist"
            });

            fnp.OrderStatus = "processed";
            await _context.SaveChangesAsync();
            return Ok(new
            {
                status = "success"
            });
        }
    }
}