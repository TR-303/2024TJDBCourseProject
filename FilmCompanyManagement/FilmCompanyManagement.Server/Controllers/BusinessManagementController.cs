using FilmCompanyManagement.Server.EntityFrame;
using FilmCompanyManagement.Server.EntityFrame.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace FilmCompanyManagement.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BusinessManagementController : ControllerBase
    {
        private readonly FCMDbContext _context;

        public BusinessManagementController(FCMDbContext context)
        {
            _context = context;
        }

        //获取总览
        [HttpGet("get-overview")]
        public async Task<IActionResult> GetOverview()
        {
            var employees = await _context.Employees
                                          .Select(e => new
                                          {
                                              e.Id,
                                              e.Name,
                                              e.Position,
                                              e.Salary,
                                              e.Phone,
                                              e.Email,
                                              e.Gender
                                          })
                                          .ToListAsync();

            return Ok(new { employee_list = employees });
        }

        // 获取外部投资信息
        [HttpGet("get-invest")]
        public async Task<IActionResult> GetInvest()
        {
            var investments = await _context.Investments
                .Select(i => new
                {
                    i.Id,                             // 投资编号
                    i.Bill.AssignDate,                // 投资日期
                    CustomerName = i.Customer.CustomerName,   // 投资方
                    i.Bill.Amount,                    // 投资金额
                    i.Bill.Status                     // 账单状态
                })
                .ToListAsync();

            return Ok(new { businesses_list = investments });
        }

        // 提交外部投资信息
        [HttpPost("submit-invest-form")]
        public async Task<IActionResult> SubmitInvestForm([FromBody] Investment form)
        {
            if (form == null)
                return BadRequest("Invalid data.");

            await _context.Investments.AddAsync(form);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Investment submitted successfully!" });
        }

        // 获取成片购买信息
        [HttpGet("get-buy")]
        public async Task<IActionResult> GetBuy()
        {
            var purchases = await _context.FinishedProducts
                .Select(fp => new
                {
                    fp.Id,                            // 购买编号
                    fp.Bill.AssignDate,               // 购买日期
                    CustomerName = fp.Customer.CustomerName,  // 购买人
                    fp.Bill.Amount,                   // 购买金额
                    fp.Status                         // 订单状态
                })
                .ToListAsync();

            return Ok(new { businesses_list = purchases });
        }

        // 提交成片购买订单
        [HttpPost("submit-buy-form")]
        public async Task<IActionResult> SubmitBuyForm([FromBody] FinishedProduct form)
        {
            if (form == null)
                return BadRequest("Invalid data.");

            await _context.FinishedProducts.AddAsync(form);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Finished product purchase submitted successfully!" });
        }

        // 获取设备租赁信息
        [HttpGet("get-lease")]
        public async Task<IActionResult> GetLease()
        {
            var leases = await _context.EquipmentLeases
                .Select(el => new
                {
                    el.Id,                            // 租赁编号
                    el.Bill.AssignDate,               // 租赁日期
                    CustomerName = el.Customer.CustomerName,  // 租赁人
                    el.Bill.Amount,                   // 租赁金额
                    el.Status                         // 租赁状态
                })
                .ToListAsync();

            return Ok(new { businesses_list = leases });
        }

        // 提交设备租赁信息
        [HttpPost("submit-lease-form")]
        public async Task<IActionResult> SubmitLeaseForm([FromBody] EquipmentLease form)
        {
            if (form == null)
                return BadRequest("Invalid data.");

            await _context.EquipmentLeases.AddAsync(form);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Equipment lease submitted successfully!" });
        }

        // 获取公司项目信息
        [HttpGet("get-project")]
        public async Task<IActionResult> GetProject()
        {
            var projects = await _context.Projects
                .Select(p => new
                {
                    p.Id,                            // 项目编号
                    p.Bill.AssignDate,               // 项目日期
                    CustomerName = p.Customer.CustomerName,  // 项目客户
                    p.Bill.Amount,                   // 项目金额
                    ManagerName = p.Manager.Name,    // 项目负责人
                    p.Status                         // 项目状态
                })
                .ToListAsync();

            return Ok(new { businesses_list = projects });
        }

        // 提交公司项目信息
        [HttpPost("submit-project-form")]
        public async Task<IActionResult> SubmitProjectForm([FromBody] Project form)
        {
            if (form == null)
                return BadRequest("Invalid data.");

            await _context.Projects.AddAsync(form);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Project submitted successfully!" });
        }

        // 删除外部投资信息
        [HttpPost("delete-invest-form")]
        public async Task<IActionResult> DeleteInvestForm([FromBody] Investment form)
        {
            var investment = await _context.Investments.FindAsync(form.Id);
            if (investment == null)
                return NotFound("Investment not found.");

            _context.Investments.Remove(investment);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Investment deleted successfully!" });
        }

        // 删除成片购买信息
        [HttpPost("delete-buy-form")]
        public async Task<IActionResult> DeleteBuyForm([FromBody] FinishedProduct form)
        {
            var purchase = await _context.FinishedProducts.FindAsync(form.Id);
            if (purchase == null)
                return NotFound("Finished product purchase not found.");

            _context.FinishedProducts.Remove(purchase);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Finished product purchase deleted successfully!" });
        }

        // 删除设备租赁信息
        [HttpPost("delete-lease-form")]
        public async Task<IActionResult> DeleteLeaseForm([FromBody] EquipmentLease form)
        {
            var lease = await _context.EquipmentLeases.FindAsync(form.Id);
            if (lease == null)
                return NotFound("Equipment lease not found.");

            _context.EquipmentLeases.Remove(lease);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Equipment lease deleted successfully!" });
        }

        // 删除公司项目信息
        [HttpPost("delete-project-form")]
        public async Task<IActionResult> DeleteProjectForm([FromBody] Project form)
        {
            var project = await _context.Projects.FindAsync(form.Id);
            if (project == null)
                return NotFound("Project not found.");

            _context.Projects.Remove(project);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Project deleted successfully!" });
        }

        // 查看外部投资详情
        [HttpPost("details-invest")]
        public async Task<IActionResult> DetailsInvest([FromBody] Investment form)
        {
            var investment = await _context.Investments
                .Include(i => i.Customer)
                .Include(i => i.Bill)
                .FirstOrDefaultAsync(i => i.Id == form.Id);

            if (investment == null)
                return NotFound("Investment not found.");

            return Ok(new[] { investment });
        }

        // 查看成片购买详情
        [HttpPost("details-buy")]
        public async Task<IActionResult> DetailsBuy([FromBody] FinishedProduct form)
        {
            var purchase = await _context.FinishedProducts
                .Include(fp => fp.Customer)
                .Include(fp => fp.Bill)
                .FirstOrDefaultAsync(fp => fp.Id == form.Id);

            if (purchase == null)
                return NotFound("Finished product purchase not found.");

            return Ok(new[] { purchase });
        }

        // 查看设备租赁详情
        [HttpPost("details-lease")]
        public async Task<IActionResult> DetailsLease([FromBody] EquipmentLease form)
        {
            var lease = await _context.EquipmentLeases
                .Include(el => el.Customer)
                .Include(el => el.Bill)
                .FirstOrDefaultAsync(el => el.Id == form.Id);

            if (lease == null)
                return NotFound("Equipment lease not found.");

            return Ok(new[] { lease });
        }

        // 查看公司项目详情
        [HttpPost("details-project")]
        public async Task<IActionResult> DetailsProject([FromBody] Project form)
        {
            var project = await _context.Projects
                .Include(p => p.Customer)
                .Include(p => p.Bill)
                .Include(p => p.Manager)
                .FirstOrDefaultAsync(p => p.Id == form.Id);

            if (project == null)
                return NotFound("Project not found.");

            return Ok(new[] { project });
        }
    }
}
