using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FilmCompanyManagement.Server.EntityFrame.Models;
using FilmCompanyManagement.Server.EntityFrame;
using System.Collections.Generic;

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
            return Ok(await _context.FundingApplications.Where(fa => fa.Employee.UserName == userName).ToListAsync());
        }

        [HttpPost]
        public async Task<ActionResult> GetEquipments(string userName)
        {//worker:设备购买
            var unfinishedProjects = await _context.PhotoEquipments.Include(p => p.Employee).Where(p => p.Employee.UserName == userName).ToListAsync();
            return Ok(unfinishedProjects);
        }

        [HttpPost]
        public async Task<ActionResult> GetEquipmentsRepairs(string userName)
        {//worker:设备维修
            var unfinishedProjects = await _context.EquipmentRepairs.Include(p => p.Employee).Where(p => p.Employee.UserName == userName).ToListAsync();
            return Ok(unfinishedProjects);
        }

        [HttpPost]
        public async Task<ActionResult> GetUnfinishedDrills(string userName)
        {//worker:培训通知
            var user = await _context.Employees.Where(d => d.UserName == userName).SingleAsync();
            return Ok(await _context.Drills.Include(d => d.Students).Where(d => DateTime.Now < d.EndTime && d.Students.Contains(user)).ToListAsync());
        }

        [HttpPost]
        public async Task<ActionResult> BossGetFundingApplications(string userName)
        {//boss:报销申请
            var workers = await _context.Employees.Include(e => e.Department).Include(e => e.Department.Leader).Where(e => e.Department.Leader.UserName == userName).ToListAsync();
            var fundingApplications = new List<FundingApplication>();
            foreach (var worker in workers)
                fundingApplications.AddRange(await _context.FundingApplications.Where(fa => fa.Employee == worker).ToListAsync());
            return Ok(fundingApplications);
        }

        [HttpPost]
        public async Task<ActionResult> BossGetEquipments(string userName)
        {//boss:设备购买
            var workers = await _context.Employees.Include(e => e.Department).Include(e => e.Department.Leader).Where(e => e.Department.Leader.UserName == userName).ToListAsync();
            var unfinishedProjects = new List<PhotoEquipment>();
            foreach (var worker in workers)
                unfinishedProjects.AddRange(await _context.PhotoEquipments.Where(pe => pe.Employee == worker).ToListAsync());
            return Ok(unfinishedProjects);
        }

        [HttpPost]
        public async Task<ActionResult> BossGetEquipmentsRepairs(string userName)
        {//boss:设备维修
            var workers = await _context.Employees.Include(e => e.Department).Include(e => e.Department.Leader).Where(e => e.Department.Leader.UserName == userName).ToListAsync();
            var unfinishedProjects = new List<EquipmentRepair>();
            foreach (var worker in workers)
                unfinishedProjects.AddRange(await _context.EquipmentRepairs.Where(pe => pe.Employee == worker).ToListAsync());
            return Ok(unfinishedProjects);
        }

        [HttpPost]
        public async Task<ActionResult> GetEquipmentLeases()
        {//finance:设备租赁
            return Ok(await _context.EquipmentLeases.Where(el => el.Status == "未完成").ToListAsync());
        }


        [HttpPost]
        public async Task<ActionResult> GetFinishedProducts()
        {//finance:成片购买
            return Ok(await _context.FinishedProducts.Include(fp => fp.Bill).Include(fp => fp.Customer).Include(fp => fp.File).Where(fp => fp.Status == 0).ToListAsync());
        }


        [HttpPost]
        public async Task<ActionResult> GetSalaryBills()
        {//finance:工资数据
            return Ok(await _context.Employees.Include(e => e.SalaryBill).Where(e => e.SalaryBill != null).Select(e => e.SalaryBill).ToListAsync());
        }

        [HttpPost]
        public async Task<ActionResult> GetInvestments()//500
        {//finance:投资数据
            var investments = await _context.Investments.Include(i => i.Bill).Where(i => i.Bill.Status == false).ToListAsync();
            return Ok(investments);
        }

        //考勤板块
        [HttpPost]
        public async Task<ActionResult> IsAttended(string userName)
        {
            var attendance = await _context.Attendances.Include(a => a.Employee).Where(a => a.Employee.UserName == userName).FirstOrDefaultAsync();
            return Ok(attendance == null);
        }

        [HttpPost]
        public async Task<ActionResult> InsertAttendenceRecord(string userName, int state)
        {
            var user = await _context.Employees.Where(e => e.UserName == userName).SingleAsync();
            if (await _context.Attendances.Include(a => a.Employee).FirstOrDefaultAsync(a => a.Employee.UserName == userName && a.Date == DateTime.Now) == null)
            {
                await _context.Attendances.AddAsync(new Attendance
                {
                    Date = DateTime.Now,
                    Employee = user,
                    CheckInTime = state == 1 ? DateTime.Now : null,
                    CheckOutTime = state == 0 ? DateTime.Now : null
                });
            }
            await _context.SaveChangesAsync();
            return Ok();
        }

        //绩效板块
        [HttpPost]
        public async Task<ActionResult> GetKPIs(string userName)
        {
            var KPIs = await _context.KPI.Include(k => k.Project).Include(k => k.Project.Manager).
                Where(k => k.Project.Manager.UserName == userName).ToListAsync();
            if (KPIs.Count == 0)
                return Ok();
            var latestDate = KPIs.Max<KPI>(k => (long)k.Date.Subtract(new DateTime(1970, 1, 1)).TotalDays);
            var latestKPI = await _context.KPI.Include(k => k.Project).Include(k => k.Project.Manager).
                Where(k => k.Project.Manager.UserName == userName && (long)k.Date.Subtract(new DateTime(1970, 1, 1)).TotalDays == latestDate).SingleAsync();
            return Ok(latestKPI.Project);
        }
    }
}
