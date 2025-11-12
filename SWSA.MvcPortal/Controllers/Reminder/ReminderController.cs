using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using B11 = SWSA.MvcPortal.Models.Reminder.B11;


namespace SWSA.MvcPortal.Controllers.Reminder

{
    public class ReminderController(IConfiguration configuration) : Controller
    {
        private readonly string _connectionString = configuration.GetConnectionString("SwsaConntection");

        public async Task<IActionResult> SdnBhd18monthReminder()
        {
            try
            {
                Console.WriteLine("=== Loading Reminder Page with Dapper ===");

                using (var connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();
                    Console.WriteLine("✅ Database connection successful");

                    var sql = "SELECT * FROM [Quartz].[dbo].[B11] ORDER BY Id DESC";
                    var records = await connection.QueryAsync<B11>(sql);

                    Console.WriteLine($"✅ Successfully loaded {records.Count()} records for page");
                    return View(records);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Error in B11AcorrespondanceRecord: {ex.Message}");
                Console.WriteLine($"Stack Trace: {ex.StackTrace}");

                // 返回空列表而不是抛出异常
                return View(new List<B11>());
            }
        }
        public IActionResult AuditReminderSchedule()
        {
            return View();
        }
        public IActionResult SdnBhdAccDocReminderSchedule()
        {
            return View();
        }

        public IActionResult LLPAccDocReminderSchedule()
        {
            return View();
        }

        public IActionResult FormBnPReminderSchedule()
        {
            return View();
        }

        public IActionResult FormEReminderSchedule()
        {
            return View();
        }
        public IActionResult FormBEindividualTaxReminderSchedule()
        {
            return View();
        }

        #region B11 API Methods
        [HttpGet("get-b11-records")]
        public async Task<IActionResult> GetB11Records()
        {
            try
            {
                Console.WriteLine("=== Starting GetB11Records with Dapper ===");

                using (var connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();
                    Console.WriteLine("✅ Database connection successful");

                    var sql = "SELECT * FROM [Quartz].[dbo].[B11] ORDER BY Id DESC";
                    var records = await connection.QueryAsync<B11>(sql);

                    Console.WriteLine($"✅ Successfully retrieved {records.Count()} records");
                    return Json(new { success = true, data = records });
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Error in GetB11Records: {ex.Message}");
                Console.WriteLine($"Stack Trace: {ex.StackTrace}");

                return Json(new
                {
                    success = false,
                    message = ex.Message,
                    detailed = ex.StackTrace
                });
            }
        }

        [HttpGet("get-b11-record/{id}")]
        public async Task<IActionResult> GetB11Record(int id)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    var sql = "SELECT * FROM [Quartz].[dbo].[B11] WHERE Id = @Id";
                    var record = await connection.QueryFirstOrDefaultAsync<B11>(sql, new { Id = id });

                    if (record == null)
                        return Json(new { success = false, message = "Record not found" });

                    return Json(new { success = true, data = record });
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        [HttpPost("create-b11")]
        public async Task<IActionResult> CreateB11([FromBody] B11 model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return Json(new { success = false, errors = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage)) });

                using (var connection = new SqlConnection(_connectionString))
                {
                    var sql = @"INSERT INTO [Quartz].[dbo].[B11] 
                        ([Grouping], [File], [Company], [CompanyNo], 
                         [IncorporationDate], [YearEnd], [YMDueDate], [CirculationAFSDueDate], 
                         [ReminderDate], [EmailSend], [DateSent])
                        VALUES 
                        (@Grouping, @File, @Company, @CompanyNo, 
                         @IncorporationDate, @YearEnd, @YMDueDate, @CirculationAFSDueDate, 
                         @ReminderDate, @EmailSend, @DateSent);
                        SELECT CAST(SCOPE_IDENTITY() as int);";

                    var id = await connection.ExecuteScalarAsync<int>(sql, new
                    {
                        model.Grouping,
                        model.File,
                        model.Company,
                        model.CompanyNo,
                        model.IncorporationDate,
                        model.YearEnd,
                        model.YMDueDate,
                        model.CirculationAFSDueDate,
                        model.ReminderDate,
                        model.EmailSend,
                        model.DateSent
                    });

                    return Json(new { success = true, id = id, data = model });
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        [HttpPut("update-b11")]
        public async Task<IActionResult> UpdateB11([FromBody] B11 model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return Json(new { success = false, errors = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage)) });

                using (var connection = new SqlConnection(_connectionString))
                {
                    var sql = @"UPDATE [Quartz].[dbo].[B11] SET 
                        [Grouping] = @Grouping, [File] = @File, 
                        [Company] = @Company, [CompanyNo] = @CompanyNo, 
                        [IncorporationDate] = @IncorporationDate, [YearEnd] = @YearEnd, 
                        [YMDueDate] = @YMDueDate, [CirculationAFSDueDate] = @CirculationAFSDueDate, 
                        [ReminderDate] = @ReminderDate, [EmailSend] = @EmailSend, [DateSent] = @DateSent
                        WHERE Id = @Id";

                    var affectedRows = await connection.ExecuteAsync(sql, new
                    {
                        model.Id,
                        model.Grouping,
                        model.File,
                        model.Company,
                        model.CompanyNo,
                        model.IncorporationDate,
                        model.YearEnd,
                        model.YMDueDate,
                        model.CirculationAFSDueDate,
                        model.ReminderDate,
                        model.EmailSend,
                        model.DateSent
                    });

                    if (affectedRows == 0)
                        return Json(new { success = false, message = "Record not found" });

                    return Json(new { success = true, data = model });
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        [HttpDelete("delete-b11/{id}")]
        public async Task<IActionResult> DeleteB11(int id)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    var sql = "DELETE FROM [Quartz].[dbo].[B11] WHERE Id = @Id";
                    var affectedRows = await connection.ExecuteAsync(sql, new { Id = id });

                    if (affectedRows == 0)
                        return Json(new { success = false, message = "Record not found" });

                    return Json(new { success = true });
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }
        #endregion
    }
}
