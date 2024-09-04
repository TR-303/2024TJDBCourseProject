using FilmCompanyManagement.Server.EntityFrame;
using FilmCompanyManagement.Server.EntityFrame.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FilmCompanyManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApplicationApprovalController : ControllerBase
    {
        private readonly FCMDbContext _context;

        public ApplicationApprovalController(FCMDbContext context)
        {
            _context = context;
        }

        // 获取经费申请列表
        [HttpGet("ApplicationList")]
        public async Task<ActionResult<List<FundingApplication>>> ApplicationList()
        {
            var applications = await _context.FundingApplications.ToListAsync();
            return Ok(applications);
        }

        // 更新申请状态
        [HttpPost("ApplicationReply/{id}")]
        public async Task<IActionResult> ApplicationReply(int id, string status, [FromBody] string remark)
        {
            var application = await _context.FundingApplications.FindAsync(id);
            if (application == null)
            {
                return NotFound("Application not found.");
            }

            // 更新申请状态和备注
            application.BillStatus = status;

            await _context.SaveChangesAsync();
            return Ok();
        }

        // 删除申请
        [HttpPost("ApplicationDelete/{id}")]
        public async Task<IActionResult> ApplicationDelete(int id)
        {
            var application = await _context.FundingApplications.FindAsync(id);
            if (application == null)
            {
                return NotFound("Application not found.");
            }

            _context.FundingApplications.Remove(application);
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}
