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
        public async Task<ActionResult> EmployeesInDepartment(string department)
        {
            var dept = await _context.Departments.Where(d => d.Name == department).SingleAsync();
            return Ok(dept.Employees);
        }

        [HttpGet]
        public async Task<ActionResult> DeptLeader(string userName)
        {
            var user = await _context.Employees.Where(e => e.UserName == userName).SingleAsync();
            var dept = await _context.Departments.Where(d => d.Employees.Contains(user)).SingleAsync();
            return Ok(dept.LeaderId);
        }
    }
}
