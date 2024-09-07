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
        public async Task<ActionResult> EmployeesInDepartment(departmentRequest request)
        {
            var dept = await _context.Departments.Include(d => d.Employees).SingleAsync(d => d.Name == request.department);
            return Ok(dept.Employees);
        }

        [HttpGet]
        public async Task<ActionResult> DeptLeader(thisRequest request)
        {
            var user = await _context.Employees.Include(e => e.Department).SingleAsync(e => e.UserName == request.userName);
            return Ok(user.Department.Leader);
        }

        public class thisRequest
        {
            public string userName { get; set; }
        }

        public class departmentRequest
        {
            public string department { get; set; }
        }
    }
}
