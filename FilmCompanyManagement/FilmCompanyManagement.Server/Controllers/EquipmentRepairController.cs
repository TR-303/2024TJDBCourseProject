using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FilmCompanyManagement.Server.EntityFrame;
using FilmCompanyManagement.Server.EntityFrame.Models;

namespace FilmCompanyManagement.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class EquipmentRepairController : ControllerBase
    {

        private readonly FCMDbContext _context;

        public EquipmentRepairController(FCMDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<ActionResult> InsertEquipmentRepair(string userName, int equipmentID, int price)
        {
            var user = await _context.Employees.Where(e => e.UserName == userName).SingleAsync();
            var bill = new Bill
            {
                Amount = price,
                Type = "EquipmentRepair",
                AssignDate = DateTime.Now
            };
            await _context.Bills.AddAsync(bill);
            var photoEquipment = await _context.PhotoEquipments.Where(pe => pe.Id == equipmentID).SingleAsync();
            var equipmentRepair = new EquipmentRepair
            {
                PhotoEquipment = photoEquipment,
                Bill = bill,
                Employee = user
            };
            await _context.EquipmentRepairs.AddAsync(equipmentRepair);
            await _context.SaveChangesAsync();
            
            return Ok();
        }
    }
}
