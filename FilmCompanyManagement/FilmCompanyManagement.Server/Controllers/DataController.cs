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
            string id = request.id;

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

        //[HttpPost]
        //public async Task<IActionResult> SignData(Sign sign)
        //{
        //    string id = sign.id;
        //    DateTime time = DateTime.Parse(sign.time);
        //    string state = sign.state;

        //    if (state == "1")
        //    {
        //        _context.Attendances.Add(new Attendance
        //        {
        //            Id = sign.time + id,
        //            Date =time,
        //        });
        //        return Ok(new
        //        {
        //            success = 1,
        //            signtime = sign.time
        //        });
        //    }
        //    else
        //    {
        //        var att = await _context.Attendances.SingleAsync(e => e.Id == sign.time + id);
        //        if (att == null) return BadRequest("has not sign in");

        //        return Ok(new
        //        {
        //            success = 1,
        //            signtime=sign.time
        //        });
        //    }

        //}
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