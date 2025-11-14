using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;

namespace SWSA.MvcPortal.Controllers.AccDept
{
    [Route("api/test")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public TestController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet("connection")]
        public IActionResult TestConnection()
        {
            try
            {
                Console.WriteLine("=== Testing Database Connection ===");
                var connectionString = _configuration.GetConnectionString("SwsaConntection");
                Console.WriteLine($"Connection String: {connectionString}");

                using var connection = new SqlConnection(connectionString);
                connection.Open();
                Console.WriteLine("✅ Database connection successful!");

                return Ok(new { success = true, message = "Database connection successful" });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Database connection failed: {ex.Message}");
                Console.WriteLine($"Stack Trace: {ex.StackTrace}");
                return StatusCode(500, new { success = false, message = ex.Message });
            }
        }

        [HttpGet("simple")]
        public IActionResult SimpleTest()
        {
            Console.WriteLine("=== Simple Test Endpoint ===");
            return Ok(new { success = true, message = "Simple test works", timestamp = DateTime.Now });
        }
    }
}