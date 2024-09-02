using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FilmCompanyManagement.Server.EntityFrame.Models;
using System.Threading.Tasks;
using System.Text;
using System.Security.Cryptography;
using FilmCompanyManagement.Server.EntityFrame;
using Microsoft.Extensions.Configuration;
using Oracle.ManagedDataAccess.Client;
using System;

namespace FilmCompanyManagement.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AttendanceController : ControllerBase
    {

        private readonly FCMDbContext _context;

        public AttendanceController(FCMDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<string> IsAttended(string userName)
        {
            var attendances = await _context.Attendances.Where(a => a.Employee.UserName == userName).ToListAsync();
            return attendances.Count == 0 ? "false" : "true";
        }
    }
}
