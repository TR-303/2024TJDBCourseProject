using FilmCompanyManagement.Server.EntityFrame;
using FilmCompanyManagement.Server.EntityFrame.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FilmCompanyManagement.Controllers
{
    [ApiController, Route("api/[controller]")]
    public class SalaryController : ControllerBase
    {
        private readonly FCMDbContext _context;

        public SalaryController(FCMDbContext context)
        {
            _context = context;
        }

        [HttpGet("unprocessed")]
        public async Task<IActionResult> GetUnprocessed()
        {
            var ret = await _context.Employees.Where(i => i.SalaryStatus == "unprocessed").ToListAsync();
            return Ok(ret);
        }

        [HttpGet("processed")]
        public async Task<IActionResult> GetProcessed()
        {
            var ret = await _context.Employees.Where(i => i.SalaryStatus == "processed").ToListAsync();
            return Ok(ret);
        }

        [HttpPost]
        public async Task<IActionResult> MarkAsProcessed([FromBody] string id)
        {
            Employee emp
                = await _context.Employees.SingleAsync(i => i.Id == id);
            if (emp == null) return BadRequest(new
            {
                status = "failed",
                message = "Id doesnt exist"
            });

            emp.SalaryStatus = "processed";
            
            await _context.SaveChangesAsync();
            return Ok(new
            {
                status = "success"
            });
        }
    }
}