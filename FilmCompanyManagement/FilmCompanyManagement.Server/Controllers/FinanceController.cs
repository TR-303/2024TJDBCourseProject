using FilmCompanyManagement.Server.EntityFrame;
using FilmCompanyManagement.Server.EntityFrame.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FilmCompanyManagement.Server.Controllers
{
    [ApiController, Route("api/[controller]")]
    public class FinanceController : ControllerBase
    {
        private readonly FCMDbContext _context;

        public FinanceController(FCMDbContext context)
        {
            _context = context;
        }

        [HttpGet("investment/{status}")]
        public async Task<IActionResult> GetInvestment(int status)
        {
            var ret = await _context.Investments.Include(i => i.Bill).Include(i => i.Customer).Where(i => i.Bill.Status == status).Select(i => new
            {
                investmentId = i.Id,
                customerName = i.Customer == null ? null : i.Customer.CustomerName,
                orderId = i.Bill.Id,
                orderDate = i.Bill.AssignDate,
                amount = i.Bill.Amount,
                processedDate = status == 1 ? i.Bill.ProcessedDate : null
            }).ToListAsync();
            return Ok(ret);
        }

        [HttpPost("investment/process")]
        public async Task<IActionResult> ProcessInvestment(ProcessRequest request)
        {
            Investment tar = await _context.Investments.Include(i => i.Bill).Include(i => i.Customer).SingleAsync(i => i.Id == request.Id);
            if (tar == null) return BadRequest("Invalid Id");

            tar.Bill.Status = 1;
            tar.Bill.ProcessedDate = DateTime.Parse(request.ProcessedDate);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpGet("equipmentlease/{status}")]
        public async Task<IActionResult> GetEquipmentLease(int status)
        {
            var ret = await _context.EquipmentLeases.Include(i => i.Bill).Include(i => i.Customer).Where(i => i.Bill.Status == status).Select(i => new
            {
                projectId = i.Id,
                orderId = i.Bill.Id,
                orderDate = i.Bill.AssignDate,
                amount = i.Bill.Amount,
                processedDate = status == 1 ? i.Bill.ProcessedDate : null
            }).ToListAsync();
            return Ok(ret);
        }

        [HttpPost("equipmentlease/process")]
        public async Task<IActionResult> ProcessEquipmentLease(ProcessRequest request)
        {
            EquipmentLease tar = await _context.EquipmentLeases.Include(i => i.Bill).Include(i => i.Customer).SingleAsync(i => i.Id == request.Id);
            if (tar == null) return BadRequest("Invalid Id");

            tar.Bill.Status = 1;
            tar.Bill.ProcessedDate = DateTime.Parse(request.ProcessedDate);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpGet("finishedproduct/{status}")]
        public async Task<IActionResult> GetFinishedProduct(int status)
        {
            var ret = await _context.FinishedProducts.Include(i => i.Bill).Include(i => i.Customer).Where(i => i.Bill.Status == status).Select(i => new
            {
                projectId = i.Id,
                orderId = i.Bill.Id,
                orderDate = i.Bill.AssignDate,
                amount = i.Bill.Amount,
                processedDate = status == 1 ? i.Bill.ProcessedDate : null
            }).ToListAsync();
            return Ok(ret);
        }

        [HttpPost("finishedproduct/process")]
        public async Task<IActionResult> ProcessFinishedProduct(ProcessRequest request)
        {
            FinishedProduct tar = await _context.FinishedProducts.Include(i => i.Bill).Include(i => i.Customer).SingleAsync(i => i.Id == request.Id);
            if (tar == null) return BadRequest("Invalid Id");

            tar.Bill.Status = 1;
            tar.Bill.ProcessedDate = DateTime.Parse(request.ProcessedDate);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpGet("salary/{status}")]
        public async Task<IActionResult> GetSalary(int status)
        {
            var ret = await _context.Employees.Include(i => i.SalaryBill).Where(i => i.SalaryBill != null && i.SalaryBill.Status == status).Select(i => new
            {
                employeeId = i.Id,
                employeeName = i.Name,
                orderId = i.SalaryBill.Id,
                orderDate = i.SalaryBill.AssignDate,
                salary = i.Salary,
                processedDate = status == 1 ? i.SalaryBill.ProcessedDate : null
            }).ToListAsync();
            return Ok(ret);
        }

        [HttpPost("salary/process")]
        public async Task<IActionResult> ProcessSalary(ProcessRequest request)
        {
            Employee tar = await _context.Employees.Include(i => i.SalaryBill).SingleAsync(i => i.Id == request.Id && i.SalaryBill != null);
            if (tar == null) return BadRequest("Invalid Id");

            tar.SalaryBill.Status = 1;
            tar.SalaryBill.ProcessedDate = DateTime.Parse(request.ProcessedDate);
            tar.SalaryBill = null;
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpGet("projectincome/{status}")]
        public async Task<IActionResult> GetProjectIncome(int status)
        {
            var ret = await _context.Projects.Include(i => i.Bill).Include(i => i.Customer).Where(i => i.Bill.Status == status).Select(i => new
            {
                projectId = i.Id,
                orderId = i.Bill.Id,
                orderDate = i.Bill.AssignDate,
                amount = i.Bill.Amount,
                processedDate = status == 1 ? i.Bill.ProcessedDate : null
            }).ToListAsync();
            return Ok(ret);
        }

        [HttpPost("projectincome/process")]
        public async Task<IActionResult> ProcessProjectIncome(ProcessRequest request)
        {
            FinishedProduct tar = await _context.FinishedProducts.Include(i => i.Bill).Include(i => i.Customer).SingleAsync(i => i.Id == request.Id);
            if (tar == null) return BadRequest("Invalid Id");

            tar.Bill.Status = 1;
            tar.Bill.ProcessedDate = DateTime.Parse(request.ProcessedDate);
            await _context.SaveChangesAsync();
            return Ok();
        }
    }

    public class ProcessRequest
    {
        public int Id { get; set; }
        public string ProcessedDate { get; set; }
    }
}