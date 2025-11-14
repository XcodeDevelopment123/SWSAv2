using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using B11 = SWSA.MvcPortal.Models.Reminder.B11;
using B2 = SWSA.MvcPortal.Models.Reminder.B2;
using B31 = SWSA.MvcPortal.Models.Reminder.B31;
using B32 = SWSA.MvcPortal.Models.Reminder.B32;
using B34 = SWSA.MvcPortal.Models.Reminder.B34;
using B35 = SWSA.MvcPortal.Models.Reminder.B35;


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
        public async Task<IActionResult> AuditReminderSchedule()
        {
            try
            {
                Console.WriteLine("=== Loading Reminder Page with Dapper ===");

                using (var connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();
                    Console.WriteLine("✅ Database connection successful");

                    var sql = "SELECT * FROM [Quartz].[dbo].[B2] ORDER BY Id DESC";
                    var records = await connection.QueryAsync<B2>(sql);

                    Console.WriteLine($"✅ Successfully loaded {records.Count()} records for page");
                    return View(records);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Error in B2AcorrespondanceRecord: {ex.Message}");
                Console.WriteLine($"Stack Trace: {ex.StackTrace}");

                // 返回空列表而不是抛出异常
                return View(new List<B2>());
            }
        }
        public async Task<IActionResult> SdnBhdAccDocReminderSchedule()
        {
            try
            {
                Console.WriteLine("=== Loading Reminder Page with Dapper ===");

                using (var connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();
                    Console.WriteLine("✅ Database connection successful");

                    var sql = "SELECT * FROM [Quartz].[dbo].[B31] ORDER BY Id DESC";
                    var records = await connection.QueryAsync<B31>(sql);

                    Console.WriteLine($"✅ Successfully loaded {records.Count()} records for page");
                    return View(records);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Error in B31AcorrespondanceRecord: {ex.Message}");
                Console.WriteLine($"Stack Trace: {ex.StackTrace}");

                // 返回空列表而不是抛出异常
                return View(new List<B31>());
            }
        }

        public async Task <IActionResult> LLPAccDocReminderSchedule()
        {
            try
            {
                Console.WriteLine("=== Loading Reminder Page with Dapper ===");

                using (var connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();
                    Console.WriteLine("✅ Database connection successful");

                    var sql = "SELECT * FROM [Quartz].[dbo].[B32] ORDER BY Id DESC";
                    var records = await connection.QueryAsync<B32>(sql);

                    Console.WriteLine($"✅ Successfully loaded {records.Count()} records for page");
                    return View(records);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Error in B32AcorrespondanceRecord: {ex.Message}");
                Console.WriteLine($"Stack Trace: {ex.StackTrace}");

                // 返回空列表而不是抛出异常
                return View(new List<B31>());
            }
        }

        public async Task<IActionResult> FormBnPReminderSchedule()
        {
            try
            {
                Console.WriteLine("=== Loading Reminder Page with Dapper ===");

                using (var connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();
                    Console.WriteLine("✅ Database connection successful");

                    var sql = "SELECT * FROM [Quartz].[dbo].[B34] ORDER BY Id DESC";
                    var records = await connection.QueryAsync<B34>(sql);

                    Console.WriteLine($"✅ Successfully loaded {records.Count()} records for page");
                    return View(records);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Error in B34AcorrespondanceRecord: {ex.Message}");
                Console.WriteLine($"Stack Trace: {ex.StackTrace}");

                // 返回空列表而不是抛出异常
                return View(new List<B34>());
            }
        }

        public async Task <IActionResult> FormEReminderSchedule()
        {
            try
            {
                Console.WriteLine("=== Loading Reminder Page with Dapper ===");

                using (var connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();
                    Console.WriteLine("✅ Database connection successful");

                    var sql = "SELECT * FROM [Quartz].[dbo].[B35] ORDER BY Id DESC";
                    var records = await connection.QueryAsync<B35>(sql);

                    Console.WriteLine($"✅ Successfully loaded {records.Count()} records for page");
                    return View(records);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Error in B35AcorrespondanceRecord: {ex.Message}");
                Console.WriteLine($"Stack Trace: {ex.StackTrace}");

                // 返回空列表而不是抛出异常
                return View(new List<B34>());
            }
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

        #region B2 Api Method
        [HttpGet("get-b2-records")]
        public async Task<IActionResult> GetB2Records()
        {
            try
            {
                Console.WriteLine("=== Starting GetB2Records with Dapper ===");

                using (var connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();
                    Console.WriteLine("✅ Database connection successful");

                    var sql = "SELECT * FROM [Quartz].[dbo].[B2] ORDER BY Id DESC";
                    var records = await connection.QueryAsync<B2>(sql);

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

        [HttpGet("get-b2-record/{id}")]
        public async Task<IActionResult> GetB2Record(int id)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    var sql = "SELECT * FROM [Quartz].[dbo].[B2] WHERE Id = @Id";
                    var record = await connection.QueryFirstOrDefaultAsync<B2>(sql, new { Id = id });

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

        [HttpPost("create-b2")]
        public async Task<IActionResult> CreateB2([FromBody] B2 model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return Json(new { success = false, errors = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage)) });

                using (var connection = new SqlConnection(_connectionString))
                {
                    var sql = @"INSERT INTO [Quartz].[dbo].[B2] 
                        ([Grouping],[CompanyName],[ActiveStatus],[AEXstatus],[YearEnd]
                        ,[PIC],[SSM18MthDue],[SSM_TAX],[TargetedSendDate],[DateSend]
                        ,[TargetedReminder],[DateSend2],[DateReceived],[TargetedDate],[DateSent]
                        ,[TargetedCall],[DateRemind],[TargetedFinalText],[DateText]
                        ,[DateReceived2],[Note])
                        VALUES 
                        (@Grouping,@CompanyName,@ActiveStatus,@AEXstatus,@YearEnd
                        ,@PIC,@SSM18MthDue,@SSM_TAX,@TargetedSendDate,@DateSend
                        ,@TargetedReminder,@DateSend2,@DateReceived,@TargetedDate,@DateSent
                        ,@TargetedCall,@DateRemind,@TargetedFinalText,@DateText
                        ,@DateReceived2,@Note);
                        SELECT CAST(SCOPE_IDENTITY() as int);";

                    var id = await connection.ExecuteScalarAsync<int>(sql, new
                    {
                        model.Grouping,
                        model.CompanyName,
                        model.ActiveStatus,
                        model.AEXstatus,
                        model.YearEnd,
                        model.PIC,
                        model.SSM18MthDue,
                        model.SSM_TAX,
                        model.TargetedSendDate,
                        model.DateSend,
                        model.TargetedReminder,
                        model.DateSend2,
                        model.DateReceived,
                        model.TargetedDate,
                        model.DateSent,
                        model.TargetedCall,
                        model.DateRemind,
                        model.TargetedFinalText,
                        model.DateText,
                        model.DateReceived2,
                        model.Note
                    });

                    return Json(new { success = true, id = id, data = model });
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        [HttpPut("update-b2")]
        public async Task<IActionResult> UpdateB2([FromBody] B2 model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return Json(new { success = false, errors = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage)) });

                using (var connection = new SqlConnection(_connectionString))
                {
                    var sql = @"UPDATE [Quartz].[dbo].[B2] SET 
                      [Grouping] = @Grouping,[CompanyName] = @CompanyName,[ActiveStatus] = @ActiveStatus,
                      [AEXstatus] = @AEXstatus,[YearEnd] = @YearEnd,[PIC] = @PIC,
                      [SSM18MthDue] = @SSM18MthDue,[SSM_TAX] = @SSM_TAX,[TargetedSendDate] = @TargetedSendDate,
                      [DateSend] = @DateSend,[TargetedReminder] = @TargetedReminder,[DateSend2] = @DateSend2,
                      [DateReceived] = @DateReceived,[TargetedDate] = @TargetedDate,[DateSent] = @DateSent,
                      [TargetedCall] = @TargetedCall,[DateRemind] = @DateRemind,[TargetedFinalText] = @TargetedFinalText,
                      [DateText] = @DateText,[DateReceived2] = @DateReceived2,[Note] = @Note
                      WHERE [Id] = @Id";

                    var affectedRows = await connection.ExecuteAsync(sql, new
                    {
                        model.Id,
                        model.Grouping,
                        model.CompanyName,
                        model.ActiveStatus,
                        model.AEXstatus,
                        model.YearEnd,
                        model.PIC,
                        model.SSM18MthDue,
                        model.SSM_TAX,
                        model.TargetedSendDate,
                        model.DateSend,
                        model.TargetedReminder,
                        model.DateSend2,
                        model.DateReceived,
                        model.TargetedDate,
                        model.DateSent,
                        model.TargetedCall,
                        model.DateRemind,
                        model.TargetedFinalText,
                        model.DateText,
                        model.DateReceived2,
                        model.Note
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

        [HttpDelete("delete-b2/{id}")]
        public async Task<IActionResult> DeleteB2(int id)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    var sql = "DELETE FROM [Quartz].[dbo].[B2] WHERE Id = @Id";
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

        #region B31 Api Method
        [HttpGet("get-b31-records")]
        public async Task<IActionResult> GetB31Records()
        {
            try
            {
                Console.WriteLine("=== Starting GetB31Records with Dapper ===");

                using (var connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();
                    Console.WriteLine("✅ Database connection successful");

                    var sql = "SELECT * FROM [Quartz].[dbo].[B31] ORDER BY Id DESC";
                    var records = await connection.QueryAsync<B31>(sql);

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

        [HttpGet("get-b31-record/{id}")]
        public async Task<IActionResult> GetB31Record(int id)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    var sql = "SELECT * FROM [Quartz].[dbo].[B31] WHERE Id = @Id";
                    var record = await connection.QueryFirstOrDefaultAsync<B31>(sql, new { Id = id });

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

        [HttpPost("create-b31")]
        public async Task<IActionResult> CreateB31([FromBody] B31 model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return Json(new { success = false, errors = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage)) });

                using (var connection = new SqlConnection(_connectionString))
                {
                    var sql = @"INSERT INTO [Quartz].[dbo].[B31] 
                        ([Grouping],[CompanyName],[ActiveStatus],[AEXstatus]
                        ,[YearEnd],[PIC],[SSM18MthDue],[SSM_TAX],[T_startAccWk]
                        ,[T_Date],[DateSent],[T_Call],[DateRemind],[T_FinalText]
                        ,[DateText],[DateReceived],[Note])
                        VALUES 
                        (@Grouping,@CompanyName,@ActiveStatus,@AEXstatus
                        ,@YearEnd,@PIC,@SSM18MthDue,@SSM_TAX,@T_startAccWk
                        ,@T_Date,@DateSent,@T_Call,@DateRemind,@T_FinalText
                        ,@DateText,@DateReceived,@Note);
                        SELECT CAST(SCOPE_IDENTITY() as int);";

                    var id = await connection.ExecuteScalarAsync<int>(sql, new
                    {
                        model.Grouping,
                        model.CompanyName,
                        model.ActiveStatus,
                        model.AEXstatus,
                        model.YearEnd,
                        model.PIC,
                        model.SSM18MthDue,
                        model.SSM_TAX,
                        model.T_startAccWk,
                        model.T_Date,
                        model.DateSent,
                        model.T_Call,
                        model.DateRemind,
                        model.T_FinalText,
                        model.DateText,
                        model.DateReceived,
                        model.Note
                    });

                    return Json(new { success = true, id = id, data = model });
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        [HttpPut("update-b31")]
        public async Task<IActionResult> UpdateB31([FromBody] B31 model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return Json(new { success = false, errors = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage)) });

                using (var connection = new SqlConnection(_connectionString))
                {
                    var sql = @"UPDATE [Quartz].[dbo].[B31] SET
                    [Grouping] = @Grouping,[CompanyName] = @CompanyName,[ActiveStatus] = @ActiveStatus,
                    [AEXstatus] = @AEXstatus,[YearEnd] = @YearEnd,[PIC] = @PIC,[SSM18MthDue] = @SSM18MthDue,
                    [SSM_TAX] = @SSM_TAX,[T_startAccWk] = @T_startAccWk,[T_Date] = @T_Date,[DateSent] = @DateSent,
                    [T_Call] = @T_Call,[DateRemind] = @DateRemind,[T_FinalText] = @T_FinalText,[DateText] = @DateText,
                    [DateReceived] = @DateReceived,[Note] = @Note
                    WHERE [Id] = @Id";


                    var affectedRows = await connection.ExecuteAsync(sql, new
                    {
                        model.Id,
                        model.Grouping,
                        model.CompanyName,
                        model.ActiveStatus,
                        model.AEXstatus,
                        model.YearEnd,
                        model.PIC,
                        model.SSM18MthDue,
                        model.SSM_TAX,
                        model.T_startAccWk,
                        model.T_Date,
                        model.DateSent,
                        model.T_Call,
                        model.DateRemind,
                        model.T_FinalText,
                        model.DateText,
                        model.DateReceived,
                        model.Note
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

        [HttpDelete("delete-b31/{id}")]
        public async Task<IActionResult> DeleteB31(int id)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    var sql = "DELETE FROM [Quartz].[dbo].[B31] WHERE Id = @Id";
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

        #region B32 Api Method
        [HttpGet("get-b32-records")]
        public async Task<IActionResult> GetB32Records()
        {
            try
            {
                Console.WriteLine("=== Starting GetB32Records with Dapper ===");

                using (var connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();
                    Console.WriteLine("✅ Database connection successful");

                    var sql = "SELECT * FROM [Quartz].[dbo].[B32] ORDER BY Id DESC";
                    var records = await connection.QueryAsync<B32>(sql);

                    Console.WriteLine($"✅ Successfully retrieved {records.Count()} records");
                    return Json(new { success = true, data = records });
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Error in GetB32Records: {ex.Message}");
                Console.WriteLine($"Stack Trace: {ex.StackTrace}");

                return Json(new
                {
                    success = false,
                    message = ex.Message,
                    detailed = ex.StackTrace
                });
            }
        }

        [HttpGet("get-b32-record/{id}")]
        public async Task<IActionResult> GetB32Record(int id)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    var sql = "SELECT * FROM [Quartz].[dbo].[B32] WHERE Id = @Id";
                    var record = await connection.QueryFirstOrDefaultAsync<B32>(sql, new { Id = id });

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

        [HttpPost("create-b32")]
        public async Task<IActionResult> CreateB32([FromBody] B32 model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return Json(new { success = false, errors = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage)) });

                using (var connection = new SqlConnection(_connectionString))
                {
                    var sql = @"INSERT INTO [Quartz].[dbo].[B32] 
                        ([Grouping],[CompanyName],[ActiveStatus],[AEXstatus]
                        ,[YearEnd],[PIC],[SSM18MthDue],[SSM_TAX],[T_startAccWk]
                        ,[T_Date],[DateSent],[T_Call],[DateRemind],[T_FinalText]
                        ,[DateText],[DateReceived],[Note])
                        VALUES 
                        (@Grouping,@CompanyName,@ActiveStatus,@AEXstatus
                        ,@YearEnd,@PIC,@SSM18MthDue,@SSM_TAX,@T_startAccWk
                        ,@T_Date,@DateSent,@T_Call,@DateRemind,@T_FinalText
                        ,@DateText,@DateReceived,@Note);
                        SELECT CAST(SCOPE_IDENTITY() as int);";

                    var id = await connection.ExecuteScalarAsync<int>(sql, new
                    {
                        model.Grouping,
                        model.CompanyName,
                        model.ActiveStatus,
                        model.AEXstatus,
                        model.YearEnd,
                        model.PIC,
                        model.SSM18MthDue,
                        model.SSM_TAX,
                        model.T_startAccWk,
                        model.T_Date,
                        model.DateSent,
                        model.T_Call,
                        model.DateRemind,
                        model.T_FinalText,
                        model.DateText,
                        model.DateReceived,
                        model.Note
                    });

                    return Json(new { success = true, id = id, data = model });
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        [HttpPut("update-b32")]
        public async Task<IActionResult> UpdateB32([FromBody] B32 model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return Json(new { success = false, errors = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage)) });

                using (var connection = new SqlConnection(_connectionString))
                {
                    var sql = @"UPDATE [Quartz].[dbo].[B32] SET
                    [Grouping] = @Grouping,[CompanyName] = @CompanyName,[ActiveStatus] = @ActiveStatus,
                    [AEXstatus] = @AEXstatus,[YearEnd] = @YearEnd,[PIC] = @PIC,[SSM18MthDue] = @SSM18MthDue,
                    [SSM_TAX] = @SSM_TAX,[T_startAccWk] = @T_startAccWk,[T_Date] = @T_Date,[DateSent] = @DateSent,
                    [T_Call] = @T_Call,[DateRemind] = @DateRemind,[T_FinalText] = @T_FinalText,[DateText] = @DateText,
                    [DateReceived] = @DateReceived,[Note] = @Note
                    WHERE [Id] = @Id";


                    var affectedRows = await connection.ExecuteAsync(sql, new
                    {
                        model.Id,
                        model.Grouping,
                        model.CompanyName,
                        model.ActiveStatus,
                        model.AEXstatus,
                        model.YearEnd,
                        model.PIC,
                        model.SSM18MthDue,
                        model.SSM_TAX,
                        model.T_startAccWk,
                        model.T_Date,
                        model.DateSent,
                        model.T_Call,
                        model.DateRemind,
                        model.T_FinalText,
                        model.DateText,
                        model.DateReceived,
                        model.Note
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

        [HttpDelete("delete-b32/{id}")]
        public async Task<IActionResult> DeleteB32(int id)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    var sql = "DELETE FROM [Quartz].[dbo].[B32] WHERE Id = @Id";
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

        #region B34 Api Method
        [HttpGet("get-b34-records")]
        public async Task<IActionResult> GetB34Records()
        {
            try
            {
                Console.WriteLine("=== Starting GetB34Records with Dapper ===");

                using (var connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();
                    Console.WriteLine("✅ Database connection successful");

                    var sql = "SELECT * FROM [Quartz].[dbo].[B34] ORDER BY Id DESC";
                    var records = await connection.QueryAsync<B34>(sql);

                    Console.WriteLine($"✅ Successfully retrieved {records.Count()} records");
                    return Json(new { success = true, data = records });
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Error in GetB34Records: {ex.Message}");
                Console.WriteLine($"Stack Trace: {ex.StackTrace}");

                return Json(new
                {
                    success = false,
                    message = ex.Message,
                    detailed = ex.StackTrace
                });
            }
        }

        [HttpGet("get-b34-record/{id}")]
        public async Task<IActionResult> GetB34Record(int id)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    var sql = "SELECT * FROM [Quartz].[dbo].[B34] WHERE Id = @Id";
                    var record = await connection.QueryFirstOrDefaultAsync<B34>(sql, new { Id = id });

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

        [HttpPost("create-b34")]
        public async Task<IActionResult> CreateB34([FromBody] B34 model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return Json(new { success = false, errors = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage)) });

                using (var connection = new SqlConnection(_connectionString))
                {
                    var sql = @"INSERT INTO [Quartz].[dbo].[B34]
                        ([Grouping],[CompanyName],[SingleEntry],[FullSet],
                        [Reveiw],[TaxOnly],[PIC],[T_startAccWk],[T_Date],
                        [DateSent],[T_Call],[DateRemind],[T_FinalText],
                        [DateReceived],[Note],[DateText])
                        VALUES(@Grouping,@CompanyName,@SingleEntry,
                        @FullSet,@Reveiw,@TaxOnly,@PIC,@T_startAccWk,@T_Date,
                        @DateSent,@T_Call,@DateRemind,@T_FinalText,@DateReceived,
                        @Note,@DateText);
                        SELECT CAST(SCOPE_IDENTITY() AS int);";

                    var id = await connection.ExecuteScalarAsync<int>(sql, new
                    {
                        model.Id,
                        model.Grouping,
                        model.CompanyName,
                        model.SingleEntry,
                        model.FullSet,
                        model.Reveiw,
                        model.TaxOnly,
                        model.PIC,
                        model.T_startAccWk,
                        model.T_Date,
                        model.DateSent,
                        model.T_Call,
                        model.DateRemind,
                        model.T_FinalText,
                        model.DateReceived,
                        model.Note,
                        model.DateText
                    });

                    return Json(new { success = true, id = id, data = model });
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        [HttpPut("update-b34")]
        public async Task<IActionResult> UpdateB34([FromBody] B34 model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return Json(new { success = false, errors = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage)) });

                using (var connection = new SqlConnection(_connectionString))
                {
                    var sql = @"UPDATE [Quartz].[dbo].[B34]
                        SET[Grouping] = @Grouping,[CompanyName] = @CompanyName,
                        [SingleEntry] = @SingleEntry,[FullSet] = @FullSet,
                        [Reveiw] = @Reveiw,[TaxOnly] = @TaxOnly,[PIC] = @PIC,
                        [T_startAccWk] = @T_startAccWk,[T_Date] = @T_Date,
                        [DateSent] = @DateSent,[T_Call] = @T_Call,[DateRemind] = @DateRemind,
                        [T_FinalText] = @T_FinalText,[DateReceived] = @DateReceived,[Note] = @Note,[DateText] = @DateText
                        WHERE [Id] = @Id";


                    var affectedRows = await connection.ExecuteAsync(sql, new
                    {
                        model.Id,
                        model.Grouping,
                        model.CompanyName,
                        model.SingleEntry,
                        model.FullSet,
                        model.Reveiw,
                        model.TaxOnly,
                        model.PIC,
                        model.T_startAccWk,
                        model.T_Date,
                        model.DateSent,
                        model.T_Call,
                        model.DateRemind,
                        model.T_FinalText,
                        model.DateReceived,
                        model.Note,
                        model.DateText
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

        [HttpDelete("delete-b34/{id}")]
        public async Task<IActionResult> DeleteB34(int id)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    var sql = "DELETE FROM [Quartz].[dbo].[B34] WHERE Id = @Id";
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

        #region B35 Api Method
        [HttpGet("get-b35-records")]
        public async Task<IActionResult> GetB35Records()
        {
            try
            {
                Console.WriteLine("=== Starting GetB35Records with Dapper ===");

                using (var connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();
                    Console.WriteLine("✅ Database connection successful");

                    var sql = "SELECT * FROM [Quartz].[dbo].[B35] ORDER BY Id DESC";
                    var records = await connection.QueryAsync<B35>(sql);

                    Console.WriteLine($"✅ Successfully retrieved {records.Count()} records");
                    return Json(new { success = true, data = records });
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Error in GetB35Records: {ex.Message}");
                Console.WriteLine($"Stack Trace: {ex.StackTrace}");

                return Json(new
                {
                    success = false,
                    message = ex.Message,
                    detailed = ex.StackTrace
                });
            }
        }

        [HttpGet("get-b35-record/{id}")]
        public async Task<IActionResult> GetB35Record(int id)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    var sql = "SELECT * FROM [Quartz].[dbo].[B35] WHERE Id = @Id";
                    var record = await connection.QueryFirstOrDefaultAsync<B35>(sql, new { Id = id });

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

        [HttpPost("create-b35")]
        public async Task<IActionResult> CreateB35([FromBody] B35 model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return Json(new { success = false, errors = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage)) });

                using (var connection = new SqlConnection(_connectionString))
                {
                    var sql = @"INSERT INTO [Quartz].[dbo].[B35]
    ([Grouping],[CompanyName],[YearEnd],[PIC],
     [T_startWk],[T_Date],[DateSent],[T_call],[DateRemind],
     [T_finalText],[DateText],[DateReceived],[Note])
    VALUES(@Grouping,@CompanyName,@YearEnd,@PIC,
           @T_startWk,@T_Date,@DateSent,@T_call,@DateRemind,
           @T_finalText,@DateText,@DateReceived,@Note);
    SELECT CAST(SCOPE_IDENTITY() AS int);";


                    var id = await connection.ExecuteScalarAsync<int>(sql, new
                    {
                        model.Id,
                        model.Grouping,
                        model.CompanyName,
                        model.YearEnd,
                        model.PIC,
                        model.T_startWk,
                        model.T_Date,
                        model.DateSent,
                        model.T_call,
                        model.DateRemind,
                        model.T_finalText,
                        model.DateText,
                        model.DateReceived,
                        model.Note
                    });

                    return Json(new { success = true, id = id, data = model });
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        [HttpPut("update-b35")]
        public async Task<IActionResult> UpdateB35([FromBody] B35 model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return Json(new { success = false, errors = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage)) });

                using (var connection = new SqlConnection(_connectionString))
                {
                    var sql = @"UPDATE [Quartz].[dbo].[B35]
                    SET [Grouping] = @Grouping,[CompanyName] = @CompanyName,
                    [YearEnd] = @YearEnd,[PIC] = @PIC,[T_startWk] = @T_startWk,
                    [T_Date] = @T_Date,[DateSent] = @DateSent,[T_call] = @T_call,
                    [DateRemind] = @DateRemind,[T_finalText] = @T_finalText,[DateReceived] = @DateReceived,[DateText]=@DateText,
                    [Note] = @Note WHERE [Id] = @Id";

                    var affectedRows = await connection.ExecuteAsync(sql, new
                    {
                        model.Id,
                        model.Grouping,
                        model.CompanyName,
                        model.YearEnd,
                        model.PIC,
                        model.T_startWk,
                        model.T_Date,
                        model.DateSent,
                        model.T_call,
                        model.DateRemind,
                        model.T_finalText,
                        model.DateText,
                        model.DateReceived,
                        model.Note
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

        [HttpDelete("delete-b35/{id}")]
        public async Task<IActionResult> DeleteB35(int id)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    var sql = "DELETE FROM [Quartz].[dbo].[B35] WHERE Id = @Id";
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
