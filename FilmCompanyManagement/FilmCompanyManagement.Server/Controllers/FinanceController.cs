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
        public async Task<IActionResult> GetInvestment(int status)
        {
            var ret = await _context.Investments.Include(i => i.Bill).Include(i=>i.Customer).Where(i => i.Bill.Status == status).Select(i => new
            {
                investmentId=i.Id,
                customerName= i.Customer==null?null:i.Customer.CustomerName,

            }).ToListAsync();
            return Ok(ret);
        }

        //[HttpPost("investment/process")]
        //public Task<IActionResult> ProcessInvestment()
        //{

        //}
    }
}