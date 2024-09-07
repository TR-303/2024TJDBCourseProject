using FilmCompanyManagement.Server.EntityFrame;
using FilmCompanyManagement.Server.EntityFrame.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using System.Linq;
using System.Threading.Tasks;
using File = FilmCompanyManagement.Server.EntityFrame.Models.File;

namespace FilmCompanyManagement.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BusinessManagementController : ControllerBase
    {
        private readonly FCMDbContext _context;

        public BusinessManagementController(FCMDbContext context)
        {
            _context = context;
        }

        //获取总览
        [HttpGet("get-overview")]
        public async Task<IActionResult> GetOverview()
        {
            var employees = await _context.Employees
                                          .Select(e => new
                                          {
                                              e.Id,
                                              e.Name,
                                              e.Position,
                                              e.Salary,
                                              e.Phone,
                                              e.Email,
                                              e.Gender
                                          })
                                          .ToListAsync();

            return Ok(new { employee_list = employees });
        }

        // 获取外部投资信息
        [HttpGet("get-invest")]
        public async Task<IActionResult> GetInvest()
        {
            var investments = await _context.Investments.Include(i => i.Bill)
                .Select(i => new
                {
                    id = i.Id,                             // 投资编号
                    billDate = i.Bill.AssignDate.ToString(),                // 投资日期
                    customerName = i.Customer == null ? null : i.Customer.CustomerName,   // 投资方
                    billAmount=i.Bill.Amount,                    // 投资金额
                    billStatus=i.Bill.Status?"已处理":"未处理"                     // 账单状态
                })
                .ToListAsync();

            return Ok(new { businesses_list = investments });
        }

        // 提交外部投资信息
        [HttpPost("submit-invest-form")]
        public async Task<IActionResult> SubmitInvest([FromBody] InvestmentForm form)
        {
            var investment = new Investment
            {
                Customer = new Customer
                {
                    CustomerName = form.CustomerName,
                    BusinessType = form.CustomerBusinessType,
                },
                Bill = new Bill
                {
                    Id = form.BillId,
                    Amount = form.BillAmount,
                    Type = form.BillType,
                    AssignDate = form.BillDate,
                    Status = form.BillStatus=="未处理"
                }
            };

            await _context.Investments.AddAsync(investment);
            await _context.SaveChangesAsync();
            return Ok(new { message = "投资记录提交成功" });
        }


        // 获取成片购买信息
        [HttpGet("get-buy")]
        public async Task<IActionResult> GetBuy()
        {
            var purchases = await _context.FinishedProducts.Include(fp=>fp.Bill)
                .Select(fp => new
                {
                    id=fp.Id,                            // 购买编号
                    billDate=fp.Bill.AssignDate.ToString(),               // 购买日期
                    customerName = fp.Customer == null ? null : fp.Customer.CustomerName,  // 购买人
                    billAmount=fp.Bill.Amount,                   // 购买金额
                    status=fp.Bill.Status?"已处理":"未处理"                      // 订单状态
                })
                .ToListAsync();

            return Ok(new { businesses_list = purchases });
        }

        // 提交成片购买订单



        // 获取设备租赁信息
        [HttpGet("get-lease")]
        public async Task<IActionResult> GetLease()
        {
            var leases = await _context.EquipmentLeases.Include(el=>el.Bill)
                .Select(el => new
                {
                    id=el.Id,                            // 租赁编号
                    billDate=el.Bill.AssignDate.ToString(),               // 租赁日期
                    customerName = el.Customer==null ? null : el.Customer.CustomerName,  // 租赁人
                    billAmount=el.Bill.Amount,                   // 租赁金额
                    status=el.Bill.Status ? "已处理" : "未处理"                         // 租赁状态
                })
                .ToListAsync();

            return Ok(new { businesses_list = leases });
        }

        // 提交设备租赁信息
        [HttpPost("submit-lease-form")]
        public async Task<IActionResult> SubmitLeaseForm([FromBody] EquipmentLease form)
        {
            if (form == null)
                return BadRequest("Invalid data.");

            await _context.EquipmentLeases.AddAsync(form);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Equipment lease submitted successfully!" });
        }

        // 获取公司项目信息
        [HttpGet("get-project")]
        public async Task<IActionResult> GetProject()
        {
            var projects = await _context.Projects.Include(p=>p.Bill)
                .Select(p => new
                {
                    id=p.Id,                            // 项目编号
                    billDate=p.Bill.AssignDate.ToString(),               // 项目日期
                    customerName = p.Customer == null ? null : p.Customer.CustomerName,  // 项目客户
                    billAmount=p.Bill.Amount,                   // 项目金额
                    manager=p.Manager == null ? null : p.Manager.Name,    // 项目负责人
                    status=p.Bill.Status ? "已处理" : "未处理"                         // 项目状态
                })
                .ToListAsync();

            return Ok(new { businesses_list = projects });
        }

        // 提交公司项目信息
        [HttpPost("submit-project-form")]
        public async Task<IActionResult> SubmitProjectForm([FromBody] Project form)
        {
            if (form == null)
                return BadRequest("Invalid data.");

            await _context.Projects.AddAsync(form);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Project submitted successfully!" });
        }

        // 删除外部投资信息
        [HttpPost("delete-invest-form")]
        public async Task<IActionResult> DeleteInvestForm([FromBody] InvestmentRow form)
        {
            var investment = await _context.Investments.FindAsync(form.id);
            if (investment == null)
                return NotFound("Investment not found.");

            _context.Investments.Remove(investment);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Investment deleted successfully!" });
        }

        // 删除成片购买信息
        [HttpPost("delete-buy-form")]
        public async Task<IActionResult> DeleteBuyForm([FromBody] PurchaseRow form)
        {
            var purchase = await _context.FinishedProducts.FindAsync(form.id);
            if (purchase == null)
                return NotFound("Finished product purchase not found.");

            _context.FinishedProducts.Remove(purchase);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Finished product purchase deleted successfully!" });
        }

        // 删除设备租赁信息
        [HttpPost("delete-lease-form")]
        public async Task<IActionResult> DeleteLeaseForm([FromBody] LeaseRow form)
        {
            var lease = await _context.EquipmentLeases.FindAsync(form.id);
            if (lease == null)
                return NotFound("Equipment lease not found.");

            _context.EquipmentLeases.Remove(lease);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Equipment lease deleted successfully!" });
        }

        // 删除公司项目信息
        [HttpPost("delete-project-form")]
        public async Task<IActionResult> DeleteProjectForm([FromBody] ProjectRow form)
        {
            var project = await _context.Projects.FindAsync(form.id);
            if (project == null)
                return NotFound("Project not found.");

            _context.Projects.Remove(project);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Project deleted successfully!" });
        }

        // 查看外部投资详情
        [HttpGet("details-invest/{id}")]
        public async Task<IActionResult> DetailsInvest(int id)
        {
            var investment = await _context.Investments
                .Include(i => i.Customer)
                .Include(i => i.Bill)
                .FirstOrDefaultAsync(i => i.Id == id);

            if (investment == null)
                return NotFound("Investment not found.");

            return Ok(new
            {
                id=investment.Id.ToString(),
                customerType= investment.Customer.BusinessType,
                customerName=investment.Customer.CustomerName,
                customerBusinessType=investment.Customer.BusinessType,
                customerPhone=investment.Customer.CustomerPhone,
                customerEmail=investment.Customer.CustomerEmail,
                customerAddress=investment.Customer.CustomerAddress,
                billId=investment.Bill.Id,
                billAmount=investment.Bill.Amount,
                billType=investment.Bill.Type,
                billDate=investment.Bill.AssignDate,
                billStatus=investment.Bill.Status?"已处理":"未处理"
            });
        }

        // 查看成片购买详情
        [HttpGet("details-buy/{id}")]
        public async Task<IActionResult> DetailsBuy(int id)
        {
            var purchase = await _context.FinishedProducts
                .Include(fp => fp.Customer)
                .Include(fp => fp.Bill)
                .FirstOrDefaultAsync(fp => fp.Id == id);

            if (purchase == null)
                return NotFound("Finished product purchase not found.");

            return Ok(new[] { purchase });
        }

        // 查看设备租赁详情
        [HttpGet("details-lease/{id}")]
        public async Task<IActionResult> DetailsLease(int id)
        {
            var lease = await _context.EquipmentLeases
                .Include(el => el.Customer)
                .Include(el => el.Bill)
                .FirstOrDefaultAsync(el => el.Id == id);

            if (lease == null)
                return NotFound("Equipment lease not found.");

            return Ok(new[] { lease });
        }

        // 查看公司项目详情
        [HttpGet("details-project/{id}")]
        public async Task<IActionResult> DetailsProject(int id)
        {
            var project = await _context.Projects
                .Include(p => p.Customer)
                .Include(p => p.Bill)
                .Include(p => p.Manager)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (project == null)
                return NotFound("Project not found.");

            return Ok(new[] { project });
        }

    }
    public class InvestmentForm
    {
        public int Id { get; set; } // 投资编号
        public string CustomerType { get; set; } // 客户类型（企业、政府、个人）
        public string CustomerName { get; set; } // 客户名称
        public string CustomerBusinessType { get; set; } // 业务类型（直接投资、间接投资）
        public string CustomerPhone { get; set; } // 联系电话
        public string CustomerEmail { get; set; } // 电子邮件
        public string CustomerAddress { get; set; } // 客户地址
        public int BillId { get; set; } // 账单编号
        public decimal BillAmount { get; set; } // 金额
        public string BillType { get; set; } // 账单类型（存款、拨款）
        public DateTime BillDate { get; set; } // 账单日期
        public string BillStatus { get; set; } // 账单状态（发起、完成）
    }
    public class PurchaseForm
    {
        public int Id { get; set; } // 订单编号
        public string Type { get; set; } // 订单类型（标准、加急、特殊）
        public int FileId { get; set; } // 文件ID
        public string FileName { get; set; } // 文件名
        public string FileType { get; set; } // 文件类型（图片、视频、文档）
        public string FileContentType { get; set; } // 内容类型（项目报告、成片等）
        public decimal FileSize { get; set; } // 文件大小
        public string FilePath { get; set; } // 文件路径
        public DateTime FileUploadDate { get; set; } // 上传日期
        public string FileStatus { get; set; } // 文件状态（已上传、未上传、上传失败）
        public string CustomerType { get; set; } // 客户类型（企业、政府、个人）
        public string CustomerName { get; set; } // 客户名称
        public string CustomerBusinessType { get; set; } // 业务类型（短期租赁、长期租赁）
        public string CustomerPhone { get; set; } // 联系电话
        public string CustomerEmail { get; set; } // 电子邮件
        public string CustomerAddress { get; set; } // 客户地址
        public int BillId { get; set; } // 账单编号
        public decimal BillAmount { get; set; } // 金额
        public string BillType { get; set; } // 账单类型（存款、拨款）
        public DateTime BillDate { get; set; } // 账单日期
        public string BillStatus { get; set; } // 账单状态（发起、完成）
        public string Status { get; set; } // 订单状态（待发货、已发货、发货中）
    }
    public class LeaseForm
    {
        public int Id { get; set; } // 租赁编号
        public string Employee { get; set; } // 对接员工
        public string CustomerType { get; set; } // 客户类型（企业、政府、个人）
        public string CustomerName { get; set; } // 客户名称
        public string CustomerBusinessType { get; set; } // 业务类型（短期租赁、长期租赁）
        public string CustomerPhone { get; set; } // 联系电话
        public string CustomerEmail { get; set; } // 电子邮件
        public string CustomerAddress { get; set; } // 客户地址
        public int BillId { get; set; } // 账单编号
        public decimal BillAmount { get; set; } // 金额
        public string BillType { get; set; } // 账单类型（存款、拨款）
        public DateTime BillDate { get; set; } // 账单日期
        public string BillStatus { get; set; } // 账单状态（发起、完成）
        public string Status { get; set; } // 订单状态（待确认、已确认、已发货）
    }
    public class ProjectForm
    {
        public int Id { get; set; } // 项目编号
        public string Manager { get; set; } // 对接经理
        public string CustomerType { get; set; } // 客户类型（企业、政府、个人）
        public string CustomerName { get; set; } // 客户名称
        public string CustomerBusinessType { get; set; } // 业务类型（照片拍摄、视频制作、后期处理）
        public string CustomerPhone { get; set; } // 联系电话
        public string CustomerEmail { get; set; } // 电子邮件
        public string CustomerAddress { get; set; } // 客户地址
        public int BillId { get; set; } // 账单编号
        public decimal BillAmount { get; set; } // 金额
        public string BillType { get; set; } // 账单类型（存款、拨款）
        public DateTime BillDate { get; set; } // 账单日期
        public string BillStatus { get; set; } // 账单状态（发起、完成）
        public DateTime KpiDate { get; set; } // 绩效评定时间
        public int Result { get; set; } // 评定结果
        public string Judger { get; set; } // 评定者
        public string Status { get; set; } // 项目状态（进行中、已完成、已取消）
    }

    public class InvestmentRow
    {
        public int id { get; set; }  // 投资编号
        public string billDate { get; set; }  // 投资日期
        public string customerName { get; set; }  // 投资方
        public decimal billAmount { get; set; }  // 投资金额
        public string billStatus { get; set; }  // 账单状态
    }

    public class PurchaseRow
    {
        public int id { get; set; }  // 购买编号
        public string billDate { get; set; }  // 购买日期
        public string customerName { get; set; }  // 购买人
        public decimal billAmount { get; set; }  // 购买金额
        public string status { get; set; }  // 订单状态
    }

    public class LeaseRow
    {
        public int id { get; set; }  // 租赁编号
        public string billDate { get; set; }  // 租赁日期
        public string customerName { get; set; }  // 租赁人
        public decimal billAmount { get; set; }  // 租赁金额
        public string status { get; set; }  // 租赁状态
    }

    public class ProjectRow
    {
        public int id { get; set; }  // 项目编号
        public string billDate { get; set; }  // 项目日期
        public string customerName { get; set; }  // 项目客户
        public decimal billAmount { get; set; }  // 项目金额
        public string manager { get; set; }  // 项目负责人
        public string status { get; set; }  // 项目状态
    }

}
