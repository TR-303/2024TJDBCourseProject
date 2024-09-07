using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FilmCompanyManagement.Server.EntityFrame;

namespace FilmCompanyManagement.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class WorkerController : ControllerBase
    {

        private readonly FCMDbContext _context;

        public WorkerController(FCMDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<ActionResult> userdata(thisRequest request)
        {
            var user = await _context.Employees.FirstOrDefaultAsync(e => e.UserName == request.id);
            if (user == null)
                return BadRequest(0);
            return Ok(user.Name);
        }
    }

    public class thisRequest
    {
        public string id { get; set; }
    }
}
