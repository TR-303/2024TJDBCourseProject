using FilmCompanyManagement.Server.EntityFrame;
using Microsoft.AspNetCore.Mvc;

namespace FilmCompanyManagement.Controllers
{
    [ApiController,Route("/api/[controller]")]
    public class WorkerController : ControllerBase
    {
        private readonly FCMDbContext _context;

        public WorkerController(FCMDbContext context)
        {
            _context = context;
        }

        //[HttpGet("files")]
        //public async Task<IActionResult> GetFiles()
        //{
        //    var ret=await _context.Files.
        //}
    }
}