using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FilmCompanyManagement.Server.EntityFrame.Models;
using System.Linq;
using System.Threading.Tasks;
using FilmCompanyManagement.Server.EntityFrame;

namespace FilmCompanyManagement.Server.Controllers
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

        // 获取所有申请
        [HttpGet("requisition")]
        public async Task<IActionResult> GetRequisitions()
        {
            var requisitions = await _context.FundingApplications.Include(f => f.Bill).Include(f=>f.Employee)
                .Select(f => new
                {
                    id = f.Id,
                    employee = f.Employee.Name ,
                    type = f.Opinion,
                    billAmount = f.Bill.Amount,
                    billDate = f.Bill.AssignDate.ToString(),
                    status = f.Bill.Status ? "已处理" : "未处理"
                })
                .ToListAsync();
            return Ok(new { requisition = requisitions });
        }

        // 提交或更新申请
        [HttpPost("submit-req-form")]
        public async Task<IActionResult> SubmitRequisition([FromBody] FundingApplication application)
        {
            if (application.Id == 0)
            {
                _context.FundingApplications.Add(application);
            }
            else
            {
                _context.FundingApplications.Update(application);
            }

            await _context.SaveChangesAsync();
            return Ok(new { message = "申请已成功提交" });
        }

        // 删除申请
        [HttpPost("delete-form")]
        public async Task<IActionResult> DeleteRequisition([FromBody] FundingApplication application)
        {
            var applicationToDelete = await _context.FundingApplications.FindAsync(application.Id);
            if (applicationToDelete == null)
            {
                return NotFound("申请未找到");
            }

            _context.FundingApplications.Remove(applicationToDelete);
            await _context.SaveChangesAsync();
            return Ok(new { message = "申请已删除" });
        }

        // 获取申请详情
        [HttpPost("details-req-form")]
        public async Task<IActionResult> GetRequisitionDetails([FromBody] int id)
        {
            var application = await _context.FundingApplications
                                            .Include(f => f.Employee) // 包含申请人信息
                                            .Include(f => f.Bill) // 包含账单信息
                                            .FirstOrDefaultAsync(f => f.Id == id);

            if (application == null)
            {
                return NotFound("申请未找到");
            }

            return Ok(new[] { application });
        }
    }
}
