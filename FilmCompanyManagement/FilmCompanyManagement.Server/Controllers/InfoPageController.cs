using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FilmCompanyManagement.Server.EntityFrame.Models;
using FilmCompanyManagement.Server.EntityFrame;

namespace FilmCompanyManagement.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class InfoPageController : ControllerBase
    {

        private readonly FCMDbContext _context;

        public InfoPageController(FCMDbContext context)
        {
            _context = context;
        }

        //通知板块
        [HttpPost]
        public async Task<ActionResult> GetUnfinishedProjects(string userName)
        {
            var unfinishedProjects = await _context.Projects.Where(p => p.Manager.UserName == userName).ToListAsync();
            return Ok(unfinishedProjects);
        }

        [HttpPost]
        public async Task<ActionResult> GetUnfinishedDrills(string userName)//有问题
        {
            var user = await _context.Employees.
                Where(d => d.UserName == userName).SingleAsync();
            var unfinishedDrills = await _context.Drills.
                Where(d => DateTime.Now - d.DateTime < d.TimeSpan && d.Employees.Contains(user) || d.Teacher == user).ToListAsync();
            return Ok(unfinishedDrills);
        }

        [HttpPost]
        public async Task<ActionResult> GetFundingApplications(string userName)
        {
            var fundingApplications = await _context.FundingApplications.Where(fa => fa.Employee.UserName == userName).ToListAsync();
            return Ok(fundingApplications);
        }

        //考勤板块
        [HttpPost]
        public async Task<ActionResult> IsAttended(string userName)
        {
            var attendance = await _context.Attendances.Where(a => a.Employee.UserName == userName).FirstOrDefaultAsync();
            return Ok(attendance == null);
        }

        [HttpPost]
        public async Task<ActionResult> InsertAttendenceRecord(string userName, string time, int state)
        {
            var times = time.Split(' ');
            var date = Convert.ToDateTime(times[0]);
            var accurateTime = TimeSpan.Parse(times[1]);
          
            var attendance = await _context.Attendances.FirstOrDefaultAsync(a => a.Employee.UserName == userName && a.Date == date);
            if (attendance != null)
            {
                if (state == 1)//上班
                    attendance.CheckInTime = accurateTime;
                else if (state == 0)//下班
                    attendance.CheckOutTime = accurateTime;
            }
            else
            {
                attendance = new Attendance
                {
                    Id = "A" + date.ToString("yyyyMMdd") + userName,
                    Date = date,
                    IsLate = false,
                    IsEarlyLeave = false,
                    IsOnLeave = false,
                    IsOvertime = false
                };
                await _context.Attendances.AddAsync(attendance);
                if (state == 1)//上班
                    attendance.CheckInTime = accurateTime;
                else if (state == 0)//下班
                    attendance.CheckOutTime = accurateTime;
            }
            await _context.SaveChangesAsync();
            return Ok();
        }

        //绩效板块
        [HttpPost]
        public async Task<ActionResult> GetKPIs(string userName)//等待数据库数据
        {
            var KPIs = await _context.KPI.Where(fa => fa.Project.Manager.UserName == userName).ToListAsync();
            if (KPIs.Count == 0)
                return Ok();
            var latestDate = KPIs.Max<KPI>(k => (long)k.Date.Subtract(new DateTime(1970, 1, 1)).TotalDays);
            var latestKPI = await _context.KPI.Where(k => k.Project.Manager.UserName == userName && (long)k.Date.Subtract(new DateTime(1970, 1, 1)).TotalDays == latestDate).SingleAsync();
            return Ok(latestKPI.Project);
        }
    }
}
