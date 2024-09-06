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
    public class ProjectManagementController : ControllerBase
    {
        private readonly FCMDbContext _context;

        public ProjectManagementController(FCMDbContext context)
        {
            _context = context;
        }

        [HttpGet("AttendingProjects/{userName}")]
        public async Task<ActionResult> AttendingProjects(string userName)
        {
            var user = await _context.Employees.Where(e => e.UserName == userName).SingleAsync();
            return Ok(await _context.Projects.Where(p => p.Employees.Contains(user)).ToListAsync());
        }

        // 获取项目列表
        [HttpGet("ProjectList")]
        public async Task<ActionResult<List<Project>>> ProjectList()
        {
            return await _context.Projects.ToListAsync();
        }

        // 获取项目详细信息
        [HttpGet("ProjectDetail/{id}")]
        public async Task<ActionResult<Project>> ProjectDetail(string id, int type)
        {
            var project = await _context.Projects.FindAsync(id);
            if (project == null) return NotFound();
            return project;
        }

        // 更新项目信息
        [HttpPost("UpdateProject/{id}")]
        public async Task<IActionResult> UpdateProject(string id, int type, [FromBody] Project project)
        {
            if (id == "000")
            {
                _context.Projects.Add(project);
            }
            else
            {
                var existingProject = await _context.Projects.FindAsync(id);
                if (existingProject == null) return NotFound();
                existingProject.Id = project.Id;
                // 更新其他字段...
            }
            await _context.SaveChangesAsync();
            return Ok();
        }

        // 删除项目
        [HttpPost("ProjectDelete/{id}")]
        public async Task<IActionResult> ProjectDelete(string id)
        {
            var project = await _context.Projects.FindAsync(id);
            if (project == null) return NotFound();
            _context.Projects.Remove(project);
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}
