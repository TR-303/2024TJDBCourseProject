using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Oracle.ManagedDataAccess.Client;
using System;

namespace FilmCompanyManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public TestController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public IActionResult TestConnection()
        {
            string connectionString = _configuration.GetConnectionString("FCMConnection") ?? throw new InvalidOperationException("ConnectionString name not configured.");
            string resultMessage;

            try
            {
                using (var connection = new OracleConnection(connectionString))
                {
                    connection.Open();
                    resultMessage = "Database connection successful!";
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                resultMessage = $"Error connecting to database: {ex.Message}";
            }

            return Ok(resultMessage);
        }
    }
}
