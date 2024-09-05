using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FilmCompanyManagement.Server.EntityFrame;
using FilmCompanyManagement.Server.EntityFrame.Models;

namespace FilmCompanyManagement.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {

        private readonly FCMDbContext _context;

        public DepartmentController(FCMDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<ActionResult> SelectAllEmployees(string department)
        {
            var dept = await _context.Departments.Where(d => d.Name == department).SingleAsync();
            return Ok(dept.Employees);
        }
    }
}
