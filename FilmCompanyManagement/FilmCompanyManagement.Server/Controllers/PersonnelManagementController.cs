using FilmCompanyManagement.Server.EntityFrame;
using FilmCompanyManagement.Server.EntityFrame.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FilmCompanyManagement.Controllers
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

        // 获取招聘人员列表
        [HttpGet("RecruitmentList")]
        public async Task<ActionResult<List<Employee>>> RecruitmentList()
        {
            return await _context.Employees.Where(e => e.Position == "Recruitment").ToListAsync();
        }

        // 更新招聘人员信息
        [HttpPost("RecruitmentInformation/{id}")]
        public async Task<IActionResult> RecruitmentInformation(string id, [FromBody] Employee employee)
        {
            if (id == "000")
            {
                _context.Employees.Add(employee);
            }
            else
            {
                var existingEmployee = await _context.Employees.FindAsync(id);
                if (existingEmployee == null) return NotFound();
                existingEmployee.Name = employee.Name;
                existingEmployee.Position = employee.Position;
                // 更新其他字段...
            }
            await _context.SaveChangesAsync();
            return Ok();
        }

        // 删除招聘人员
        [HttpPost("RecruitmentDelete/{id}")]
        public async Task<IActionResult> RecruitmentDelete(string id)
        {
            var employee = await _context.Employees.FindAsync(id);
            if (employee == null) return NotFound();
            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();
            return Ok();
        }

        // 获取实习人员列表
        [HttpGet("TraineeList")]
        public async Task<ActionResult<List<Employee>>> TraineeList()
        {
            return await _context.Employees.Where(e => e.Position == "Trainee").ToListAsync();
        }

        // 更新实习人员信息
        [HttpPost("UppdateTrainee/{id}")]
        public async Task<IActionResult> UppdateTrainee(string id, [FromBody] Employee employee)
        {
            if (id == "000")
            {
                _context.Employees.Add(employee);
            }
            else
            {
                var existingEmployee = await _context.Employees.FindAsync(id);
                if (existingEmployee == null) return NotFound();
                existingEmployee.Name = employee.Name;
                existingEmployee.Position = employee.Position;
                // 更新其他字段...
            }
            await _context.SaveChangesAsync();
            return Ok();
        }

        // 删除实习人员
        [HttpPost("TraineeDelete/{id}")]
        public async Task<IActionResult> TraineeDelete(string id)
        {
            var trainee = await _context.Employees.FindAsync(id);
            if (trainee == null) return NotFound();
            _context.Employees.Remove(trainee);
            await _context.SaveChangesAsync();
            return Ok();
        }

        // 获取员工列表
        [HttpGet("EmployeeList")]
        public async Task<ActionResult<List<Employee>>> EmployeeList()
        {
            return await _context.Employees.ToListAsync();
        }

        // 获取员工详细信息
        [HttpGet("EmployeeDetail/{id}")]
        public async Task<ActionResult<Employee>> EmployeeDetail(string id, int type)
        {
            var employee = await _context.Employees.FindAsync(id);
            if (employee == null) return NotFound();
            // 根据type返回不同信息，比如0为基础信息，1为考勤信息
            return employee; // 假设只返回基础信息
        }

        // 更新员工信息
        [HttpPost("UppdateEmployee/{id}")]
        public async Task<IActionResult> UppdateEmployee(string id, int type, [FromBody] Employee employee)
        {
            if (id == "000")
            {
                _context.Employees.Add(employee);
            }
            else
            {
                var existingEmployee = await _context.Employees.FindAsync(id);
                if (existingEmployee == null) return NotFound();
                // 更新不同类型的信息，比如type = 0更新基础信息，1更新考勤信息
                existingEmployee.Name = employee.Name;
                // 更新其他字段...
            }
            await _context.SaveChangesAsync();
            return Ok();
        }

        // 删除员工信息
        [HttpPost("EmployeeDelete/{id}")]
        public async Task<IActionResult> EmployeeDelete(string id)
        {
            var employee = await _context.Employees.FindAsync(id);
            if (employee == null) return NotFound();
            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}
