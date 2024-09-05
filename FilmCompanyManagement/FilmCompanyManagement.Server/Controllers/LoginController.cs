using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FilmCompanyManagement.Server.EntityFrame;

namespace FilmCompanyManagement.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class LoginController:ControllerBase
    {

        private readonly FCMDbContext _context;

        public LoginController(FCMDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<bool> IsUserUni(string userName)
        {
            var userExists = await _context.Employees.AnyAsync(e => e.UserName == userName);
            return userExists;
        }

        [HttpPost]
        public async Task<ActionResult> UserLogin(string userName, string password, string department)
        {
            var loginUser = await _context.Employees.Where(e => e.UserName == userName).SingleAsync();
            if (loginUser == null)
                return Ok(-1);//账户不存在
            if (loginUser.Password == password)
                return Ok(1);//登入成功
            return Ok(0);//密码错误
        }
    }
}
