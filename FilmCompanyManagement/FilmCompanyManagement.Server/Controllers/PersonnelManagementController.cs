using FilmCompanyManagement.Server.EntityFrame;
using FilmCompanyManagement.Server.EntityFrame.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FilmCompanyManagement.Controllers
{
    [Route("api")]
    [ApiController]
    public class PersonnelManagementController : ControllerBase
    {
        private readonly FCMDbContext _context;

        public PersonnelManagementController(FCMDbContext context)
        {
            _context = context;
        }

        #region 招聘部分

        // 获取招聘人员列表
        [HttpGet("get-recruiters")]
        public async Task<ActionResult<List<Recruiter>>> GetRecruiters()
        {
            var recruiterList = await _context.Recruiters
                .Include(r => r.Position) // 包含职位信息
                .Include(r => r.Interviewer) // 包含面试官信息
                .ToListAsync();
            return Ok(new { employee_list = recruiterList });
        }

        // 获取招聘人员详情
        [HttpPost("details-recruiter")]
        public async Task<ActionResult<Recruiter>> GetRecruiterDetails([FromBody] Recruiter recruiter)
        {
            var employeedata = await _context.Recruiters
                .Include(r => r.Position)
                .Include(r => r.Interviewer)
                .FirstOrDefaultAsync(r => r.Id == recruiter.Id);

            if (employeedata != null)
            {
                return Ok(employeedata);
            }
            return NotFound(new { message = "未找到相关招聘信息" });
        }

        // 提交招聘人员表单
        [HttpPost("submit-recruiter-form")]
        public async Task<IActionResult> SubmitRecruiterForm([FromBody] Recruiter recruiter)
        {
            if (string.IsNullOrEmpty(recruiter.Id))
            {
                return BadRequest(new { message = "ID不能为空" });
            }

            var existingRecruiter = await _context.Recruiters
                .FirstOrDefaultAsync(r => r.Id == recruiter.Id);

            if (existingRecruiter == null)
            {
                // 如果该招聘条目不存在，则创建新的条目
                _context.Recruiters.Add(recruiter);
            }
            else
            {
                // 更新现有的条目
                existingRecruiter.Name = recruiter.Name;
                existingRecruiter.Gender = recruiter.Gender;
                existingRecruiter.DateOfBirth = recruiter.DateOfBirth;
                existingRecruiter.Resume = recruiter.Resume;
                existingRecruiter.Position = recruiter.Position;
                existingRecruiter.Phone = recruiter.Phone;
                existingRecruiter.Email = recruiter.Email;
                existingRecruiter.Interviewer = recruiter.Interviewer;
                existingRecruiter.InterviewStage = recruiter.InterviewStage;
                existingRecruiter.State = recruiter.State;
            }

            await _context.SaveChangesAsync();
            return Ok(new { message = "recruiter提交成功" });
        }

        // 删除招聘人员表单
        [HttpPost("delete-recruiter")]
        public async Task<IActionResult> DeleteRecruiter([FromBody] Recruiter recruiter)
        {
            var existingRecruiter = await _context.Recruiters
                .FirstOrDefaultAsync(r => r.Id == recruiter.Id);

            if (existingRecruiter != null)
            {
                _context.Recruiters.Remove(existingRecruiter);
                await _context.SaveChangesAsync();
                return Ok(new { message = "recruiter删除成功" });
            }

            return NotFound(new { message = "未找到相关招聘信息" });
        }

        #endregion

        #region 人员总览部分

        // 获取人员总览列表
        [HttpGet("get-overview")]
        public async Task<ActionResult> GetOverview()
        {
            var overviewList = await _context.Employees.ToListAsync();
            return Ok(new { employee_list = overviewList });
        }

        // 获取人员总览详情
        [HttpPost("details-overview")]
        public async Task<ActionResult> GetOverviewDetails([FromBody] Employee employee)
        {
            var employeedata = await _context.Employees.FindAsync(employee.Id);
            if (employeedata != null)
            {
                return Ok(new List<Employee> { employeedata });
            }
            return NotFound();
        }

        // 提交人员总览表单
        [HttpPost("submit-overview-form")]
        public async Task<IActionResult> SubmitOverviewForm([FromBody] Employee employee)
        {
            if (employee == null || string.IsNullOrEmpty(employee.Name))
            {
                return BadRequest(new { message = "无效的数据" });
            }

            if (string.IsNullOrEmpty(employee.Id))
            {
                employee.Id = Guid.NewGuid().ToString();
                _context.Employees.Add(employee);
            }
            else
            {
                _context.Employees.Update(employee);
            }
            await _context.SaveChangesAsync();
            return Ok(new { message = "overview提交成功" });
        }

        // 删除人员总览表单
        [HttpPost("delete-overview-form")]
        public async Task<IActionResult> DeleteOverviewForm([FromBody] Employee employee)
        {
            var existingEmployee = await _context.Employees.FindAsync(employee.Id);
            if (existingEmployee != null)
            {
                _context.Employees.Remove(existingEmployee);
                await _context.SaveChangesAsync();
                return Ok(new { message = "overview删除成功" });
            }
            return NotFound();
        }

        #endregion

        #region 员工培训部分

        // 获取员工培训列表
        [HttpGet("get-train")]
        public async Task<ActionResult> GetTrain()
        {
            var trainList = await _context.Drills.ToListAsync();
            return Ok(new { employee_list = trainList });
        }

        // 获取员工培训详情
        [HttpPost("details-train")]
        public async Task<ActionResult> GetTrainDetails([FromBody] Drill drill)
        {
            var traindata = await _context.Drills.FindAsync(drill.Id);
            if (traindata != null)
            {
                return Ok(new List<Drill> { traindata });
            }
            return NotFound();
        }

        // 提交员工培训表单
        [HttpPost("submit-train-form")]
        public async Task<IActionResult> SubmitTrainForm([FromBody] Drill drill)
        {
            if (string.IsNullOrEmpty(drill.Id))
            {
                drill.Id = Guid.NewGuid().ToString();
                _context.Drills.Add(drill);
            }
            else
            {
                _context.Drills.Update(drill);
            }
            await _context.SaveChangesAsync();
            return Ok(new { message = "train提交成功" });
        }

        // 删除员工培训表单
        [HttpPost("delete-train-form")]
        public async Task<IActionResult> DeleteTrainForm([FromBody] Drill drill)
        {
            var existingDrill = await _context.Drills.FindAsync(drill.Id);
            if (existingDrill != null)
            {
                _context.Drills.Remove(existingDrill);
                await _context.SaveChangesAsync();
                return Ok(new { message = "train删除成功" });
            }
            return NotFound();
        }

        #endregion
    }
}
