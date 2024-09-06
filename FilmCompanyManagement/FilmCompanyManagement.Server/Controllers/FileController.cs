﻿using Microsoft.AspNetCore.Mvc;
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
            var file = await _context.Files.Where(f => f.Id == fileID).SingleAsync();
            
            var receivers = new List<Customer>();
            if (file.Projects != null)
                receivers.Add(file.Projects.Customer);
            if (file.FinishedProducts != null)
                receivers.Add(file.FinishedProducts.Customer);
            return Ok(receivers);
        }

        [HttpPost]
        public async Task<ActionResult> InsertFile(string filename, string fileType, string contentType, string filePath, int fileSize)
        {
            await _context.AddAsync(new Server.EntityFrame.Models.File
            {
                Name = filename,
                FileType = fileType,
                ContentType = contentType,
                Path = filePath,
                Size = fileSize,
                UploadDate = DateTime.Now,
                Status = "待审批",
            });
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}
