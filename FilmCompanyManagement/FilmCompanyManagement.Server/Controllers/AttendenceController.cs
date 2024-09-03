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
            var attendance = await _context.Attendances.Where(a => a.Employee.UserName == userName).SingleAsync();
            return attendance == null ? "false" : "true";
        }

        [HttpPost]
        public async Task<string> InsertAttendenceRecord(string userName, string time, int state)
        {
            await Task.Run(() =>
            {
                var times = time.Split(' ');
                var date = Convert.ToDateTime(times[0]);
                var accurateTime = TimeSpan.Parse(times[1]);
                var attendance = _context.Attendances.Where(a => a.Employee.UserName == userName && a.Date = date).SingleAsync();
                if (attendance == null)
                {
                    attendance = new Attendance
                    {
                        Date = date;
                        IsLate = 0;
                        IsEarlyLeave = 0;
                        IsOnLeave = 0;
                        IsOvertime = 0;
                    };
                    _context.AddAsync(attendance);
                }
                if (state == 1)//上班
                    attendance.CheckInTime = accurateTime;
                else if (state == 0)//下班
                    attendance.CheckOutTime = accurateTime;
                _context.SaveChanfesAsync();
        `   }
            return "1";
        }
    }
}
