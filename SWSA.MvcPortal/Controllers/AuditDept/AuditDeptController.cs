using Dapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using SWSA.MvcPortal.Models.AccDeptModel;
using SWSA.MvcPortal.Models.AuditDeptModel;

namespace SWSA.MvcPortal.Controllers.AuditDept
{
    public class AuditDeptController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;

        public AuditDeptController(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("SwsaConntection");
        }


        public IActionResult AuditTemplate()
        {
            return View();
        }
        public IActionResult AuditBacklog()
        {
            return View();
        }
        public IActionResult AuditAexTemplate()
        {
            return View();
        }
        public IActionResult AuditAexBacklog()
        {
            return View();
        }

        public IActionResult AuditMasterSchedule()
        {
            return View();
        }

        public IActionResult AuditMasterLogBook()
        {
            return View();
        }

        public IActionResult AT21AuditMasterSchedule()
        {
            return View();
        }

        public IActionResult AT31AuditMasterLogBook()
        {
            return View();
        }

        public IActionResult AT32ACAWIP()
        {
            return View();
        }

        public IActionResult AT33AAAFSA()
        {
            return View();
        }
        public IActionResult AT34AADSP()
        {
            return View();
        }
        public IActionResult AEX41S()
        {
            return View();
        }
        public IActionResult AEX51LB()
        {
            return View();
        }
        public IActionResult AEX52WIP()
        {
            return View();
        }



