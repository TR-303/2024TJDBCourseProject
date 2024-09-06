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
            int id = request.id;

            var ret = await _context.Employees.SingleAsync(e => e.Id == id);
            if (ret == null) return BadRequest("not exist");

            return Ok(new
            {
                name = ret.Name
            });
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
        public int id { get; set; }
    }
}