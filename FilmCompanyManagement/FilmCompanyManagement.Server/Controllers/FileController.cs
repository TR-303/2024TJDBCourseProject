using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FilmCompanyManagement.Server.EntityFrame;
using FilmCompanyManagement.Server.EntityFrame.Models;

namespace FilmCompanyManagement.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class FileController : ControllerBase
    {

        private readonly FCMDbContext _context;

        public FileController(FCMDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult> GetReceiver(int fileID)
        {
            var file = await _context.Files.Include(f => f.Projects).Include(f => f.FinishedProducts).Where(f => f.Id == fileID).SingleAsync();
            
            var receivers = new List<Customer>();
            if (file.Projects != null)
                receivers.Add(file.Projects.Customer);
            if (file.FinishedProducts != null)
                receivers.Add(file.FinishedProducts.Customer);
            return Ok(receivers);
        }

        [HttpPost]
        public async Task<ActionResult> InsertFile(string filename, int fileSize, int receiverId, int senderId)
        {
            var receiver = await _context.Employees.FindAsync(receiverId);
            var sender = await _context.Employees.FindAsync(senderId);

            await _context.AddAsync(new Server.EntityFrame.Models.File
            {
                Name = filename,
                Size = fileSize,
                Receiver = receiver,
                Sender = sender,
                UploadDate = DateTime.Now,
            });
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}
