using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FilmCompanyManagement.Server.EntityFrame.Models;
using System.Threading.Tasks;
using System.Linq;
using FilmCompanyManagement.Server.EntityFrame;

namespace FilmCompanyManagement.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BusinessManagementController : ControllerBase
    {
        private readonly FCMDbContext _context;

        public BusinessManagementController(FCMDbContext context)
        {
            _context = context;
        }

        // 获取所有设备租赁信息
        [HttpGet("get-equipment-leases")]
        public async Task<IActionResult> GetEquipmentLeases()
        {
            var leases = await _context.EquipmentLeases
                                       .Include(e => e.Employee)
                                       .Include(e => e.Customer)
                                       .Include(e => e.Bill)
                                       .ToListAsync();
            return Ok(new { leases });
        }

        // 提交或更新设备租赁
        [HttpPost("submit-equipment-lease")]
        public async Task<IActionResult> SubmitEquipmentLease([FromBody] EquipmentLease lease)
        {
            if (lease.Id == 0)
            {
                _context.EquipmentLeases.Add(lease);
            }
            else
            {
                _context.EquipmentLeases.Update(lease);
            }
            await _context.SaveChangesAsync();
            return Ok(new { message = "设备租赁记录已保存" });
        }

        // 删除设备租赁
        [HttpPost("delete-equipment-lease")]
        public async Task<IActionResult> DeleteEquipmentLease([FromBody] EquipmentLease lease)
        {
            var leaseToDelete = await _context.EquipmentLeases.FindAsync(lease.Id);
            if (leaseToDelete == null)
            {
                return NotFound("租赁记录未找到");
            }
            _context.EquipmentLeases.Remove(leaseToDelete);
            await _context.SaveChangesAsync();
            return Ok(new { message = "租赁记录已删除" });
        }

        // 获取所有设备维修信息
        [HttpGet("get-equipment-repairs")]
        public async Task<IActionResult> GetEquipmentRepairs()
        {
            var repairs = await _context.EquipmentRepairs
                                        .Include(e => e.Employee)
                                        .Include(e => e.PhotoEquipment)
                                        .Include(e => e.Bill)
                                        .ToListAsync();
            return Ok(new { repairs });
        }

        // 提交或更新设备维修
        [HttpPost("submit-equipment-repair")]
        public async Task<IActionResult> SubmitEquipmentRepair([FromBody] EquipmentRepair repair)
        {
            if (repair.Id == 0)
            {
                _context.EquipmentRepairs.Add(repair);
            }
            else
            {
                _context.EquipmentRepairs.Update(repair);
            }
            await _context.SaveChangesAsync();
            return Ok(new { message = "设备维修记录已保存" });
        }

        // 删除设备维修
        [HttpPost("delete-equipment-repair")]
        public async Task<IActionResult> DeleteEquipmentRepair([FromBody] EquipmentRepair repair)
        {
            var repairToDelete = await _context.EquipmentRepairs.FindAsync(repair.Id);
            if (repairToDelete == null)
            {
                return NotFound("维修记录未找到");
            }
            _context.EquipmentRepairs.Remove(repairToDelete);
            await _context.SaveChangesAsync();
            return Ok(new { message = "维修记录已删除" });
        }

        // 获取所有投资信息
        [HttpGet("get-investments")]
        public async Task<IActionResult> GetInvestments()
        {
            var investments = await _context.Investments
                                            .Include(i => i.Customer)
                                            .Include(i => i.Bill)
                                            .ToListAsync();
            return Ok(new { investments });
        }

        // 提交或更新投资
        [HttpPost("submit-investment")]
        public async Task<IActionResult> SubmitInvestment([FromBody] Investment investment)
        {
            if (investment.Id == 0)
            {
                _context.Investments.Add(investment);
            }
            else
            {
                _context.Investments.Update(investment);
            }
            await _context.SaveChangesAsync();
            return Ok(new { message = "投资记录已保存" });
        }

        // 删除投资记录
        [HttpPost("delete-investment")]
        public async Task<IActionResult> DeleteInvestment([FromBody] Investment investment)
        {
            var investmentToDelete = await _context.Investments.FindAsync(investment.Id);
            if (investmentToDelete == null)
            {
                return NotFound("投资记录未找到");
            }
            _context.Investments.Remove(investmentToDelete);
            await _context.SaveChangesAsync();
            return Ok(new { message = "投资记录已删除" });
        }
    }
}
