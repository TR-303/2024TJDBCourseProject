using FilmCompanyManagement.Server.EntityFrame;
using FilmCompanyManagement.Server.EntityFrame.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FilmCompanyManagement.Server.Controllers
{
    [ApiController, Route("api/worker")]
    public class WorkerController : ControllerBase
    {
        private readonly FCMDbContext _context;

        public WorkerController(FCMDbContext context)
        {
            _context = context;
        }

        [HttpPost("upload")]
        public async Task<IActionResult> Upload([FromBody] UploadForm form)
        {
            Employee? sender = await _context.Employees.Include(e => e.Department).ThenInclude(d => d.Leader).FirstOrDefaultAsync(e => e.UserName == form.id);
            if (sender == null) return BadRequest("invalid sender");
            await _context.Files.AddAsync(new FilmCompanyManagement.Server.EntityFrame.Models.File
            {
                Sender = sender,
                Name = form.name,
                FileType = form.type,
                Size = form.size,
                Description = form.description,
                UploadDate = DateTime.Now,
                Receiver = sender.Department.Leader
            });
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpGet("files")]
        public async Task<IActionResult> GetFiles()
        {
            var ret = await _context.Files.Include(f => f.Sender).Select(f => new
            {
                id = f.Id,
                name = f.Name,
                type = f.FileType,
                description = f.Description,
                size = f.Size,
                uploadDate = f.UploadDate.ToString(),
                workerID = f.Sender.Id
            }).ToListAsync();
            return Ok(ret);
        }
    }

    public class UploadForm
    {
        public string id { get; set; }
        public string name { get; set; }
        public string type { get; set; }
        public int size { get; set; }
        public string description { get; set; }
    }
}