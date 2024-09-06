using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FilmCompanyManagement.Server.EntityFrame;
using FilmCompanyManagement.Server.EntityFrame.Models;

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
            await _context.AddAsync(new PhotoEquipment
            {
                Id = "PE" + DateTime.Now.ToString("yyyyMMddhhmmss"),
                Name = equipmentName,
                Model = equipmentModel,
                Price = price,
                Status = 0,
                Employee = user
            });
            await _context.SaveChangesAsync();
            
            return Ok();
        }
    }
}
