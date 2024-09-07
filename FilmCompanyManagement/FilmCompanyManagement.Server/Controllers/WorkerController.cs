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
        public async Task<ActionResult> userdata(workerRequest request)
        {
            var user = await _context.Employees.FirstOrDefaultAsync(e => e.UserName == request.id);
            if (user == null)
                return BadRequest(0);
            return Ok(user.Name);
        }

        [HttpPost]
        public async Task<ActionResult> expense(expenseRequest request)
        {
            var user = await _context.Employees.FirstOrDefaultAsync(e => e.UserName == request.id);
            if (user == null)
                return BadRequest(0);
            return Ok(user.Name);
        }
    }

    public class workerRequest
    {
        public string id { get; set; }
    }

    public class expenseRequest
    {
        public string id { get; set; }
        public string date { get; set; }
        public string description { get; set; }
        public decimal amount { get; set; }
    }
    }
}
