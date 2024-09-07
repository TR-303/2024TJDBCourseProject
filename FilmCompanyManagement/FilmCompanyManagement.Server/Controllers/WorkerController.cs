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

        [HttpPost("expense")]
        public async Task<ActionResult> InsertExpense(expenseRequest request)
        {
            var bill = new Bill
            {
                AssignDate = Convert.ToDateTime(request.date),
                Amount = request.amount
            };
            _context.Bills.Add(bill);
            await _context.SaveChangesAsync();
            return Ok(1);
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

        [HttpPost("repair")]
        public async Task<IActionResult> Repair([FromBody] RepairForm form)
        {
            var employee = await _context.Employees.FirstOrDefaultAsync(e => e.UserName == form.id);
            if (employee == null)
            {
                return BadRequest("Employee not found.");
            }

            var photoEquipment = await _context.PhotoEquipments.FindAsync(Convert.ToInt32(form.equipmentID));
            if (photoEquipment == null)
            {
                return BadRequest("PhotoEquipment not found.");
            }

            var newBill = new Bill
            {
                Amount = form.amount,
                Type = "Repair",
                Status = false,       // ��ʼ״̬Ϊfalse
                AssignDate = DateTime.Parse(form.date)
            };

            await _context.EquipmentRepairs.AddAsync(new EquipmentRepair
            {
                Employee = employee,
                PhotoEquipment = photoEquipment,
                Bill = newBill,
            });
            await _context.SaveChangesAsync();

            return Ok(new { message = "Repair created successfully" });
        }

        [HttpPost("purchase")]
        public async Task<IActionResult> Purchase([FromBody] PurchaseForm form)
        {
            var employee = await _context.Employees.FirstOrDefaultAsync(e => e.UserName == form.id);
            if (employee == null)
            {
                return BadRequest("Employee not found.");
            }

            await _context.PhotoEquipments.AddAsync(new PhotoEquipment
            {
                Employee = employee,
                Name = form.equipment,
                Model = form.model,
                Bill = new Bill
                {
                    Amount = form.amount,
                    Type = "purchase",
                    AssignDate = DateTime.Now
                }
            });
            await _context.SaveChangesAsync();
            return Ok(new
            {
                message = "Purchase created successfully"
            });
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

    public class expenseRequest
    {
        public string id { get; set; }
        public string date { get; set; }
        public string description { get; set; }
        public decimal amount { get; set; }
    }

    public class RepairForm
    {
        public string id { get; set; }
        public string equipmentID { get; set; }
        public string date { get; set; }
        public decimal amount { get; set; }
    }

    public class PurchaseForm
    {
        public string id { get; set; }
        public string equipment { get; set; }
        public string model { get; set; }
        public decimal amount { get; set; }
    }
}
