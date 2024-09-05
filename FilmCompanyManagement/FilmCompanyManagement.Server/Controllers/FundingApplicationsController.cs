using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FilmCompanyManagement.Server.EntityFrame;
using FilmCompanyManagement.Server.EntityFrame.Models;

namespace FilmCompanyManagement.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class FundingApplicationsController : ControllerBase
    {

        private readonly FCMDbContext _context;

        public FundingApplicationsController(FCMDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<ActionResult> InsertFundingApplication(string userName, string date, string billStatus, string accountStatus)
        {
            var user = await _context.Employees.Where(e => e.UserName == userName).SingleAsync();
            var fundingApplication = new FundingApplication
            {
                Employee = user,
                Date = Convert.ToDateTime(date),
                BillStatus = billStatus,
                AccountStatus = accountStatus
            };
            await _context.FundingApplications.AddAsync(fundingApplication);
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}
