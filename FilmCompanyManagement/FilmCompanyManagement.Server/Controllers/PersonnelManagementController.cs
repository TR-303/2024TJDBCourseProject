using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FilmCompanyManagement.Server.EntityFrame.Models;
using System.Threading.Tasks;
using System.Linq;
using FilmCompanyManagement.Server.EntityFrame;

namespace FilmCompanyManagement.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonnelManagementController : ControllerBase
    {
        private readonly FCMDbContext _context;

        public PersonnelManagementController(FCMDbContext context)
        {
            _context = context;
        }

        [HttpGet("get-invite")]
        public async Task<IActionResult> GetInvite()
        {
            var rec = await _context.Recruiters
                .Select(e => new
                {
                    id = e.Id,
                    name = e.Name,
                    gender = e.Gender,
                    state = e.State ? "已录用" : "未录用"
                })
                .ToListAsync();

            return Ok(rec);
        }

        // 获取实习人员信息
        [HttpGet("get-intern")]
        public async Task<IActionResult> GetIntern()
        {
            var interns = await _context.AdvicerIntern.Include(ai => ai.Intern).Include(ai => ai.Advicer)
                .Select(e => new
                {
                    internId = e.InternId,
                    intern = e.Intern.Name,
                    advicer = e.Advicer.Name
                })
                .ToListAsync();

            return Ok(interns);
        }

        // 获取人员总览信息
        [HttpGet("get-overview")]
        public async Task<IActionResult> GetOverview()
        {
            var overview = await _context.Employees
                .Select(e => new
                {
                    id = e.Id,
                    name = e.Name,
                    salary = e.Salary,
                })
                .ToListAsync();

            return Ok(overview);
        }

        // 获取培训员工信息
        [HttpGet("get-train")]
        public async Task<IActionResult> GetTrain()
        {
            var trainedEmployees = await _context.Drills.Include(d => d.Teacher)
                .Select(e => new
                {
                    id = e.Id,
                    teacher = e.Teacher == null ? null : e.Teacher.Name,
                    dateTime = e.StartTime
                })
                .ToListAsync();

            return Ok(trainedEmployees);
        }

        [HttpPost("submit-invite-form")]
        public async Task<IActionResult> SubmitInvite([FromBody] RecruitForm form)
        {

            await _context.Recruiters.AddAsync(new Recruiter
            {
                Name = form.name,
                Gender = form.gender,
                Position = form.positionTitle,
                Salary = form.salary,
                Phone = form.phone,
                Email = form.email,
                Interviewer = null,
                InterviewStage = 0,
                State = false
            });
            await _context.SaveChangesAsync();
            return Ok(new
            {
                message = "提交成功"
            });
        }
    }

    public class RecruitForm
    {
        public int id { get; set; }  // 招聘编号
        public string name { get; set; }  // 招聘人员姓名
        public string gender { get; set; }  // 性别，'男' 或 '女'
        public string positionTitle { get; set; }  // 职位名称
        public int salary { get; set; }  // 工资
        public string phone { get; set; }  // 联系电话
        public string email { get; set; }  // 电子邮件
        public string? interviewer { get; set; }  // 面试官姓名
        public string interviewerStage { get; set; }  // 面试阶段
        public string state { get; set; }  // 录用状态：未录用或已录用
    }
}