        #region AT11 (Audit Template) CRUD Methods
        [AllowAnonymous]
        [HttpGet("api/auditdept/at11/get-all")]
        public async Task<IActionResult> GetAllAT11Records()
        {
            try
            {
                Console.WriteLine("=== Starting GetAllAT11Records ===");
                using var connection = new SqlConnection(_connectionString);

                await connection.OpenAsync();
                Console.WriteLine("Database connection successful");

                var sql = "SELECT * FROM [Quartz].[dbo].[AT11] ORDER BY Id DESC";
                Console.WriteLine($"Executing SQL: {sql}");

                var records = await connection.QueryAsync<AT11Model>(sql);
                Console.WriteLine($"Query executed successfully. Found {records?.Count() ?? 0} records");

                return Json(new { success = true, data = records });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"!!! ERROR in GetAllAT11Records !!!");
                Console.WriteLine($"Message: {ex.Message}");
                Console.WriteLine($"Stack Trace: {ex.StackTrace}");

                if (ex.InnerException != null)
                {
                    Console.WriteLine($"Inner Exception: {ex.InnerException.Message}");
                }

                return Json(new { success = false, message = $"Database error: {ex.Message}" });
            }
        }

        [AllowAnonymous]
        [HttpGet("api/auditdept/at11/get/{id}")]
        public async Task<IActionResult> GetAT11ById(int id)
        {
            try
            {
                using var connection = new SqlConnection(_connectionString);
                var sql = "SELECT * FROM [Quartz].[dbo].[AT11] WHERE Id = @Id";
                var record = await connection.QueryFirstOrDefaultAsync<AT11Model>(sql, new { Id = id });

                if (record == null)
                    return Json(new { success = false, message = "Record not found" });

                return Json(new { success = true, data = record });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in GetAT11ById: {ex.Message}");
                return Json(new { success = false, message = ex.Message });
            }
        }

        [AllowAnonymous]
        [HttpPost("api/auditdept/at11/create")]
        public async Task<IActionResult> CreateAT11([FromBody] AT11Model model)
        {
            try
            {
                Console.WriteLine("Creating new AT11 record...");
                using var connection = new SqlConnection(_connectionString);
                var sql = @"INSERT INTO [Quartz].[dbo].[AT11] 
            ([CompanyName], [Activity], [WhichDB], [YEtodo], [QuarterTodo], [PIC], [Status],
             [Revenue], [ProfitLoss], [AuditFee], [DateBilled], [StartDate], [AsAt], [NoOfDays],
             [TotalFieldWkDays], [ResultOverUnder], [AccSetup], [AcSummary], [AuditPlanning],
             [AuditExecution], [AuditCompletion], [TotalPercent], [DateSent], [EndDate],
             [ResultOverUnderKuching], [DateSentKK], [EndDateKK], [ResultOverUnderKK],
             [TotalReviewDaysKK], [DateSentToKK], [DateReceivedAR], [DateOfReport],
             [DateOfDirectorsRept], [DateSentSigning], [FlwUpDate], [DateReceivedSigning],
             [CommOfOathsDate], [TaxDueDate], [PassToTaxDept], [SSMDueDate], [DatePassToSecDept],
             [DateBinded], [DespatchDateToClient])
            VALUES 
            (@CompanyName, @Activity, @WhichDB, @YEtodo, @QuarterTodo, @PIC, @Status,
             @Revenue, @ProfitLoss, @AuditFee, @DateBilled, @StartDate, @AsAt, @NoOfDays,
             @TotalFieldWkDays, @ResultOverUnder, @AccSetup, @AcSummary, @AuditPlanning,
             @AuditExecution, @AuditCompletion, @TotalPercent, @DateSent, @EndDate,
             @ResultOverUnderKuching, @DateSentKK, @EndDateKK, @ResultOverUnderKK,
             @TotalReviewDaysKK, @DateSentToKK, @DateReceivedAR, @DateOfReport,
             @DateOfDirectorsRept, @DateSentSigning, @FlwUpDate, @DateReceivedSigning,
             @CommOfOathsDate, @TaxDueDate, @PassToTaxDept, @SSMDueDate, @DatePassToSecDept,
             @DateBinded, @DespatchDateToClient);
            SELECT CAST(SCOPE_IDENTITY() as int);";

                var id = await connection.ExecuteScalarAsync<int>(sql, model);
                Console.WriteLine($"Record created successfully with ID: {id}");
                return Json(new { success = true, id = id, message = "Record created successfully" });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in CreateAT11: {ex.Message}");
                return Json(new { success = false, message = ex.Message });
            }
        }

        [AllowAnonymous]
        [HttpPut("api/auditdept/at11/update")]
        public async Task<IActionResult> UpdateAT11([FromBody] AT11Model model)
        {
            try
            {
                using var connection = new SqlConnection(_connectionString);
                var sql = @"UPDATE [Quartz].[dbo].[AT11] SET 
            [CompanyName] = @CompanyName, [Activity] = @Activity, [WhichDB] = @WhichDB,
            [YEtodo] = @YEtodo, [QuarterTodo] = @QuarterTodo, [PIC] = @PIC, [Status] = @Status,
            [Revenue] = @Revenue, [ProfitLoss] = @ProfitLoss, [AuditFee] = @AuditFee,
            [DateBilled] = @DateBilled, [StartDate] = @StartDate, [AsAt] = @AsAt,
            [NoOfDays] = @NoOfDays, [TotalFieldWkDays] = @TotalFieldWkDays,
            [ResultOverUnder] = @ResultOverUnder, [AccSetup] = @AccSetup, [AcSummary] = @AcSummary,
            [AuditPlanning] = @AuditPlanning, [AuditExecution] = @AuditExecution,
            [AuditCompletion] = @AuditCompletion, [TotalPercent] = @TotalPercent,
            [DateSent] = @DateSent, [EndDate] = @EndDate, [ResultOverUnderKuching] = @ResultOverUnderKuching,
            [DateSentKK] = @DateSentKK, [EndDateKK] = @EndDateKK, [ResultOverUnderKK] = @ResultOverUnderKK,
            [TotalReviewDaysKK] = @TotalReviewDaysKK, [DateSentToKK] = @DateSentToKK,
            [DateReceivedAR] = @DateReceivedAR, [DateOfReport] = @DateOfReport,
            [DateOfDirectorsRept] = @DateOfDirectorsRept, [DateSentSigning] = @DateSentSigning,
            [FlwUpDate] = @FlwUpDate, [DateReceivedSigning] = @DateReceivedSigning,
            [CommOfOathsDate] = @CommOfOathsDate, [TaxDueDate] = @TaxDueDate,
            [PassToTaxDept] = @PassToTaxDept, [SSMDueDate] = @SSMDueDate,
            [DatePassToSecDept] = @DatePassToSecDept, [DateBinded] = @DateBinded,
            [DespatchDateToClient] = @DespatchDateToClient
            WHERE Id = @Id";

                var affectedRows = await connection.ExecuteAsync(sql, model);
                if (affectedRows == 0)
                    return Json(new { success = false, message = "Record not found" });

                return Json(new { success = true, message = "Record updated successfully" });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in UpdateAT11: {ex.Message}");
                return Json(new { success = false, message = ex.Message });
            }
        }

        [AllowAnonymous]
        [HttpDelete("api/auditdept/at11/delete/{id}")]
        public async Task<IActionResult> DeleteAT11(int id)
        {
            try
            {
                using var connection = new SqlConnection(_connectionString);
                var sql = "DELETE FROM [Quartz].[dbo].[AT11] WHERE Id = @Id";
                var affectedRows = await connection.ExecuteAsync(sql, new { Id = id });

                if (affectedRows == 0)
                    return Json(new { success = false, message = "Record not found" });

                return Json(new { success = true, message = "Record deleted successfully" });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in DeleteAT11: {ex.Message}");
                return Json(new { success = false, message = ex.Message });
            }
        }
        #endregion

        #region AT21 (AuditMasterSchedule) CRUD Methods
        [AllowAnonymous]
        [HttpGet("api/auditdept/at21/get-all")]
        public async Task<IActionResult> GetAllAT21Records()
        {
            try
            {
                Console.WriteLine("=== Starting GetAllAT21Records ===");
                using var connection = new SqlConnection(_connectionString);

                await connection.OpenAsync();
                Console.WriteLine("Database connection successful");

                var sql = "SELECT * FROM [Quartz].[dbo].[AT21] ORDER BY Id DESC";
                Console.WriteLine($"Executing SQL: {sql}");

                var records = await connection.QueryAsync<AT21Model>(sql);
                Console.WriteLine($"Query executed successfully. Found {records?.Count() ?? 0} records");

                return Json(new { success = true, data = records });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"!!! ERROR in GetAllAT21Records !!!");
                Console.WriteLine($"Message: {ex.Message}");
                Console.WriteLine($"Stack Trace: {ex.StackTrace}");

                if (ex.InnerException != null)
                {
                    Console.WriteLine($"Inner Exception: {ex.InnerException.Message}");
                }

                return Json(new { success = false, message = $"Database error: {ex.Message}" });
            }
        }

        [AllowAnonymous]
        [HttpGet("api/auditdept/at21/get/{id}")]
        public async Task<IActionResult> GetAT21ById(int id)
        {
            try
            {
                using var connection = new SqlConnection(_connectionString);
                var sql = "SELECT * FROM [Quartz].[dbo].[AT21] WHERE Id = @Id";
                var record = await connection.QueryFirstOrDefaultAsync<AT21Model>(sql, new { Id = id });

                if (record == null)
                    return Json(new { success = false, message = "Record not found" });

                return Json(new { success = true, data = record });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in GetAT21ById: {ex.Message}");
                return Json(new { success = false, message = ex.Message });
            }
        }

        [AllowAnonymous]
        [HttpPost("api/auditdept/at21/create")]
        public async Task<IActionResult> CreateAT21([FromBody] AT21Model model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return Json(new { success = false, errors = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage)) });

                Console.WriteLine("Creating new AT21 record...");
                using var connection = new SqlConnection(_connectionString);

                // 更详细的日志记录
                Console.WriteLine($"Model data: {System.Text.Json.JsonSerializer.Serialize(model)}");

                var sql = @"INSERT INTO [Quartz].[dbo].[AT21] 
            ([Grouping], [CompanyName], [QuartertoAudit], [Activity], [YearEnd], 
             [YearToDo], [CompanyStatus], [AuditStatus], [MovetoAEX], [MovetoBacklog],
             [First18mthdue], [AFSdueDate], [CoSec], [AuditStaff], [DateDocIn],
             [EstRev], [AcctngWk], [JobCompleted], [Remark])
            VALUES 
            (@Grouping, @CompanyName, @QuartertoAudit, @Activity, @YearEnd, 
             @YearToDo, @CompanyStatus, @AuditStatus, @MovetoAEX, @MovetoBacklog,
             @First18mthdue, @AFSdueDate, @CoSec, @AuditStaff, @DateDocIn,
             @EstRev, @AcctngWk, @JobCompleted, @Remark);
            SELECT CAST(SCOPE_IDENTITY() as int);";

                Console.WriteLine($"Executing SQL: {sql}");

                var id = await connection.ExecuteScalarAsync<int>(sql, model);
                Console.WriteLine($"Record created successfully with ID: {id}");
                return Json(new { success = true, id = id, message = "Record created successfully" });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in CreateAT21: {ex.Message}");
                Console.WriteLine($"Stack Trace: {ex.StackTrace}");
                if (ex.InnerException != null)
                {
                    Console.WriteLine($"Inner Exception: {ex.InnerException.Message}");
                }
                return Json(new { success = false, message = $"Database error: {ex.Message}" });
            }
        }

        [AllowAnonymous]
        [HttpPut("api/auditdept/at21/update")]
        public async Task<IActionResult> UpdateAT21([FromBody] AT21Model model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return Json(new { success = false, errors = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage)) });

                Console.WriteLine($"Updating AT21 record with ID: {model.Id}");
                using var connection = new SqlConnection(_connectionString);

                var sql = @"UPDATE [Quartz].[dbo].[AT21] SET 
            [Grouping] = @Grouping, 
            [CompanyName] = @CompanyName, 
            [QuartertoAudit] = @QuartertoAudit, 
            [Activity] = @Activity, 
            [YearEnd] = @YearEnd, 
            [YearToDo] = @YearToDo, 
            [CompanyStatus] = @CompanyStatus, 
            [AuditStatus] = @AuditStatus, 
            [MovetoAEX] = @MovetoAEX, 
            [MovetoBacklog] = @MovetoBacklog,
            [First18mthdue] = @First18mthdue, 
            [AFSdueDate] = @AFSdueDate, 
            [CoSec] = @CoSec, 
            [AuditStaff] = @AuditStaff, 
            [DateDocIn] = @DateDocIn,
            [EstRev] = @EstRev, 
            [AcctngWk] = @AcctngWk, 
            [JobCompleted] = @JobCompleted, 
            [Remark] = @Remark
            WHERE Id = @Id";

                Console.WriteLine($"Executing update SQL: {sql}");

                var affectedRows = await connection.ExecuteAsync(sql, model);
                Console.WriteLine($"Affected rows: {affectedRows}");

                if (affectedRows == 0)
                    return Json(new { success = false, message = "Record not found" });

                return Json(new { success = true, message = "Record updated successfully" });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in UpdateAT21: {ex.Message}");
                Console.WriteLine($"Stack Trace: {ex.StackTrace}");
                if (ex.InnerException != null)
                {
                    Console.WriteLine($"Inner Exception: {ex.InnerException.Message}");
                }
                return Json(new { success = false, message = $"Database error: {ex.Message}" });
            }
        }

        [AllowAnonymous]
        [HttpDelete("api/auditdept/at21/delete/{id}")]
        public async Task<IActionResult> DeleteAT21(int id)
        {
            try
            {
                Console.WriteLine($"Deleting AT21 record with ID: {id}");
                using var connection = new SqlConnection(_connectionString);
                var sql = "DELETE FROM [Quartz].[dbo].[AT21] WHERE Id = @Id";
                var affectedRows = await connection.ExecuteAsync(sql, new { Id = id });

                Console.WriteLine($"Affected rows: {affectedRows}");

                if (affectedRows == 0)
                    return Json(new { success = false, message = "Record not found" });

                return Json(new { success = true, message = "Record deleted successfully" });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in DeleteAT21: {ex.Message}");
                Console.WriteLine($"Stack Trace: {ex.StackTrace}");
                if (ex.InnerException != null)
                {
                    Console.WriteLine($"Inner Exception: {ex.InnerException.Message}");
                }
                return Json(new { success = false, message = $"Database error: {ex.Message}" });
            }
        }
        #endregion

        #region AT22 (AuditBacklog) CRUD Methods
        [AllowAnonymous]
        [HttpGet("api/auditdept/at22/get-all")]
        public async Task<IActionResult> GetAllAT22Records()
        {
            try
            {
                Console.WriteLine("=== Starting GetAllAT22Records ===");
                using var connection = new SqlConnection(_connectionString);

                await connection.OpenAsync();
                Console.WriteLine("Database connection successful");

                var sql = "SELECT * FROM [Quartz].[dbo].[AT22] ORDER BY Id DESC";
                Console.WriteLine($"Executing SQL: {sql}");

                var records = await connection.QueryAsync<AT22Model>(sql);
                Console.WriteLine($"Query executed successfully. Found {records?.Count() ?? 0} records");

                return Json(new { success = true, data = records });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"!!! ERROR in GetAllAT22Records !!!");
                Console.WriteLine($"Message: {ex.Message}");
                Console.WriteLine($"Stack Trace: {ex.StackTrace}");

                if (ex.InnerException != null)
                {
                    Console.WriteLine($"Inner Exception: {ex.InnerException.Message}");
                }

                return Json(new { success = false, message = $"Database error: {ex.Message}" });
            }
        }

        [AllowAnonymous]
        [HttpGet("api/auditdept/at22/get/{id}")]
        public async Task<IActionResult> GetAT22ById(int id)
        {
            try
            {
                using var connection = new SqlConnection(_connectionString);
                var sql = "SELECT * FROM [Quartz].[dbo].[AT22] WHERE Id = @Id";
                var record = await connection.QueryFirstOrDefaultAsync<AT22Model>(sql, new { Id = id });

                if (record == null)
                    return Json(new { success = false, message = "Record not found" });

                return Json(new { success = true, data = record });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in GetAT22ById: {ex.Message}");
                return Json(new { success = false, message = ex.Message });
            }
        }

        [AllowAnonymous]
        [HttpPost("api/auditdept/at22/create")]
        public async Task<IActionResult> CreateAT22([FromBody] AT22Model model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return Json(new { success = false, errors = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage)) });

                Console.WriteLine("Creating new AT22 record...");
                using var connection = new SqlConnection(_connectionString);
                var sql = @"INSERT INTO [Quartz].[dbo].[AT22] 
            ([Grouping], [CompanyName], [QuarterToDoAudit], [Activity], [YearEnd], 
             [YearToDo], [MoveToActiveAexSch], [DateDocIn], [AcctngWk], [ReasonWhyBacklog])
            VALUES 
            (@Grouping, @CompanyName, @QuarterToDoAudit, @Activity, @YearEnd, 
             @YearToDo, @MoveToActiveAexSch, @DateDocIn, @AcctngWk, @ReasonWhyBacklog);
            SELECT CAST(SCOPE_IDENTITY() as int);";

                var id = await connection.ExecuteScalarAsync<int>(sql, model);
                Console.WriteLine($"Record created successfully with ID: {id}");
                return Json(new { success = true, id = id, message = "Record created successfully" });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in CreateAT22: {ex.Message}");
                return Json(new { success = false, message = ex.Message });
            }
        }

        [AllowAnonymous]
        [HttpPut("api/auditdept/at22/update")]
        public async Task<IActionResult> UpdateAT22([FromBody] AT22Model model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return Json(new { success = false, errors = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage)) });

                using var connection = new SqlConnection(_connectionString);
                var sql = @"UPDATE [Quartz].[dbo].[AT22] SET 
            [Grouping] = @Grouping, 
            [CompanyName] = @CompanyName, 
            [QuarterToDoAudit] = @QuarterToDoAudit, 
            [Activity] = @Activity, 
            [YearEnd] = @YearEnd, 
            [YearToDo] = @YearToDo, 
            [MoveToActiveAexSch] = @MoveToActiveAexSch, 
            [DateDocIn] = @DateDocIn, 
            [AcctngWk] = @AcctngWk, 
            [ReasonWhyBacklog] = @ReasonWhyBacklog
            WHERE Id = @Id";

                var affectedRows = await connection.ExecuteAsync(sql, model);
                if (affectedRows == 0)
                    return Json(new { success = false, message = "Record not found" });

                return Json(new { success = true, message = "Record updated successfully" });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in UpdateAT22: {ex.Message}");
                return Json(new { success = false, message = ex.Message });
            }
        }

        [AllowAnonymous]
        [HttpDelete("api/auditdept/at22/delete/{id}")]
        public async Task<IActionResult> DeleteAT22(int id)
        {
            try
            {
                using var connection = new SqlConnection(_connectionString);
                var sql = "DELETE FROM [Quartz].[dbo].[AT22] WHERE Id = @Id";
                var affectedRows = await connection.ExecuteAsync(sql, new { Id = id });

                if (affectedRows == 0)
                    return Json(new { success = false, message = "Record not found" });

                return Json(new { success = true, message = "Record deleted successfully" });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in DeleteAT22: {ex.Message}");
                return Json(new { success = false, message = ex.Message });
            }
        }
        #endregion

        #region AT31 (AuditMasterLogBook) CRUD Methods
        [AllowAnonymous]
        [HttpGet("api/auditdept/at31/get-all")]
        public async Task<IActionResult> GetAllAT31Records()
        {
            try
            {
                Console.WriteLine("=== Starting GetAllAT31Records ===");
                using var connection = new SqlConnection(_connectionString);

                await connection.OpenAsync();
                Console.WriteLine("Database connection successful");

                var sql = "SELECT * FROM [Quartz].[dbo].[AT31] ORDER BY Id DESC";
                Console.WriteLine($"Executing SQL: {sql}");

                var records = await connection.QueryAsync<AT31Model>(sql);
                Console.WriteLine($"Query executed successfully. Found {records?.Count() ?? 0} records");

                return Json(new { success = true, data = records });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"!!! ERROR in GetAllAT31Records !!!");
                Console.WriteLine($"Message: {ex.Message}");
                Console.WriteLine($"Stack Trace: {ex.StackTrace}");

                if (ex.InnerException != null)
                {
                    Console.WriteLine($"Inner Exception: {ex.InnerException.Message}");
                }

                return Json(new { success = false, message = $"Database error: {ex.Message}" });
            }
        }

        [AllowAnonymous]
        [HttpGet("api/auditdept/at31/get/{id}")]
        public async Task<IActionResult> GetAT31ById(int id)
        {
            try
            {
                using var connection = new SqlConnection(_connectionString);
                var sql = "SELECT * FROM [Quartz].[dbo].[AT31] WHERE Id = @Id";
                var record = await connection.QueryFirstOrDefaultAsync<AT31Model>(sql, new { Id = id });

                if (record == null)
                    return Json(new { success = false, message = "Record not found" });

                return Json(new { success = true, data = record });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in GetAT31ById: {ex.Message}");
                return Json(new { success = false, message = ex.Message });
            }
        }

        [AllowAnonymous]
        [HttpPost("api/auditdept/at31/create")]
        public async Task<IActionResult> CreateAT31([FromBody] AT31Model model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return Json(new { success = false, errors = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage)) });

                Console.WriteLine("Creating new AT31 record...");
                using var connection = new SqlConnection(_connectionString);
                var sql = @"INSERT INTO [Quartz].[dbo].[AT31] 
            ([CompanyName], [Activity], [YEtoDo], [QuartertoDo], [PIC], [MthDue], [Status],
             [DocInwardsDate], [Revenue], [Profit], [AuditFee], [DateBilled], [StartDate],
             [EndDate], [DaysDone], [DonePercent], [Completed], [DateSent], [DateSentToKK],
             [ReviewResultofDays], [DateReceiveFromKK], [WhoMeetClientDate], [DateSenttoClient],
             [DateReceiveBack], [TaxDueDate], [PasstoDept], [SSMdueDate], [DatePasstoSecDept],
             [Binded], [DespatchDateToClient])
            VALUES 
            (@CompanyName, @Activity, @YEtoDo, @QuartertoDo, @PIC, @MthDue, @Status,
             @DocInwardsDate, @Revenue, @Profit, @AuditFee, @DateBilled, @StartDate,
             @EndDate, @DaysDone, @DonePercent, @Completed, @DateSent, @DateSentToKK,
             @ReviewResultofDays, @DateReceiveFromKK, @WhoMeetClientDate, @DateSenttoClient,
             @DateReceiveBack, @TaxDueDate, @PasstoDept, @SSMdueDate, @DatePasstoSecDept,
             @Binded, @DespatchDateToClient);
            SELECT CAST(SCOPE_IDENTITY() as int);";

                var id = await connection.ExecuteScalarAsync<int>(sql, model);
                Console.WriteLine($"Record created successfully with ID: {id}");
                return Json(new { success = true, id = id, message = "Record created successfully" });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in CreateAT31: {ex.Message}");
                return Json(new { success = false, message = ex.Message });
            }
        }

        [AllowAnonymous]
        [HttpPut("api/auditdept/at31/update")]
        public async Task<IActionResult> UpdateAT31([FromBody] AT31Model model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return Json(new { success = false, errors = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage)) });

                using var connection = new SqlConnection(_connectionString);
                var sql = @"UPDATE [Quartz].[dbo].[AT31] SET 
            [CompanyName] = @CompanyName, [Activity] = @Activity, [YEtoDo] = @YEtoDo,
            [QuartertoDo] = @QuartertoDo, [PIC] = @PIC, [MthDue] = @MthDue, [Status] = @Status,
            [DocInwardsDate] = @DocInwardsDate, [Revenue] = @Revenue, [Profit] = @Profit,
            [AuditFee] = @AuditFee, [DateBilled] = @DateBilled, [StartDate] = @StartDate,
            [EndDate] = @EndDate, [DaysDone] = @DaysDone, [DonePercent] = @DonePercent,
            [Completed] = @Completed, [DateSent] = @DateSent, [DateSentToKK] = @DateSentToKK,
            [ReviewResultofDays] = @ReviewResultofDays, [DateReceiveFromKK] = @DateReceiveFromKK,
            [WhoMeetClientDate] = @WhoMeetClientDate, [DateSenttoClient] = @DateSenttoClient,
            [DateReceiveBack] = @DateReceiveBack, [TaxDueDate] = @TaxDueDate, [PasstoDept] = @PasstoDept,
            [SSMdueDate] = @SSMdueDate, [DatePasstoSecDept] = @DatePasstoSecDept, [Binded] = @Binded,
            [DespatchDateToClient] = @DespatchDateToClient
            WHERE Id = @Id";

                var affectedRows = await connection.ExecuteAsync(sql, model);
                if (affectedRows == 0)
                    return Json(new { success = false, message = "Record not found" });

                return Json(new { success = true, message = "Record updated successfully" });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in UpdateAT31: {ex.Message}");
                return Json(new { success = false, message = ex.Message });
            }
        }

        [AllowAnonymous]
        [HttpDelete("api/auditdept/at31/delete/{id}")]
        public async Task<IActionResult> DeleteAT31(int id)
        {
            try
            {
                using var connection = new SqlConnection(_connectionString);
                var sql = "DELETE FROM [Quartz].[dbo].[AT31] WHERE Id = @Id";
                var affectedRows = await connection.ExecuteAsync(sql, new { Id = id });

                if (affectedRows == 0)
                    return Json(new { success = false, message = "Record not found" });

                return Json(new { success = true, message = "Record deleted successfully" });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in DeleteAT31: {ex.Message}");
                return Json(new { success = false, message = ex.Message });
            }
        }
        #endregion

        #region AT32 (Audit) CRUD Methods
        [AllowAnonymous]
        [HttpGet("api/auditdept/at32/get-all")]
        public async Task<IActionResult> GetAllAT32Records()
        {
            try
            {
                Console.WriteLine("=== Starting GetAllAT32Records ===");
                using var connection = new SqlConnection(_connectionString);

                await connection.OpenAsync();
                Console.WriteLine("Database connection successful");

                var sql = "SELECT * FROM [Quartz].[dbo].[AT32] ORDER BY Id DESC";
                Console.WriteLine($"Executing SQL: {sql}");

                var records = await connection.QueryAsync<AT32Model>(sql);
                Console.WriteLine($"Query executed successfully. Found {records?.Count() ?? 0} records");

                return Json(new { success = true, data = records });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"!!! ERROR in GetAllAT32Records !!!");
                Console.WriteLine($"Message: {ex.Message}");
                Console.WriteLine($"Stack Trace: {ex.StackTrace}");

                if (ex.InnerException != null)
                {
                    Console.WriteLine($"Inner Exception: {ex.InnerException.Message}");
                }

                return Json(new { success = false, message = $"Database error: {ex.Message}" });
            }
        }

        [AllowAnonymous]
        [HttpGet("api/auditdept/at32/get/{id}")]
        public async Task<IActionResult> GetAT32ById(int id)
        {
            try
            {
                using var connection = new SqlConnection(_connectionString);
                var sql = "SELECT * FROM [Quartz].[dbo].[AT32] WHERE Id = @Id";
                var record = await connection.QueryFirstOrDefaultAsync<AT32Model>(sql, new { Id = id });

                if (record == null)
                    return Json(new { success = false, message = "Record not found" });

                return Json(new { success = true, data = record });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in GetAT32ById: {ex.Message}");
                return Json(new { success = false, message = ex.Message });
            }
        }

        [AllowAnonymous]
        [HttpPost("api/auditdept/at32/create")]
        public async Task<IActionResult> CreateAT32([FromBody] AT32Model model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return Json(new { success = false, errors = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage)) });

                Console.WriteLine("Creating new AT32 record...");
                using var connection = new SqlConnection(_connectionString);
                var sql = @"INSERT INTO [Quartz].[dbo].[AT32] 
            ([CompanyName], [Activity], [YeartoDo], [Quartertodo], [PIC], [Status],
             [AuditFee], [DateBilled], [StartDate], [EndDate], [NoOfDays], [TotalFieldwkDays],
             [FinalResultOverUnder], [AccSetup], [Assummary], [AuditPlanning], [AuditExecution],
             [AuditCompleion], [TotalPercent], [ReviewDateSent], [ReviewEndDate], [ReviewOfDays],
             [KKdateSent], [KKendDate], [KKofDate], [TotalReviewDays])
            VALUES 
            (@CompanyName, @Activity, @YeartoDo, @Quartertodo, @PIC, @Status,
             @AuditFee, @DateBilled, @StartDate, @EndDate, @NoOfDays, @TotalFieldwkDays,
             @FinalResultOverUnder, @AccSetup, @Assummary, @AuditPlanning, @AuditExecution,
             @AuditCompleion, @TotalPercent, @ReviewDateSent, @ReviewEndDate, @ReviewOfDays,
             @KKdateSent, @KKendDate, @KKofDate, @TotalReviewDays);
            SELECT CAST(SCOPE_IDENTITY() as int);";

                var id = await connection.ExecuteScalarAsync<int>(sql, model);
                Console.WriteLine($"Record created successfully with ID: {id}");
                return Json(new { success = true, id = id, message = "Record created successfully" });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in CreateAT32: {ex.Message}");
                return Json(new { success = false, message = ex.Message });
            }
        }

        [AllowAnonymous]
        [HttpPut("api/auditdept/at32/update")]
        public async Task<IActionResult> UpdateAT32([FromBody] AT32Model model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return Json(new { success = false, errors = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage)) });

                using var connection = new SqlConnection(_connectionString);
                var sql = @"UPDATE [Quartz].[dbo].[AT32] SET 
            [CompanyName] = @CompanyName, [Activity] = @Activity, [YeartoDo] = @YeartoDo,
            [Quartertodo] = @Quartertodo, [PIC] = @PIC, [Status] = @Status, [AuditFee] = @AuditFee,
            [DateBilled] = @DateBilled, [StartDate] = @StartDate, [EndDate] = @EndDate,
            [NoOfDays] = @NoOfDays, [TotalFieldwkDays] = @TotalFieldwkDays, [FinalResultOverUnder] = @FinalResultOverUnder,
            [AccSetup] = @AccSetup, [Assummary] = @Assummary, [AuditPlanning] = @AuditPlanning,
            [AuditExecution] = @AuditExecution, [AuditCompleion] = @AuditCompleion, [TotalPercent] = @TotalPercent,
            [ReviewDateSent] = @ReviewDateSent, [ReviewEndDate] = @ReviewEndDate, [ReviewOfDays] = @ReviewOfDays,
            [KKdateSent] = @KKdateSent, [KKendDate] = @KKendDate, [KKofDate] = @KKofDate, [TotalReviewDays] = @TotalReviewDays
            WHERE Id = @Id";

                var affectedRows = await connection.ExecuteAsync(sql, model);
                if (affectedRows == 0)
                    return Json(new { success = false, message = "Record not found" });

                return Json(new { success = true, message = "Record updated successfully" });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in UpdateAT32: {ex.Message}");
                return Json(new { success = false, message = ex.Message });
            }
        }

        [AllowAnonymous]
        [HttpDelete("api/auditdept/at32/delete/{id}")]
        public async Task<IActionResult> DeleteAT32(int id)
        {
            try
            {
                using var connection = new SqlConnection(_connectionString);
                var sql = "DELETE FROM [Quartz].[dbo].[AT32] WHERE Id = @Id";
                var affectedRows = await connection.ExecuteAsync(sql, new { Id = id });

                if (affectedRows == 0)
                    return Json(new { success = false, message = "Record not found" });

                return Json(new { success = true, message = "Record deleted successfully" });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in DeleteAT32: {ex.Message}");
                return Json(new { success = false, message = ex.Message });
            }
        }
        #endregion

        #region AT33 (AAAFSA) CRUD Methods
        [AllowAnonymous]
        [HttpGet("api/auditdept/at33/get-all")]
        public async Task<IActionResult> GetAllAT33Records()
        {
            try
            {
                Console.WriteLine("=== Starting GetAllAT33Records ===");
                using var connection = new SqlConnection(_connectionString);

                await connection.OpenAsync();
                Console.WriteLine("Database connection successful");

                var sql = "SELECT * FROM [Quartz].[dbo].[AT33] ORDER BY Id DESC";
                Console.WriteLine($"Executing SQL: {sql}");

                var records = await connection.QueryAsync<AT33Model>(sql);
                Console.WriteLine($"Query executed successfully. Found {records?.Count() ?? 0} records");

                return Json(new { success = true, data = records });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"!!! ERROR in GetAllAT33Records !!!");
                Console.WriteLine($"Message: {ex.Message}");
                Console.WriteLine($"Stack Trace: {ex.StackTrace}");

                if (ex.InnerException != null)
                {
                    Console.WriteLine($"Inner Exception: {ex.InnerException.Message}");
                }

                return Json(new { success = false, message = $"Database error: {ex.Message}" });
            }
        }

        [AllowAnonymous]
        [HttpGet("api/auditdept/at33/get/{id}")]
        public async Task<IActionResult> GetAT33ById(int id)
        {
            try
            {
                using var connection = new SqlConnection(_connectionString);
                var sql = "SELECT * FROM [Quartz].[dbo].[AT33] WHERE Id = @Id";
                var record = await connection.QueryFirstOrDefaultAsync<AT33Model>(sql, new { Id = id });

                if (record == null)
                    return Json(new { success = false, message = "Record not found" });

                return Json(new { success = true, data = record });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in GetAT33ById: {ex.Message}");
                return Json(new { success = false, message = ex.Message });
            }
        }

        [AllowAnonymous]
        [HttpPost("api/auditdept/at33/create")]
        public async Task<IActionResult> CreateAT33([FromBody] AT33Model model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return Json(new { success = false, errors = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage)) });

                Console.WriteLine("Creating new AT33 record...");
                using var connection = new SqlConnection(_connectionString);

                var sql = @"INSERT INTO [Quartz].[dbo].[AT33] 
        ([CompanyName], [YEtodo], [PIC], [Active], [AEX], 
         [DateSent], [DateReviewed], [DateSentToKK], [DateReceivedAFS], 
         [DateofAFS], [DateofDirectorsRept], [MBRSgenerated], [Remark])
        VALUES 
        (@CompanyName, @YEtodo, @PIC, @Active, @AEX, 
         @DateSent, @DateReviewed, @DateSentToKK, @DateReceivedAFS, 
         @DateofAFS, @DateofDirectorsRept, @MBRSgenerated, @Remark);
        SELECT CAST(SCOPE_IDENTITY() as int);";

                var id = await connection.ExecuteScalarAsync<int>(sql, model);
                Console.WriteLine($"Record created successfully with ID: {id}");
                return Json(new { success = true, id = id, message = "Record created successfully" });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in CreateAT33: {ex.Message}");
                Console.WriteLine($"Stack Trace: {ex.StackTrace}");
                if (ex.InnerException != null)
                {
                    Console.WriteLine($"Inner Exception: {ex.InnerException.Message}");
                }
                return Json(new { success = false, message = $"Database error: {ex.Message}" });
            }
        }

        [AllowAnonymous]
        [HttpPut("api/auditdept/at33/update")]
        public async Task<IActionResult> UpdateAT33([FromBody] AT33Model model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return Json(new { success = false, errors = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage)) });

                Console.WriteLine($"Updating AT33 record with ID: {model.Id}");
                using var connection = new SqlConnection(_connectionString);

                var sql = @"UPDATE [Quartz].[dbo].[AT33] SET 
        [CompanyName] = @CompanyName, 
        [YEtodo] = @YEtodo, 
        [PIC] = @PIC, 
        [Active] = @Active, 
        [AEX] = @AEX, 
        [DateSent] = @DateSent, 
        [DateReviewed] = @DateReviewed, 
        [DateSentToKK] = @DateSentToKK, 
        [DateReceivedAFS] = @DateReceivedAFS, 
        [DateofAFS] = @DateofAFS, 
        [DateofDirectorsRept] = @DateofDirectorsRept, 
        [MBRSgenerated] = @MBRSgenerated, 
        [Remark] = @Remark
        WHERE Id = @Id";

                var affectedRows = await connection.ExecuteAsync(sql, model);
                Console.WriteLine($"Affected rows: {affectedRows}");

                if (affectedRows == 0)
                    return Json(new { success = false, message = "Record not found" });

                return Json(new { success = true, message = "Record updated successfully" });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in UpdateAT33: {ex.Message}");
                Console.WriteLine($"Stack Trace: {ex.StackTrace}");
                if (ex.InnerException != null)
                {
                    Console.WriteLine($"Inner Exception: {ex.InnerException.Message}");
                }
                return Json(new { success = false, message = $"Database error: {ex.Message}" });
            }
        }

        [AllowAnonymous]
        [HttpDelete("api/auditdept/at33/delete/{id}")]
        public async Task<IActionResult> DeleteAT33(int id)
        {
            try
            {
                Console.WriteLine($"Deleting AT33 record with ID: {id}");
                using var connection = new SqlConnection(_connectionString);
                var sql = "DELETE FROM [Quartz].[dbo].[AT33] WHERE Id = @Id";
                var affectedRows = await connection.ExecuteAsync(sql, new { Id = id });

                Console.WriteLine($"Affected rows: {affectedRows}");

                if (affectedRows == 0)
                    return Json(new { success = false, message = "Record not found" });

                return Json(new { success = true, message = "Record deleted successfully" });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in DeleteAT33: {ex.Message}");
                Console.WriteLine($"Stack Trace: {ex.StackTrace}");
                if (ex.InnerException != null)
                {
                    Console.WriteLine($"Inner Exception: {ex.InnerException.Message}");
                }
                return Json(new { success = false, message = $"Database error: {ex.Message}" });
            }
        }
        #endregion

        #region AT34 (AADSP - Active Audit Directors Signing Pages) CRUD Methods
        [AllowAnonymous]
        [HttpGet("api/auditdept/at34/get-all")]
        public async Task<IActionResult> GetAllAT34Records()
        {
            try
            {
                Console.WriteLine("=== Starting GetAllAT34Records ===");
                using var connection = new SqlConnection(_connectionString);

                await connection.OpenAsync();
                Console.WriteLine("Database connection successful");

                var sql = "SELECT * FROM [Quartz].[dbo].[AT34] ORDER BY Id DESC";
                Console.WriteLine($"Executing SQL: {sql}");

                var records = await connection.QueryAsync<AT34Model>(sql);
                Console.WriteLine($"Query executed successfully. Found {records?.Count() ?? 0} records");

                return Json(new { success = true, data = records });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"!!! ERROR in GetAllAT34Records !!!");
                Console.WriteLine($"Message: {ex.Message}");
                Console.WriteLine($"Stack Trace: {ex.StackTrace}");

                if (ex.InnerException != null)
                {
                    Console.WriteLine($"Inner Exception: {ex.InnerException.Message}");
                }

                return Json(new { success = false, message = $"Database error: {ex.Message}" });
            }
        }

        [AllowAnonymous]
        [HttpGet("api/auditdept/at34/get/{id}")]
        public async Task<IActionResult> GetAT34ById(int id)
        {
            try
            {
                using var connection = new SqlConnection(_connectionString);
                var sql = "SELECT * FROM [Quartz].[dbo].[AT34] WHERE Id = @Id";
                var record = await connection.QueryFirstOrDefaultAsync<AT34Model>(sql, new { Id = id });

                if (record == null)
                    return Json(new { success = false, message = "Record not found" });

                return Json(new { success = true, data = record });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in GetAT34ById: {ex.Message}");
                return Json(new { success = false, message = ex.Message });
            }
        }

        [AllowAnonymous]
        [HttpPost("api/auditdept/at34/create")]
        public async Task<IActionResult> CreateAT34([FromBody] AT34Model model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return Json(new { success = false, errors = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage)) });

                Console.WriteLine("Creating new AT34 record...");
                using var connection = new SqlConnection(_connectionString);

                var sql = @"INSERT INTO [Quartz].[dbo].[AT34] 
        ([CompanyName], [YEtodo], [PIC], [Active], [AEX], 
         [DateSent], [FlwUpDates], [First18mthDate], [DateReceived], [CommofOathsDate])
        VALUES 
        (@CompanyName, @YEtodo, @PIC, @Active, @AEX, 
         @DateSent, @FlwUpDates, @First18mthDate, @DateReceived, @CommofOathsDate);
        SELECT CAST(SCOPE_IDENTITY() as int);";

                var id = await connection.ExecuteScalarAsync<int>(sql, model);
                Console.WriteLine($"Record created successfully with ID: {id}");
                return Json(new { success = true, id = id, message = "Record created successfully" });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in CreateAT34: {ex.Message}");
                Console.WriteLine($"Stack Trace: {ex.StackTrace}");
                if (ex.InnerException != null)
                {
                    Console.WriteLine($"Inner Exception: {ex.InnerException.Message}");
                }
                return Json(new { success = false, message = $"Database error: {ex.Message}" });
            }
        }

        [AllowAnonymous]
        [HttpPut("api/auditdept/at34/update")]
        public async Task<IActionResult> UpdateAT34([FromBody] AT34Model model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return Json(new { success = false, errors = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage)) });

                Console.WriteLine($"Updating AT34 record with ID: {model.Id}");
                using var connection = new SqlConnection(_connectionString);

                var sql = @"UPDATE [Quartz].[dbo].[AT34] SET 
        [CompanyName] = @CompanyName, 
        [YEtodo] = @YEtodo, 
        [PIC] = @PIC, 
        [Active] = @Active, 
        [AEX] = @AEX, 
        [DateSent] = @DateSent, 
        [FlwUpDates] = @FlwUpDates, 
        [First18mthDate] = @First18mthDate, 
        [DateReceived] = @DateReceived, 
        [CommofOathsDate] = @CommofOathsDate
        WHERE Id = @Id";

                var affectedRows = await connection.ExecuteAsync(sql, model);
                Console.WriteLine($"Affected rows: {affectedRows}");

                if (affectedRows == 0)
                    return Json(new { success = false, message = "Record not found" });

                return Json(new { success = true, message = "Record updated successfully" });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in UpdateAT34: {ex.Message}");
                Console.WriteLine($"Stack Trace: {ex.StackTrace}");
                if (ex.InnerException != null)
                {
                    Console.WriteLine($"Inner Exception: {ex.InnerException.Message}");
                }
                return Json(new { success = false, message = $"Database error: {ex.Message}" });
            }
        }

        [AllowAnonymous]
        [HttpDelete("api/auditdept/at34/delete/{id}")]
        public async Task<IActionResult> DeleteAT34(int id)
        {
            try
            {
                Console.WriteLine($"Deleting AT34 record with ID: {id}");
                using var connection = new SqlConnection(_connectionString);
                var sql = "DELETE FROM [Quartz].[dbo].[AT34] WHERE Id = @Id";
                var affectedRows = await connection.ExecuteAsync(sql, new { Id = id });

                Console.WriteLine($"Affected rows: {affectedRows}");

                if (affectedRows == 0)
                    return Json(new { success = false, message = "Record not found" });

                return Json(new { success = true, message = "Record deleted successfully" });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in DeleteAT34: {ex.Message}");
                Console.WriteLine($"Stack Trace: {ex.StackTrace}");
                if (ex.InnerException != null)
                {
                    Console.WriteLine($"Inner Exception: {ex.InnerException.Message}");
                }
                return Json(new { success = false, message = $"Database error: {ex.Message}" });
            }
        }
        #endregion

        #region AEX12 (Audit Aex Template) CRUD Methods
        [AllowAnonymous]
        [HttpGet("api/auditdept/aex12/get-all")]
        public async Task<IActionResult> GetAllAEX12Records()
        {
            try
            {
                Console.WriteLine("=== Starting GetAllAEX12Records ===");
                using var connection = new SqlConnection(_connectionString);

                await connection.OpenAsync();
                Console.WriteLine("Database connection successful");

                var sql = "SELECT * FROM [Quartz].[dbo].[AEX12] ORDER BY Id DESC";
                Console.WriteLine($"Executing SQL: {sql}");

                var records = await connection.QueryAsync<AEX12Model>(sql);
                Console.WriteLine($"Query executed successfully. Found {records?.Count() ?? 0} records");

                return Json(new { success = true, data = records });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"!!! ERROR in GetAllAEX12Records !!!");
                Console.WriteLine($"Message: {ex.Message}");
                Console.WriteLine($"Stack Trace: {ex.StackTrace}");

                if (ex.InnerException != null)
                {
                    Console.WriteLine($"Inner Exception: {ex.InnerException.Message}");
                }

                return StatusCode(500, new { success = false, message = $"Database error: {ex.Message}" });
            }
        }

        [AllowAnonymous]
        [HttpGet("api/auditdept/aex12/get/{id}")]
        public async Task<IActionResult> GetAEX12ById(int id)
        {
            try
            {
                using var connection = new SqlConnection(_connectionString);
                var sql = "SELECT * FROM [Quartz].[dbo].[AEX12] WHERE Id = @Id";
                var record = await connection.QueryFirstOrDefaultAsync<AEX12Model>(sql, new { Id = id });

                if (record == null)
                    return Json(new { success = false, message = "Record not found" });

                return Json(new { success = true, data = record });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in GetAEX12ById: {ex.Message}");
                return Json(new { success = false, message = ex.Message });
            }
        }

        [AllowAnonymous]
        [HttpPost("api/auditdept/aex12/create")]
        public async Task<IActionResult> CreateAEX12([FromBody] AEX12Model model)
        {
            try
            {
                //if (!ModelState.IsValid)
                //    return Json(new { success = false, errors = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage)) });

                Console.WriteLine("Creating new AEX12 record...");
                using var connection = new SqlConnection(_connectionString);

                var sql = @"INSERT INTO [Quartz].[dbo].[AEX12] 
            ([CompanyName], [Activity], [YEtodo], [QuarterTodo], [PIC], [Status],
             [Revenue], [ProfitLoss], [AuditFee], [DateBilled], [StartDate], [AsAt],
             [NoOfDays], [ResultOverUnder], [AccSetup], [AccSummary], [AuditPlanning],
             [AuditExecution], [AuditCompletion], [TotalPercent], [DateSentKuching],
             [EndDateKuching], [ResultOverUnderKuching], [DateSentKK], [EndDateKK],
             [ResultOverUnderKK], [Final], [DateSentToKK], [DateReceivedAR], [DateReport],
             [DateOfDirectorRept], [DateSentSigning], [FlwUpDate], [DateReceived],
             [CommOfOathsDate], [TaxDueDate], [PassToTax], [SSMDueDate], [DatePassToSecDept],
             [DateBinded], [DespatchDateToClient])
            VALUES 
            (@CompanyName, @Activity, @YEtodo, @QuarterTodo, @PIC, @Status,
             @Revenue, @ProfitLoss, @AuditFee, @DateBilled, @StartDate, @AsAt,
             @NoOfDays, @ResultOverUnder, @AccSetup, @AccSummary, @AuditPlanning,
             @AuditExecution, @AuditCompletion, @TotalPercent, @DateSentKuching,
             @EndDateKuching, @ResultOverUnderKuching, @DateSentKK, @EndDateKK,
             @ResultOverUnderKK, @Final, @DateSentToKK, @DateReceivedAR, @DateReport,
             @DateOfDirectorRept, @DateSentSigning, @FlwUpDate, @DateReceived,
             @CommOfOathsDate, @TaxDueDate, @PassToTax, @SSMDueDate, @DatePassToSecDept,
             @DateBinded, @DespatchDateToClient);
            SELECT CAST(SCOPE_IDENTITY() as int);";

                var id = await connection.ExecuteScalarAsync<int>(sql, model);
                Console.WriteLine($"Record created successfully with ID: {id}");
                return Json(new { success = true, id = id, message = "Record created successfully" });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in CreateAEX12: {ex.Message}");
                Console.WriteLine($"Stack Trace: {ex.StackTrace}");
                if (ex.InnerException != null)
                {
                    Console.WriteLine($"Inner Exception: {ex.InnerException.Message}");
                }
                return Json(new { success = false, message = $"Database error: {ex.Message}" });
            }
        }

        [AllowAnonymous]
        [HttpPut("api/auditdept/aex12/update")]
        public async Task<IActionResult> UpdateAEX12([FromBody] AEX12Model model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return Json(new { success = false, errors = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage)) });

                Console.WriteLine($"Updating AEX12 record with ID: {model.Id}");
                using var connection = new SqlConnection(_connectionString);

                var sql = @"UPDATE [Quartz].[dbo].[AEX12] SET 
            [CompanyName] = @CompanyName, 
            [Activity] = @Activity, 
            [YEtodo] = @YEtodo, 
            [QuarterTodo] = @QuarterTodo, 
            [PIC] = @PIC, 
            [Status] = @Status,
            [Revenue] = @Revenue, 
            [ProfitLoss] = @ProfitLoss, 
            [AuditFee] = @AuditFee, 
            [DateBilled] = @DateBilled, 
            [StartDate] = @StartDate, 
            [AsAt] = @AsAt,
            [NoOfDays] = @NoOfDays, 
            [ResultOverUnder] = @ResultOverUnder, 
            [AccSetup] = @AccSetup, 
            [AccSummary] = @AccSummary, 
            [AuditPlanning] = @AuditPlanning,
            [AuditExecution] = @AuditExecution, 
            [AuditCompletion] = @AuditCompletion, 
            [TotalPercent] = @TotalPercent, 
            [DateSentKuching] = @DateSentKuching,
            [EndDateKuching] = @EndDateKuching, 
            [ResultOverUnderKuching] = @ResultOverUnderKuching, 
            [DateSentKK] = @DateSentKK, 
            [EndDateKK] = @EndDateKK,
            [ResultOverUnderKK] = @ResultOverUnderKK, 
            [Final] = @Final, 
            [DateSentToKK] = @DateSentToKK, 
            [DateReceivedAR] = @DateReceivedAR, 
            [DateReport] = @DateReport,
            [DateOfDirectorRept] = @DateOfDirectorRept, 
            [DateSentSigning] = @DateSentSigning, 
            [FlwUpDate] = @FlwUpDate, 
            [DateReceived] = @DateReceived,
            [CommOfOathsDate] = @CommOfOathsDate, 
            [TaxDueDate] = @TaxDueDate, 
            [PassToTax] = @PassToTax, 
            [SSMDueDate] = @SSMDueDate, 
            [DatePassToSecDept] = @DatePassToSecDept,
            [DateBinded] = @DateBinded, 
            [DespatchDateToClient] = @DespatchDateToClient
            WHERE Id = @Id";

                var affectedRows = await connection.ExecuteAsync(sql, model);
                Console.WriteLine($"Affected rows: {affectedRows}");

                if (affectedRows == 0)
                    return Json(new { success = false, message = "Record not found" });

                return Json(new { success = true, message = "Record updated successfully" });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in UpdateAEX12: {ex.Message}");
                Console.WriteLine($"Stack Trace: {ex.StackTrace}");
                if (ex.InnerException != null)
                {
                    Console.WriteLine($"Inner Exception: {ex.InnerException.Message}");
                }
                return Json(new { success = false, message = $"Database error: {ex.Message}" });
            }
        }

        [AllowAnonymous]
        [HttpDelete("api/auditdept/aex12/delete/{id}")]
        public async Task<IActionResult> DeleteAEX12(int id)
        {
            try
            {
                Console.WriteLine($"Deleting AEX12 record with ID: {id}");
                using var connection = new SqlConnection(_connectionString);
                var sql = "DELETE FROM [Quartz].[dbo].[AEX12] WHERE Id = @Id";
                var affectedRows = await connection.ExecuteAsync(sql, new { Id = id });

                Console.WriteLine($"Affected rows: {affectedRows}");

                if (affectedRows == 0)
                    return Json(new { success = false, message = "Record not found" });

                return Json(new { success = true, message = "Record deleted successfully" });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in DeleteAEX12: {ex.Message}");
                Console.WriteLine($"Stack Trace: {ex.StackTrace}");
                if (ex.InnerException != null)
                {
                    Console.WriteLine($"Inner Exception: {ex.InnerException.Message}");
                }
                return Json(new { success = false, message = $"Database error: {ex.Message}" });
            }
        }
        #endregion

        #region AEX42 (Audit Aex Backlog) CRUD Methods
        [AllowAnonymous]
        [HttpGet("api/auditdept/aex42/get-all")]
        public async Task<IActionResult> GetAllAEX42Records()
        {
            try
            {
                Console.WriteLine("=== Starting GetAllAEX42Records ===");
                using var connection = new SqlConnection(_connectionString);

                await connection.OpenAsync();
                Console.WriteLine("Database connection successful");

                var sql = "SELECT * FROM [Quartz].[dbo].[AEX42] ORDER BY Id DESC";
                Console.WriteLine($"Executing SQL: {sql}");

                var records = await connection.QueryAsync<AEX42Model>(sql);
                Console.WriteLine($"Query executed successfully. Found {records?.Count() ?? 0} records");

                return Json(new { success = true, data = records });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"!!! ERROR in GetAllAEX42Records !!!");
                Console.WriteLine($"Message: {ex.Message}");
                Console.WriteLine($"Stack Trace: {ex.StackTrace}");

                if (ex.InnerException != null)
                {
                    Console.WriteLine($"Inner Exception: {ex.InnerException.Message}");
                }

                return Json(new { success = false, message = $"Database error: {ex.Message}" });
            }
        }

        [AllowAnonymous]
        [HttpGet("api/auditdept/aex42/get/{id}")]
        public async Task<IActionResult> GetAEX42ById(int id)
        {
            try
            {
                using var connection = new SqlConnection(_connectionString);
                var sql = "SELECT * FROM [Quartz].[dbo].[AEX42] WHERE Id = @Id";
                var record = await connection.QueryFirstOrDefaultAsync<AEX42Model>(sql, new { Id = id });

                if (record == null)
                    return Json(new { success = false, message = "Record not found" });

                return Json(new { success = true, data = record });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in GetAEX42ById: {ex.Message}");
                return Json(new { success = false, message = ex.Message });
            }
        }

        [AllowAnonymous]
        [HttpPost("api/auditdept/aex42/create")]
        public async Task<IActionResult> CreateAEX42([FromBody] AEX42Model model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return Json(new { success = false, errors = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage)) });

                Console.WriteLine("Creating new AEX42 record...");
                using var connection = new SqlConnection(_connectionString);

                var sql = @"INSERT INTO [Quartz].[dbo].[AEX42] 
        ([Grouping], [CompanyName], [QuarterToDoAudit], [Activity], [YearEnd], 
         [YearToDo], [MoveToActiveSch], [DateDocIn], [AcctngWk], [ReasonWhyBacklog])
        VALUES 
        (@Grouping, @CompanyName, @QuarterToDoAudit, @Activity, @YearEnd, 
         @YearToDo, @MoveToActiveSch, @DateDocIn, @AcctngWk, @ReasonWhyBacklog);
        SELECT CAST(SCOPE_IDENTITY() as int);";

                var id = await connection.ExecuteScalarAsync<int>(sql, model);
                Console.WriteLine($"Record created successfully with ID: {id}");
                return Json(new { success = true, id = id, message = "Record created successfully" });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in CreateAEX42: {ex.Message}");
                Console.WriteLine($"Stack Trace: {ex.StackTrace}");
                if (ex.InnerException != null)
                {
                    Console.WriteLine($"Inner Exception: {ex.InnerException.Message}");
                }
                return Json(new { success = false, message = $"Database error: {ex.Message}" });
            }
        }

        [AllowAnonymous]
        [HttpPut("api/auditdept/aex42/update")]
        public async Task<IActionResult> UpdateAEX42([FromBody] AEX42Model model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return Json(new { success = false, errors = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage)) });

                Console.WriteLine($"Updating AEX42 record with ID: {model.Id}");
                using var connection = new SqlConnection(_connectionString);

                var sql = @"UPDATE [Quartz].[dbo].[AEX42] SET 
        [Grouping] = @Grouping, 
        [CompanyName] = @CompanyName, 
        [QuarterToDoAudit] = @QuarterToDoAudit, 
        [Activity] = @Activity, 
        [YearEnd] = @YearEnd, 
        [YearToDo] = @YearToDo, 
        [MoveToActiveSch] = @MoveToActiveSch, 
        [DateDocIn] = @DateDocIn, 
        [AcctngWk] = @AcctngWk, 
        [ReasonWhyBacklog] = @ReasonWhyBacklog
        WHERE Id = @Id";

                var affectedRows = await connection.ExecuteAsync(sql, model);
                Console.WriteLine($"Affected rows: {affectedRows}");

                if (affectedRows == 0)
                    return Json(new { success = false, message = "Record not found" });

                return Json(new { success = true, message = "Record updated successfully" });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in UpdateAEX42: {ex.Message}");
                Console.WriteLine($"Stack Trace: {ex.StackTrace}");
                if (ex.InnerException != null)
                {
                    Console.WriteLine($"Inner Exception: {ex.InnerException.Message}");
                }
                return Json(new { success = false, message = $"Database error: {ex.Message}" });
            }
        }

        [AllowAnonymous]
        [HttpDelete("api/auditdept/aex42/delete/{id}")]
        public async Task<IActionResult> DeleteAEX42(int id)
        {
            try
            {
                Console.WriteLine($"Deleting AEX42 record with ID: {id}");
                using var connection = new SqlConnection(_connectionString);
                var sql = "DELETE FROM [Quartz].[dbo].[AEX42] WHERE Id = @Id";
                var affectedRows = await connection.ExecuteAsync(sql, new { Id = id });

                Console.WriteLine($"Affected rows: {affectedRows}");

                if (affectedRows == 0)
                    return Json(new { success = false, message = "Record not found" });

                return Json(new { success = true, message = "Record deleted successfully" });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in DeleteAEX42: {ex.Message}");
                Console.WriteLine($"Stack Trace: {ex.StackTrace}");
                if (ex.InnerException != null)
                {
                    Console.WriteLine($"Inner Exception: {ex.InnerException.Message}");
                }
                return Json(new { success = false, message = $"Database error: {ex.Message}" });
            }
        }
        #endregion

        #region AEX41 (AEX41S) CRUD Methods
        [AllowAnonymous]
        [HttpGet("api/auditdept/aex41/get-all")]
        public async Task<IActionResult> GetAllAEX41Records()
        {
            try
            {
                Console.WriteLine("=== Starting GetAllAEX41Records ===");
                using var connection = new SqlConnection(_connectionString);

                await connection.OpenAsync();
                Console.WriteLine("Database connection successful");

                var sql = "SELECT * FROM [Quartz].[dbo].[AEX41] ORDER BY Id DESC";
                Console.WriteLine($"Executing SQL: {sql}");

                var records = await connection.QueryAsync<AEX41Model>(sql);
                Console.WriteLine($"Query executed successfully. Found {records?.Count() ?? 0} records");

                return Json(new { success = true, data = records });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"!!! ERROR in GetAllAEX41Records !!!");
                Console.WriteLine($"Message: {ex.Message}");
                Console.WriteLine($"Stack Trace: {ex.StackTrace}");

                if (ex.InnerException != null)
                {
                    Console.WriteLine($"Inner Exception: {ex.InnerException.Message}");
                }

                return Json(new { success = false, message = $"Database error: {ex.Message}" });
            }
        }

        [AllowAnonymous]
        [HttpGet("api/auditdept/aex41/get/{id}")]
        public async Task<IActionResult> GetAEX41ById(int id)
        {
            try
            {
                using var connection = new SqlConnection(_connectionString);
                var sql = "SELECT * FROM [Quartz].[dbo].[AEX41] WHERE Id = @Id";
                var record = await connection.QueryFirstOrDefaultAsync<AEX41Model>(sql, new { Id = id });

                if (record == null)
                    return Json(new { success = false, message = "Record not found" });

                return Json(new { success = true, data = record });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in GetAEX41ById: {ex.Message}");
                return Json(new { success = false, message = ex.Message });
            }
        }

        [AllowAnonymous]
        [HttpPost("api/auditdept/aex41/create")]
        public async Task<IActionResult> CreateAEX41([FromBody] AEX41Model model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return Json(new { success = false, errors = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage)) });

                Console.WriteLine("Creating new AEX41 record...");
                using var connection = new SqlConnection(_connectionString);

                var sql = @"INSERT INTO [Quartz].[dbo].[AEX41] 
        ([Grouping], [CompanyName], [QuartertoAudit], [Activity], [YearEnd], 
         [Yeattodo], [MovetoActiveSch], [MovetoBacklog], [First18mthsdue], 
         [AuditedAccDueDate], [CoSec], [Team], [DateDocIn], [EstRev], 
         [EstNetProfit], [AcctngWk], [JobCompleted], [Remark])
        VALUES 
        (@Grouping, @CompanyName, @QuartertoAudit, @Activity, @YearEnd, 
         @Yeattodo, @MovetoActiveSch, @MovetoBacklog, @First18mthsdue, 
         @AuditedAccDueDate, @CoSec, @Team, @DateDocIn, @EstRev, 
         @EstNetProfit, @AcctngWk, @JobCompleted, @Remark);
        SELECT CAST(SCOPE_IDENTITY() as int);";

                var id = await connection.ExecuteScalarAsync<int>(sql, model);
                Console.WriteLine($"Record created successfully with ID: {id}");
                return Json(new { success = true, id = id, message = "Record created successfully" });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in CreateAEX41: {ex.Message}");
                Console.WriteLine($"Stack Trace: {ex.StackTrace}");
                if (ex.InnerException != null)
                {
                    Console.WriteLine($"Inner Exception: {ex.InnerException.Message}");
                }
                return Json(new { success = false, message = $"Database error: {ex.Message}" });
            }
        }

        [AllowAnonymous]
        [HttpPut("api/auditdept/aex41/update")]
        public async Task<IActionResult> UpdateAEX41([FromBody] AEX41Model model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return Json(new { success = false, errors = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage)) });

                Console.WriteLine($"Updating AEX41 record with ID: {model.Id}");
                using var connection = new SqlConnection(_connectionString);

                var sql = @"UPDATE [Quartz].[dbo].[AEX41] SET 
        [Grouping] = @Grouping, 
        [CompanyName] = @CompanyName, 
        [QuartertoAudit] = @QuartertoAudit, 
        [Activity] = @Activity, 
        [YearEnd] = @YearEnd, 
        [Yeattodo] = @Yeattodo, 
        [MovetoActiveSch] = @MovetoActiveSch, 
        [MovetoBacklog] = @MovetoBacklog, 
        [First18mthsdue] = @First18mthsdue, 
        [AuditedAccDueDate] = @AuditedAccDueDate, 
        [CoSec] = @CoSec, 
        [Team] = @Team, 
        [DateDocIn] = @DateDocIn, 
        [EstRev] = @EstRev, 
        [EstNetProfit] = @EstNetProfit, 
        [AcctngWk] = @AcctngWk, 
        [JobCompleted] = @JobCompleted, 
        [Remark] = @Remark
        WHERE Id = @Id";

                var affectedRows = await connection.ExecuteAsync(sql, model);
                Console.WriteLine($"Affected rows: {affectedRows}");

                if (affectedRows == 0)
                    return Json(new { success = false, message = "Record not found" });

                return Json(new { success = true, message = "Record updated successfully" });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in UpdateAEX41: {ex.Message}");
                Console.WriteLine($"Stack Trace: {ex.StackTrace}");
                if (ex.InnerException != null)
                {
                    Console.WriteLine($"Inner Exception: {ex.InnerException.Message}");
                }
                return Json(new { success = false, message = $"Database error: {ex.Message}" });
            }
        }

        [AllowAnonymous]
        [HttpDelete("api/auditdept/aex41/delete/{id}")]
        public async Task<IActionResult> DeleteAEX41(int id)
        {
            try
            {
                Console.WriteLine($"Deleting AEX41 record with ID: {id}");
                using var connection = new SqlConnection(_connectionString);
                var sql = "DELETE FROM [Quartz].[dbo].[AEX41] WHERE Id = @Id";
                var affectedRows = await connection.ExecuteAsync(sql, new { Id = id });

                Console.WriteLine($"Affected rows: {affectedRows}");

                if (affectedRows == 0)
                    return Json(new { success = false, message = "Record not found" });

                return Json(new { success = true, message = "Record deleted successfully" });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in DeleteAEX41: {ex.Message}");
                Console.WriteLine($"Stack Trace: {ex.StackTrace}");
                if (ex.InnerException != null)
                {
                    Console.WriteLine($"Inner Exception: {ex.InnerException.Message}");
                }
                return Json(new { success = false, message = $"Database error: {ex.Message}" });
            }
        }
        #endregion

        #region AEX51 (AEX51LB) CRUD Methods
        [AllowAnonymous]
        [HttpGet("api/auditdept/aex51/get-all")]
        public async Task<IActionResult> GetAllAEX51Records()
        {
            try
            {
                using var connection = new SqlConnection(_connectionString);
                var sql = "SELECT * FROM [Quartz].[dbo].[AEX51] ORDER BY Id DESC";
                var records = await connection.QueryAsync<AEX51Model>(sql);

                return Json(new { success = true, data = records });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        [AllowAnonymous]
        [HttpGet("api/auditdept/aex51/get/{id}")]
        public async Task<IActionResult> GetAEX51ById(int id)
        {
            try
            {
                using var connection = new SqlConnection(_connectionString);
                var sql = "SELECT * FROM [Quartz].[dbo].[AEX51] WHERE Id = @Id";
                var record = await connection.QueryFirstOrDefaultAsync<AEX51Model>(sql, new { Id = id });

                if (record == null)
                    return Json(new { success = false, message = "Record not found" });

                return Json(new { success = true, data = record });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        [AllowAnonymous]
        [HttpPost("api/auditdept/aex51/create")]
        public async Task<IActionResult> CreateAEX51([FromBody] AEX51Model model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return Json(new { success = false, errors = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage)) });

                using var connection = new SqlConnection(_connectionString);
                var sql = @"INSERT INTO [Quartz].[dbo].[AEX51] 
        ([CompanyName], [Activity], [YEtodo], [Quartertodo], [PIC], [First18mthDue], [Status],
         [DocInwardsDate], [Revenue], [Profit], [AuditFee], [DateBilled], [StartDate], [EndDate],
         [DonePercent], [ResultOverUnder], [Completed], [DateSent], [DateSenttoKK], [ReviewResult],
         [DateReceivedfrKK], [WhomeetClientDate], [DateSentClient], [DateReceivedBack], [TaxDueDate],
         [PasstoTaxDept], [SSMdueDate], [DatePassToSecDept], [Binded], [DespatachDateClient])
        VALUES 
        (@CompanyName, @Activity, @YEtodo, @Quartertodo, @PIC, @First18mthDue, @Status,
         @DocInwardsDate, @Revenue, @Profit, @AuditFee, @DateBilled, @StartDate, @EndDate,
         @DonePercent, @ResultOverUnder, @Completed, @DateSent, @DateSenttoKK, @ReviewResult,
         @DateReceivedfrKK, @WhomeetClientDate, @DateSentClient, @DateReceivedBack, @TaxDueDate,
         @PasstoTaxDept, @SSMdueDate, @DatePassToSecDept, @Binded, @DespatachDateClient);
        SELECT CAST(SCOPE_IDENTITY() as int);";

                var id = await connection.ExecuteScalarAsync<int>(sql, model);
                return Json(new { success = true, id = id, data = model });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        [AllowAnonymous]
        [HttpPut("api/auditdept/aex51/update")]
        public async Task<IActionResult> UpdateAEX51([FromBody] AEX51Model model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return Json(new { success = false, errors = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage)) });

                using var connection = new SqlConnection(_connectionString);
                var sql = @"UPDATE [Quartz].[dbo].[AEX51] SET 
        [CompanyName] = @CompanyName, 
        [Activity] = @Activity, 
        [YEtodo] = @YEtodo, 
        [Quartertodo] = @Quartertodo, 
        [PIC] = @PIC, 
        [First18mthDue] = @First18mthDue, 
        [Status] = @Status,
        [DocInwardsDate] = @DocInwardsDate, 
        [Revenue] = @Revenue, 
        [Profit] = @Profit, 
        [AuditFee] = @AuditFee, 
        [DateBilled] = @DateBilled, 
        [StartDate] = @StartDate, 
        [EndDate] = @EndDate,
        [DonePercent] = @DonePercent, 
        [ResultOverUnder] = @ResultOverUnder, 
        [Completed] = @Completed, 
        [DateSent] = @DateSent, 
        [DateSenttoKK] = @DateSenttoKK, 
        [ReviewResult] = @ReviewResult,
        [DateReceivedfrKK] = @DateReceivedfrKK, 
        [WhomeetClientDate] = @WhomeetClientDate, 
        [DateSentClient] = @DateSentClient, 
        [DateReceivedBack] = @DateReceivedBack, 
        [TaxDueDate] = @TaxDueDate,
        [PasstoTaxDept] = @PasstoTaxDept, 
        [SSMdueDate] = @SSMdueDate, 
        [DatePassToSecDept] = @DatePassToSecDept, 
        [Binded] = @Binded, 
        [DespatachDateClient] = @DespatachDateClient
        WHERE Id = @Id";

                var affectedRows = await connection.ExecuteAsync(sql, model);
                if (affectedRows == 0)
                    return Json(new { success = false, message = "Record not found" });

                return Json(new { success = true, data = model });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        [AllowAnonymous]
        [HttpDelete("api/auditdept/aex51/delete/{id}")]
        public async Task<IActionResult> DeleteAEX51(int id)
        {
            try
            {
                using var connection = new SqlConnection(_connectionString);
                var sql = "DELETE FROM [Quartz].[dbo].[AEX51] WHERE Id = @Id";
                var affectedRows = await connection.ExecuteAsync(sql, new { Id = id });

                if (affectedRows == 0)
                    return Json(new { success = false, message = "Record not found" });

                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }
        #endregion

        #region AEX52 (AEX52WIP) CRUD Methods
        [AllowAnonymous]
        [HttpGet("api/auditdept/aex52/get-all")]
        public async Task<IActionResult> GetAllAEX52Records()
        {
            try
            {
                using var connection = new SqlConnection(_connectionString);
                var sql = "SELECT * FROM [Quartz].[dbo].[AEX52] ORDER BY Id DESC";
                var records = await connection.QueryAsync<AEX52Model>(sql);

                return Json(new { success = true, data = records });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        [AllowAnonymous]
        [HttpGet("api/auditdept/aex52/get/{id}")]
        public async Task<IActionResult> GetAEX52ById(int id)
        {
            try
            {
                using var connection = new SqlConnection(_connectionString);
                var sql = "SELECT * FROM [Quartz].[dbo].[AEX52] WHERE Id = @Id";
                var record = await connection.QueryFirstOrDefaultAsync<AEX52Model>(sql, new { Id = id });

                if (record == null)
                    return Json(new { success = false, message = "Record not found" });

                return Json(new { success = true, data = record });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        [AllowAnonymous]
        [HttpPost("api/auditdept/aex52/create")]
        public async Task<IActionResult> CreateAEX52([FromBody] AEX52Model model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return Json(new { success = false, errors = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage)) });

                using var connection = new SqlConnection(_connectionString);
                var sql = @"INSERT INTO [Quartz].[dbo].[AEX52] 
        ([CompanyName], [Activity], [Yeartodo], [Quartertodo], [PIC], [Status],
         [AuditFee], [DateBilled], [StartDate], [EndDate], [NoofDays], [ResultOverUnder],
         [AccSetup], [AccSummary], [AuditPlanning], [AuditExecution], [AuditCompletion], [TotalPercent],
         [ReviewDateSent], [ReviewEndDate], [ReviewResultOverUnder], [KKDateSent], [KKEndDate], [KKResultOverUnder], [Final])
        VALUES 
        (@CompanyName, @Activity, @Yeartodo, @Quartertodo, @PIC, @Status,
         @AuditFee, @DateBilled, @StartDate, @EndDate, @NoofDays, @ResultOverUnder,
         @AccSetup, @AccSummary, @AuditPlanning, @AuditExecution, @AuditCompletion, @TotalPercent,
         @ReviewDateSent, @ReviewEndDate, @ReviewResultOverUnder, @KKDateSent, @KKEndDate, @KKResultOverUnder, @Final);
        SELECT CAST(SCOPE_IDENTITY() as int);";

                var id = await connection.ExecuteScalarAsync<int>(sql, model);
                return Json(new { success = true, id = id, data = model });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        [AllowAnonymous]
        [HttpPut("api/auditdept/aex52/update")]
        public async Task<IActionResult> UpdateAEX52([FromBody] AEX52Model model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return Json(new { success = false, errors = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage)) });

                using var connection = new SqlConnection(_connectionString);
                var sql = @"UPDATE [Quartz].[dbo].[AEX52] SET 
        [CompanyName] = @CompanyName, 
        [Activity] = @Activity, 
        [Yeartodo] = @Yeartodo, 
        [Quartertodo] = @Quartertodo, 
        [PIC] = @PIC, 
        [Status] = @Status,
        [AuditFee] = @AuditFee, 
        [DateBilled] = @DateBilled, 
        [StartDate] = @StartDate, 
        [EndDate] = @EndDate, 
        [NoofDays] = @NoofDays, 
        [ResultOverUnder] = @ResultOverUnder,
        [AccSetup] = @AccSetup, 
        [AccSummary] = @AccSummary, 
        [AuditPlanning] = @AuditPlanning, 
        [AuditExecution] = @AuditExecution, 
        [AuditCompletion] = @AuditCompletion, 
        [TotalPercent] = @TotalPercent,
        [ReviewDateSent] = @ReviewDateSent, 
        [ReviewEndDate] = @ReviewEndDate, 
        [ReviewResultOverUnder] = @ReviewResultOverUnder, 
        [KKDateSent] = @KKDateSent, 
        [KKEndDate] = @KKEndDate, 
        [KKResultOverUnder] = @KKResultOverUnder, 
        [Final] = @Final
        WHERE Id = @Id";

                var affectedRows = await connection.ExecuteAsync(sql, model);
                if (affectedRows == 0)
                    return Json(new { success = false, message = "Record not found" });

                return Json(new { success = true, data = model });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        [AllowAnonymous]
        [HttpDelete("api/auditdept/aex52/delete/{id}")]
        public async Task<IActionResult> DeleteAEX52(int id)
        {
            try
            {
                using var connection = new SqlConnection(_connectionString);
                var sql = "DELETE FROM [Quartz].[dbo].[AEX52] WHERE Id = @Id";
                var affectedRows = await connection.ExecuteAsync(sql, new { Id = id });

                if (affectedRows == 0)
                    return Json(new { success = false, message = "Record not found" });

                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }
        #endregion

    }
}
