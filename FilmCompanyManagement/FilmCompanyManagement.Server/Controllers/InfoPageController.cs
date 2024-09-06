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
        public async Task<ActionResult> GetFundingApplications(string userName)
        {//worker:报销申请
            var fundingApplications = await _context.FundingApplications.Where(fa => fa.Employee.UserName == userName).ToListAsync();
            return Ok(fundingApplications);
        }

        [HttpPost]
        public async Task<ActionResult> GetUnfinishedDrills(string userName)
        {//worker:培训通知
            var user = await _context.Employees.
                Where(d => d.UserName == userName).SingleAsync();
            var unfinishedDrills = await _context.Drills.
                Where(d => DateTime.Now < d.DateTime && d.Employees.Contains(user)).ToListAsync();
            return Ok(unfinishedDrills);
        }

        [HttpPost]
        public async Task<ActionResult> GetWorkersFundingApplications(string userName)
        {//boss:报销申请
            var workers = await _context.Employees.Where(e => e.Department.Leader.UserName == userName).ToListAsync();
            var fundingApplications = new List<FundingApplication>();
            foreach (var worker in workers)
                fundingApplications.AddRange(await _context.FundingApplications.Where(fa => fa.Employee == worker).ToListAsync());
            return Ok(fundingApplications);
        }

        [HttpPost]
        public async Task<ActionResult> GetEquipments(string userName)
        {//boss:设备购买
            var unfinishedProjects = await _context.Projects.Where(p => p.Manager.UserName == userName).ToListAsync();
            return Ok(unfinishedProjects);
        }

        [HttpPost]
        public async Task<ActionResult> GetEquipmentLeases(string userName)
        {//finance:设备租赁
            return Ok(await _context.EquipmentLeases.Where(el => el.OrderStatus == "未完成").ToListAsync());
        }


        [HttpPost]
        public async Task<ActionResult> GetFinishedProducts(string userName)
        {//finance:成片购买
            return Ok(await _context.FinishedProducts.Where(fp => fp.OrderStatus == "未完成").ToListAsync());
        }


        [HttpPost]
        public async Task<ActionResult> GetSalaryBills(string userName)
        {//finance:工资数据
            return Ok(await _context.Employees.Where(e => e.SalaryStatus == "未完成").ToListAsync());
        }

        [HttpPost]
        public async Task<ActionResult> GetInvestments(string userName)
        {//finance:投资数据
            return Ok(await _context.Investments.Where(i => i.BillStatus == "未完成").ToListAsync());
        }

        //考勤板块
        [HttpPost]
        public async Task<ActionResult> IsAttended(string userName)
        {
            var attendance = await _context.Attendances.Where(a => a.Employee.UserName == userName).FirstOrDefaultAsync();
            return Ok(attendance == null);
        }

        [HttpPost]
        public async Task<ActionResult> InsertAttendenceRecord(string userName, int state)
        {
            var date = DateTime.Now;
          
            var attendance = await _context.Attendances.FirstOrDefaultAsync(a => a.Employee.UserName == userName && a.Date == date);
            if (attendance == null)
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
            }
            if (state == 1)//上班
                attendance.CheckInTime = date;
            else if (state == 0)//下班
                attendance.CheckOutTime = date;
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
