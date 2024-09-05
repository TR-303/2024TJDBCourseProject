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
        public async Task<ActionResult> GetReceiver(string fileID)
        {
            var file = await _context.Files.Where(f => f.Id == fileID).SingleAsync();
            var receiverID = new List<string>();
            foreach (var project in file.Projects)
                receiverID.Add(project.Customer.Id);
            foreach (var finishedProduct in file.FinishedProducts)
                receiverID.Add(finishedProduct.Customer.Id);
            return Ok(receiverID);
        }

        [HttpPost]
        public async Task<ActionResult> InsertFile(string filename, string fileType, string contentType, string filePath, int fileSize)
        {
            var file = new Server.EntityFrame.Models.File
            {
                Name = filename,
                FileType = fileType,
                ContentType = contentType,
                Path = filePath,
                Size = fileSize,
                UploadDate = DateTime.Now,
                Status = "Pending approval",
            };
            await _context.AddAsync(file);
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}
