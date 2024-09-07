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
        public async Task<ActionResult> GetFundingApplications(thisRequest userName)
        {//worker:报销申请
            return Ok(await _context.FundingApplications.Include(fa => fa.Employee).Include(fa => fa.Bill).Where(fa => fa.Employee.UserName == userName.id).ToListAsync());
        }

        [HttpPost]
        public async Task<ActionResult> GetEquipments(thisRequest userName)
        {//worker:设备购买
            var unfinishedProjects = await _context.PhotoEquipments.Include(p => p.Employee).Include(p => p.Bill).Where(p => p.Employee.UserName == userName.id).ToListAsync();
            return Ok(unfinishedProjects);
        }

        [HttpPost]
        public async Task<ActionResult> GetEquipmentsRepairs(thisRequest userName)
        {//worker:设备维修
            var unfinishedProjects = await _context.EquipmentRepairs.Include(p => p.Employee).Include(p => p.PhotoEquipment).Include(p => p.Bill).Where(p => p.Employee.UserName == userName.id).ToListAsync();
            return Ok(unfinishedProjects);
        }

        [HttpPost]
        public async Task<ActionResult> GetUnfinishedDrills(thisRequest userName)
        {//worker:培训通知
            var user = await _context.Employees.Where(d => d.UserName == userName.id).SingleAsync();
            return Ok(await _context.Drills.Include(d => d.Students).Include(d => d.Teacher).Include(d => d.Students).Where(d => DateTime.Now < d.EndTime && d.Students.Contains(user)).ToListAsync());
        }

        [HttpPost]
        public async Task<ActionResult> BossGetFundingApplications(thisRequest userName)
        {//boss:报销申请
            var workers = await _context.Employees.Include(e => e.Department).Include(e => e.Department.Leader).Where(e => e.Department.Leader.UserName == userName.id).ToListAsync();
            var fundingApplications = new List<FundingApplication>();
            foreach (var worker in workers)
                fundingApplications.AddRange(await _context.FundingApplications.Include(fa => fa.Employee).Include(fa => fa.Bill).Where(fa => fa.Employee == worker).ToListAsync());
            return Ok(fundingApplications);
        }

        [HttpPost]
        public async Task<ActionResult> BossGetEquipments(thisRequest userName)
        {//boss:设备购买
            var workers = await _context.Employees.Include(e => e.Department).Include(e => e.Department.Leader).Where(e => e.Department.Leader.UserName == userName.id).ToListAsync();
            var photoEquipments = new List<PhotoEquipment>();
            foreach (var worker in workers)
                photoEquipments.AddRange(await _context.PhotoEquipments.Include(pe => pe.Employee).Include(pe => pe.Bill).Where(pe => pe.Employee == worker).ToListAsync());
            return Ok(photoEquipments);
        }

        [HttpPost]
        public async Task<ActionResult> BossGetEquipmentsRepairs(thisRequest userName)
        {//boss:设备维修
            var workers = await _context.Employees.Include(e => e.Department).Include(e => e.Department.Leader).Where(e => e.Department.Leader.UserName == userName.id).ToListAsync();
            var equipmentRepairs = new List<EquipmentRepair>();
            foreach (var worker in workers)
                equipmentRepairs.AddRange(await _context.EquipmentRepairs.Include(er => er.Employee).Include(er => er.Bill).Include(er => er.PhotoEquipment).Where(pe => pe.Employee == worker).ToListAsync());
            return Ok(equipmentRepairs);
        }

        [HttpPost]
        public async Task<ActionResult> GetEquipmentLeases()
        {//finance:设备租赁
            return Ok(await _context.EquipmentLeases.Include(el => el.Employee).Include(el => el.Bill).Include(el => el.Customer).Where(el => el.Status == "未完成").ToListAsync());
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
            return Ok(await _context.Investments.Include(i => i.Bill).Include(i => i.Customer).Where(i => i.Bill.Status == false).ToListAsync());
        }

        //考勤板块
        [HttpPost]
        public async Task<ActionResult> IsAttended(thisRequest userName)
        {
            return Ok(await _context.Attendances.Include(a => a.Employee).Where(a => a.Employee.UserName == userName.id ).FirstOrDefaultAsync() == null);
        }

        [HttpPost]
        public async Task<ActionResult> InsertAttendenceRecord(thisRequest userName, int state)
        {
            var user = await _context.Employees.Where(e => e.UserName == userName.id).SingleAsync();
            if (await _context.Attendances.Include(a => a.Employee).FirstOrDefaultAsync(a => a.Employee.UserName == userName.id && a.Date == DateTime.Now) == null)
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
        public async Task<ActionResult> GetKPIs(thisRequest userName)
        {
            var KPIs = await _context.KPI.Include(k => k.Project).Include(k => k.Project.Manager).
                Where(k => k.Project.Manager.UserName == userName.id).ToListAsync();
            if (KPIs.Count == 0)
                return Ok();
            var latestDate = KPIs.Max<KPI>(k => (long)k.Date.Subtract(new DateTime(1970, 1, 1)).TotalDays);
            var latestKPI = await _context.KPI.Include(k => k.Project).Include(k => k.Project.Manager).Include(k => k.Project.Bill).Include(k => k.Project.Customer).Include(k => k.Project.Employees).
                Where(k => k.Project.Manager.UserName == userName.id && (long)k.Date.Subtract(new DateTime(1970, 1, 1)).TotalDays == latestDate).SingleAsync();
            return Ok(latestKPI.Project);
        }
        public class thisRequest
        {
            public string id { get; set; }
        }
    }
}
