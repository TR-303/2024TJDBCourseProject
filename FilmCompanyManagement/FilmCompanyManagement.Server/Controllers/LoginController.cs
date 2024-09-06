﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FilmCompanyManagement.Server.EntityFrame;

namespace FilmCompanyManagement.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class LoginController : ControllerBase
    {

        private readonly FCMDbContext _context;

        public LoginController(FCMDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<ActionResult> IsUserUni(string userName)
        {
            return Ok(await _context.Employees.Where(e => e.UserName == userName).ToListAsync() != null);
        }

        [HttpPost]
        public async Task<ActionResult> UserLogin(LoginRequest request)
        {
            string userName = request.UserName;
            string password = request.Password;
            string department = request.Department;

            var loginUser = await _context.Employees.Where(e => e.UserName == userName && e.Department.Name == department).FirstOrDefaultAsync();
            if (loginUser == null)
                return Ok(-1);//账户不存在
            else if (loginUser.Password == password)
                return Ok(1);//登入成功
            return Ok(0);//密码错误
        }
    }

    public class LoginRequest
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Department { get; set; }
    }
}
