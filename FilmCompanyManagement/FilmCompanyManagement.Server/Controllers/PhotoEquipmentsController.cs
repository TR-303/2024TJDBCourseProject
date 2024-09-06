using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FilmCompanyManagement.Server.EntityFrame;
using FilmCompanyManagement.Server.EntityFrame.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace FilmCompanyManagement.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PhotoEquipmentsController : ControllerBase
    {

        private readonly FCMDbContext _context;

        public PhotoEquipmentsController(FCMDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<ActionResult> InsertPhotoEquipment(string userName, string equipmentName, string equipmentModel, int price)
        {
            var user = await _context.Employees.Where(e => e.UserName == userName).SingleAsync();
            var bill = new Bill
            {
                Amount = price,
                Type = "InsertPhotoEquipment",
                Date = DateTime.Now
            };
            await _context.Set<Bill>().AddAsync(bill);
            await _context.Set<PhotoEquipment>().AddAsync(new PhotoEquipment
            {
                Name = equipmentName,
                Model = equipmentModel,
                Status = false,
                Employee = user,
                Bill = bill
            });
            await _context.SaveChangesAsync();
            
            return Ok();
        }
    }
}
