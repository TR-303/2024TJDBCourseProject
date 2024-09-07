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
                Interviewer = _context.Employees.Single(e => e.Id == form.interviewer),
                InterviewStage = form.interviewerStage == "一面" ? 1 : 2,
                State = form.state == "已录用"
            });
            await _context.SaveChangesAsync();
            return Ok(new
            {
                message = "提交成功"
            });
        }

        [HttpPost("submit-intern-form")]
        public async Task<IActionResult> SubmitIntern([FromBody] InternForm form)
        {
            await _context.AdvicerIntern.AddAsync(new AdvicerIntern
            {
                InternId = form.internId,
                Intern = _context.Employees.Single(e=>e.Name==form.intern),
                AdvicerId = form.advicerId,
                Advicer = _context.Employees.Single(e => e.Name == form.advicer),
                InternshipStartDate = form.internshipStartDate,
                InternshipEndDate = form.internshipEndDate,
                Remarks = form.remarks
            });
            await _context.SaveChangesAsync();
            return Ok(new
            {
                message = "提交成功"
            });
        }
        [HttpPost("submit-overview-form")]
        public async Task<IActionResult> SubmitOverview([FromBody] OverviewForm form)
        {
            await _context.Employees.AddAsync(new Employee
            {
                Name= form.name,
                Gender= form.gender,
                Position= form.position,
                Phone= form.phone,
                Email= form.email,
                Salary  = form.salary,
                Department=_context.Departments.Single(d=>d.Name==form.department),
            });
            await _context.SaveChangesAsync();
            return Ok(new
            {
                message = "提交成功"
            });
        }
        [HttpPost("submit-train-form")]
        public async Task<IActionResult> SubmitTrain([FromBody] TrainingForm form)
        {
            await _context.Drills.AddAsync(new Drill
            {
                Teacher=_context.Employees.Single(e=>e.Id==form.teacher),
                StartTime= form.dateTime,
                EndTime= form.endTime,
            });
            await _context.SaveChangesAsync();
            return Ok(new
            {
                message = "提交成功"
            });
        }

        [HttpPost("delete-invite-form")]
        public async Task<IActionResult> DeleteInvite([FromBody]InviteRow row)
        {
            var invite = await _context.Recruiters.FindAsync(row.id);
            if (invite == null)
            {
                return NotFound(new { message = "招聘记录未找到" });
            }

            _context.Recruiters.Remove(invite);
            await _context.SaveChangesAsync();
            return Ok(new { message = "招聘记录已删除" });
        }

        [HttpPost("delete-intern-form")]
        public async Task<IActionResult> DeleteIntern([FromBody] InternRow row)
        {
            var interns = await _context.AdvicerIntern
                .Where(i => i.InternId == row.internId)
                .ToListAsync();

            if (interns == null || interns.Count == 0)
            {
                return NotFound(new { message = "实习记录未找到" });
            }

            _context.AdvicerIntern.RemoveRange(interns);
            await _context.SaveChangesAsync();
            return Ok(new { message = "符合条件的实习记录已删除" });
        }


        [HttpPost("delete-overview-form")]
        public async Task<IActionResult> DeleteOverview([FromBody]EmployeeRow row)
        {
            var employee = await _context.Employees.FindAsync(row.id);
            if (employee == null)
            {
                return NotFound(new { message = "员工记录未找到" });
            }

            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();
            return Ok(new { message = "员工记录已删除" });
        }

        [HttpPost("delete-train-form")]
        public async Task<IActionResult> DeleteTraining([FromBody]TrainingRow row)
        {
            var training = await _context.Drills.FindAsync(row.id);
            if (training == null)
            {
                return NotFound(new { message = "培训记录未找到" });
            }

            _context.Drills.Remove(training);
            await _context.SaveChangesAsync();
            return Ok(new { message = "培训记录已删除" });
        }

        [HttpPost("details-invite")]
        public async Task<IActionResult> CreateOrUpdateInvite([FromBody] RecruitForm form)
        {
            // 查找现有的记录
            var invite = await _context.Recruiters.FindAsync(form.id);

            // 如果没有记录，说明是新建操作
            if (invite == null)
            {
                // 新建招聘信息
                await _context.Recruiters.AddAsync(new Recruiter
                {
                    Name = form.name,
                    Gender = form.gender,
                    Position = form.positionTitle,
                    Salary = form.salary,
                    Phone = form.phone,
                    Email = form.email,
                    Interviewer = _context.Employees.Single(e => e.Id == form.interviewer), // 根据面试官 ID 找到面试官
                    InterviewStage = form.interviewerStage == "一面" ? 1 : 2,
                    State = form.state == "已录用"
                });

                await _context.SaveChangesAsync();
                return Ok(new
                {
                    message = "提交成功"
                });
            }
            else // 如果找到了记录，执行更新操作
            {
                invite.Name = form.name;
                invite.Gender = form.gender;
                invite.Position = form.positionTitle;
                invite.Salary = form.salary;
                invite.Phone = form.phone;
                invite.Email = form.email;
                invite.Interviewer = await _context.Employees.FindAsync(form.interviewer); // 根据面试官 ID 找到面试官
                invite.InterviewStage = form.interviewerStage == "一面" ? 1 : 2;
                invite.State = form.state == "已录用";

                await _context.SaveChangesAsync();

                return Ok(new
                {
                    message = "更新成功"
                });
            }
        }
        [HttpPost("details-intern")]
        public async Task<IActionResult> GetInternDetails([FromBody] IdRequest request)
        {
            var intern = await _context.AdvicerIntern
                .Where(ai => ai.Intern.Id == request.Id)
                .Select(ai => new
                {
                    InternId = ai.Intern.Id,
                    Intern = ai.Intern.Name,
                    AdvicerId = ai.Advicer.Id,
                    Advicer = ai.Advicer.Name,
                    InternshipStartDate = ai.InternshipStartDate,
                    InternshipEndDate = ai.InternshipEndDate,
                    Remarks = ai.Remarks
                })
                .FirstOrDefaultAsync();

            if (intern == null)
                return NotFound(new { message = "实习信息未找到" });

            return Ok(new[] { intern });
        }
        [HttpPost("details-overview")]
        public async Task<IActionResult> GetOverviewDetails([FromBody] IdRequest request)
        {
            var employee = await _context.Employees
                .Where(e => e.Id == request.Id)
                .Include(e => e.SalaryBill)  // 连表查询 Bill
                .Select(e => new
                {
                    e.Id,
                    e.Name,
                    e.Gender,
                    e.Position,
                    e.Phone,
                    e.Email,
                    e.Salary,
                    BillId = e.SalaryBill == null ? null : (int?)e.SalaryBill.Id,
                    BillAmount = e.SalaryBill == null ? null : (decimal?)e.SalaryBill.Amount,
                    BillType = e.SalaryBill == null ? null : e.SalaryBill.Type,
                    BillDate = e.SalaryBill == null ? null : (DateTime?)e.SalaryBill.AssignDate,
                    BillStatus = e.SalaryBill == null ? null : (e.SalaryBill.Status ? "已处理" : "未处理"),
                    e.KPI,
                    e.Department
                })
                .FirstOrDefaultAsync();

            if (employee == null)
                return NotFound(new { message = "员工信息未找到" });

            return Ok(new[] { employee });
        }

        [HttpPost("details-train")]
        public async Task<IActionResult> GetTrainDetails([FromBody] IdRequest request)
        {
            var training = await _context.Drills
                .Where(d => d.Id == request.Id)
                .Select(d => new
                {
                    d.Id,
                    TeacherId = d.Teacher == null ? null : (int?)d.Teacher.Id,  // Teacher 可能为 null
                    Teacher = d.Teacher == null ? "未指定" : d.Teacher.Name,   // 如果为 null，则返回 "未指定"
                    DateTime = d.StartTime,
                    EndTime = d.EndTime,
                })
                .FirstOrDefaultAsync();

            if (training == null)
                return NotFound(new { message = "培训信息未找到" });

            return Ok(new[] { training });
        }



    }
    public class IdRequest
    {
        public int Id { get; set; }
    }

    public class RecruitForm
    {
        public string id { get; set; }  // 招聘编号
        public string name { get; set; }  // 姓名
        public string gender { get; set; }  // 性别
        public string positionTitle { get; set; }  // 职位名称
        public decimal salary { get; set; }  // 工资
        public string phone { get; set; }  // 联系电话
        public string email { get; set; }  // 电子邮件
        public int interviewer { get; set; }  // 面试官编号
        public string interviewerStage { get; set; }  // 面试阶段
        public string state { get; set; }  // 录用状态
    }
    public class InternForm
    {
        public int internId { get; set; }  // 实习生编号
        public string intern { get; set; }  // 实习生姓名
        public int advicerId { get; set; }  // 指导老师编号
        public string advicer { get; set; }  // 指导老师姓名
        public DateTime internshipStartDate { get; set; }  // 实习开始日期
        public DateTime internshipEndDate { get; set; }  // 实习结束日期
        public string remarks { get; set; }  // 备注信息
    }
    public class OverviewForm
    {
        public string id { get; set; }  // 员工编号
        public string name { get; set; }  // 员工姓名
        public string gender { get; set; }  // 性别
        public string position { get; set; }  // 职位
        public string phone { get; set; }  // 电话
        public string email { get; set; }  // 邮箱
        public int salary { get; set; }  // 薪水
        public string department { get; set; }  // 部门
    }
    public class TrainingForm
    {
        public string id { get; set; }  // 培训编号
        public int teacher { get; set; }  // 培训讲师
        public DateTime dateTime { get; set; }  // 培训开始时间
        public DateTime endTime { get; set; }  // 培训结束时间
        public List<int> employees { get; set; }  // 参与员工编号列表
    }

    public class InviteRow
    {
        public int id { get; set; } // 招聘编号
        public string name { get; set; } // 姓名
        public string gender { get; set; } // 性别
        public string state { get; set; } // 招聘状态
    }

    public class InternRow
    {
        public int internId { get; set; } // 实习生编号
        public string intern { get; set; } // 姓名
        public string advicer { get; set; } // 指导老师
    }
    public class EmployeeRow
    {
        public int id { get; set; } // 员工编号
        public string name { get; set; } // 员工姓名
        public int salary { get; set; } // 员工薪水
    }
    public class TrainingRow
    {
        public int id { get; set; } // 培训编号
        public string teacher { get; set; } // 授课老师
        public string dateTime { get; set; } // 授课日期
    }

}
