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
    public class NotificationController : ControllerBase
    {

        private readonly FCMDbContext _context;

        public NotificationController(FCMDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<List<Project>> GetUnfinishedProjects(string userName)
        {
            var unfinishedProjects = await _context.Projects.Where(p => p.Manager.UserName == userName).ToListAsync()
            return unfinishedProjects;
        }

        [HttpPost]
        public async Task<List<Drill>> GetUnfinishedDrills(string userName)
        {
            var user = await _context.Employees.
                Where(d => d.Manager.UserName == userName).SingleAsync()
            var unfinishedDrills = await _context.Drills.
                Where(d => (d.Employees.Contains(user) || d.Teacher == user) && DateTime.Now - d.DateTime < d.TimeSpan).ToListAsync()
            return unfinishedDrills;
        }

        [HttpPost]
        public async Task<List<FundingApplication>> GetFundingApplications(string userName)
        {
            var fundingApplications = await _context.FundingApplications.Where(fa => (fa.Employee.UserName == userName).ToListAsync()
            return fundingApplications;
        }
    }
}
