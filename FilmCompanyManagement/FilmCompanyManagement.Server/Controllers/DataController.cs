using FilmCompanyManagement.Server.EntityFrame;
using FilmCompanyManagement.Server.EntityFrame.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FilmCompanyManagement.Controllers
{
    [ApiController, Route("/api/data")]
    public class DataController : ControllerBase
    {
        private readonly FCMDbContext _context;

        public DataController(FCMDbContext context)
        {
            _context = context;
        }

        [HttpPost("userdata")]
        public async Task<IActionResult> UserData(UserDataRequest request)
        {
            var ret = await _context.Employees.Include(e => e.Department).SingleAsync(e => e.UserName == request.id);
            if (ret == null) return BadRequest("not exist");
            return Ok(ret) ;
        }

        [HttpPost("departmentuserdata")]
        public async Task<IActionResult> DepartmentUserData(DepartmentRequest request)
        {
            string department = request.department;

            var dept = await _context.Departments.Include(d => d.Employees).SingleAsync(d => d.Name == department);
            if (dept == null) return BadRequest("no department");

            var ret = dept.Employees.Select(e => new
            {
                department = e.Department.Name,
                id = e.Id,
                phone = e.Phone
            }).ToList();
            return Ok(ret);
        }
    }

    public class DepartmentRequest
    {
        public string department { get; set; }
    }

    public class UserDataRequest
    {
        public string id { get; set; }
    }
}