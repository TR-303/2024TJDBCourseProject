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

        // 1. 获取员工总览
        [HttpGet("get-overview")]
        public async Task<IActionResult> GetOverview()
        {
            var employees = await _context.Employees.Include(e => e.Department).ToListAsync();
            return Ok(new { employee_list = employees });
        }

        // 2. 提交员工总览表单（创建或更新员工信息）
        [HttpPost("submit-overview-form")]
        public async Task<IActionResult> SubmitOverviewForm([FromBody] Employee employee)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid data.");
            }

            if (_context.Employees.Any(e => e.Id == employee.Id))
            {
                _context.Employees.Update(employee);
            }
            else
            {
                _context.Employees.Add(employee);
            }
            await _context.SaveChangesAsync();

            return Ok(new { message = "员工数据提交成功" });
        }

        // 3. 删除员工
        [HttpPost("delete-overview-form")]
        public async Task<IActionResult> DeleteEmployee([FromBody] Employee employee)
        {
            var employeeToDelete = await _context.Employees.FindAsync(employee.Id);
            if (employeeToDelete == null)
            {
                return NotFound();
            }

            _context.Employees.Remove(employeeToDelete);
            await _context.SaveChangesAsync();
            return Ok(new { message = "员工删除成功" });
        }

        // 4. 获取员工详情
        [HttpPost("details-overview")]
        public async Task<IActionResult> GetEmployeeDetails([FromBody] int id)
        {
            var employee = await _context.Employees.Include(e => e.Department)
                                                   .Include(e => e.Drills) // 员工参与的课程
                                                   .Include(e => e.Teachs) // 员工教授的课程
                                                   .FirstOrDefaultAsync(e => e.Id == id);
            if (employee == null)
            {
                return NotFound();
            }
            return Ok(new[] { employee });
        }

        // 5. 获取培训课程列表
        [HttpGet("get-train")]
        public async Task<IActionResult> GetTrainingSessions()
        {
            var drills = await _context.Drills.Include(d => d.Students) // 包含学生信息
                                              .Include(d => d.Teacher)  // 包含教师信息
                                              .ToListAsync();
            return Ok(new { drills });
        }

        // 6. 创建或更新培训课程
        [HttpPost("submit-train-form")]
        public async Task<IActionResult> SubmitTrainingForm([FromBody] Drill drill)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid data.");
            }

            if (_context.Drills.Any(d => d.Id == drill.Id))
            {
                _context.Drills.Update(drill);
            }
            else
            {
                _context.Drills.Add(drill);
            }
            await _context.SaveChangesAsync();

            return Ok(new { message = "培训数据提交成功" });
        }

        // 7. 删除培训课程
        [HttpPost("delete-train-form")]
        public async Task<IActionResult> DeleteTraining([FromBody] Drill drill)
        {
            var drillToDelete = await _context.Drills.FindAsync(drill.Id);
            if (drillToDelete == null)
            {
                return NotFound();
            }

            _context.Drills.Remove(drillToDelete);
            await _context.SaveChangesAsync();
            return Ok(new { message = "培训记录删除成功" });
        }

        // 8. 获取培训课程详情
        [HttpPost("details-train")]
        public async Task<IActionResult> GetTrainingDetails([FromBody] int id)
        {
            var drill = await _context.Drills.Include(d => d.Students) // 包含学生信息
                                             .Include(d => d.Teacher)  // 包含教师信息
                                             .FirstOrDefaultAsync(d => d.Id == id);
            if (drill == null)
            {
                return NotFound();
            }
            return Ok(new[] { drill });
        }

        // 9. 添加学生到培训课程
        [HttpPost("add-student-to-drill")]
        public async Task<IActionResult> AddStudentToDrill([FromBody] AddStudentToDrillRequest request)
        {
            var drill = await _context.Drills.Include(d => d.Students)
                                             .FirstOrDefaultAsync(d => d.Id == request.DrillId);
            if (drill == null)
            {
                return NotFound("课程未找到");
            }

            var student = await _context.Employees.FindAsync(request.StudentId);
            if (student == null)
            {
                return NotFound("员工未找到");
            }

            drill.Students.Add(student);
            await _context.SaveChangesAsync();

            return Ok(new { message = "学生添加成功" });
        }

        // 10. 从培训课程中移除学生
        [HttpPost("remove-student-from-drill")]
        public async Task<IActionResult> RemoveStudentFromDrill([FromBody] AddStudentToDrillRequest request)
        {
            var drill = await _context.Drills.Include(d => d.Students)
                                             .FirstOrDefaultAsync(d => d.Id == request.DrillId);
            if (drill == null)
            {
                return NotFound("课程未找到");
            }

            var student = await _context.Employees.FindAsync(request.StudentId);
            if (student == null)
            {
                return NotFound("员工未找到");
            }

            drill.Students.Remove(student);
            await _context.SaveChangesAsync();

            return Ok(new { message = "学生移除成功" });
        }
    }

    // 请求模型
    public class AddStudentToDrillRequest
    {
        public int DrillId { get; set; } // 培训课程ID
        public int StudentId { get; set; } // 学生ID
    }
}
