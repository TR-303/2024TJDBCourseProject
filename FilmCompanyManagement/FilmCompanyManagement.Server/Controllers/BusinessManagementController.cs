using FilmCompanyManagement.Server.EntityFrame;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FilmCompanyManagement.Controllers
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

        // 获取业务列表（投资和设备租赁）
        [HttpGet("BusinessList")]
        public async Task<ActionResult> BusinessList(string type)
        {
            switch (type)
            {
                case "Investment":
                    var investments = await _context.Investments.ToListAsync();
                    return Ok(investments);
                case "EquipmentLease":
                    var equipmentLeases = await _context.EquipmentLeases.ToListAsync();
                    return Ok(equipmentLeases);
                default:
                    return BadRequest("Invalid business type.");
            }
        }

        // 更新业务信息（投资或设备租赁）
        [HttpPost("BusinessInformation/{type}/{id}")]
        public async Task<IActionResult> BusinessInformation(string type, int id, [FromBody] object businessData)
        {
            switch (type)
            {
                case "Investment":
                    var investment = await _context.Investments.FindAsync(id);
                    if (investment == null) return NotFound("Investment not found.");
                    // 假设 businessData 是传入的修改数据，进行处理
                    // 更新投资相关字段
                    _context.Entry(investment).CurrentValues.SetValues(businessData);
                    break;

                case "EquipmentLease":
                    var lease = await _context.EquipmentLeases.FindAsync(id);
                    if (lease == null) return NotFound("Equipment Lease not found.");
                    // 假设 businessData 是传入的修改数据，进行处理
                    // 更新租赁相关字段
                    _context.Entry(lease).CurrentValues.SetValues(businessData);
                    break;

                default:
                    return BadRequest("Invalid business type.");
            }

            await _context.SaveChangesAsync();
            return Ok();
        }

        // 删除业务信息（投资或设备租赁）
        [HttpPost("BusinessDelete/{type}/{id}")]
        public async Task<IActionResult> BusinessDelete(string type, int id)
        {
            switch (type)
            {
                case "Investment":
                    var investment = await _context.Investments.FindAsync(id);
                    if (investment == null) return NotFound("Investment not found.");
                    _context.Investments.Remove(investment);
                    break;

                case "EquipmentLease":
                    var lease = await _context.EquipmentLeases.FindAsync(id);
                    if (lease == null) return NotFound("Equipment Lease not found.");
                    _context.EquipmentLeases.Remove(lease);
                    break;

                default:
                    return BadRequest("Invalid business type.");
            }

            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}
