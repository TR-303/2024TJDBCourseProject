using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FilmCompanyManagement.Server.EntityFrame.Models;
using System.Threading.Tasks;
using System.Text;
using System.Security.Cryptography;
using FilmCompanyManagement.Server.EntityFrame;
using Microsoft.Extensions.Configuration;
using Oracle.ManagedDataAccess.Client;
using System;

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
        public async Task<bool> IsUserUni(string user_id)
        {
            var userExists = await _context.Employees
                                           .AnyAsync(e => e.UserName == user_id);
            return userExists;
        }

    }
}
