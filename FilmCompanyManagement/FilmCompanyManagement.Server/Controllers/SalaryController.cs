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
            var ret = await _context.Employees.Where(i => i.SalaryStatus == "unprocessed").Select(e => new
            {
                payrollNumber=e.SalaryBill.Id,
                basePay=e.Salary
            }).ToListAsync();
            return Ok(ret);
        }

        [HttpGet("processed")]
        public async Task<IActionResult> GetProcessed()
        {
            var ret = await _context.Employees.Where(i => i.SalaryStatus == "processed").Select(e => new
            {
                payrollNumber = 0,
                basePay = e.Salary
            }).ToListAsync();
            return Ok(ret);
        }

        [HttpPost("markprocessed")]
        public async Task<IActionResult> MarkProcessed([FromBody] string id)
        {
            Employee emp = await _context.Employees.SingleAsync(i => i.Id == id && i.SalaryStatus == "unprocessed");
            if (emp == null) return BadRequest(new
            {
                status = "failed",
                message = "Id doesnt exist or already processed"
            });

            emp.SalaryStatus = "processed";
            emp.SalaryBill.Account.Balance -= emp.Salary;
            await _context.SaveChangesAsync();

            return Ok(new
            {
                status = "success"
            });
        }
    }
}