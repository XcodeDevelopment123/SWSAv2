using Dapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Serialization;
using SWSA.MvcPortal.Models.AccDeptModel;
using SWSA.MvcPortal.Models.AuditDeptModel;
using SWSA.MvcPortal.Models.Clients;
using SWSA.MvcPortal.Services.Clients;
using SWSA.MvcPortal.Services.Interfaces.Clients;

namespace SWSA.MvcPortal.Controllers.AuditDept
{
    public class AuditDeptController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;
        private readonly IClientService _clientService;

        public AuditDeptController(IConfiguration configuration,IClientService clientService)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("SwsaConntection");
            _clientService = clientService;
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

                // 检查是否有 DateDocIn，如果有则创建 A31A 记录
                if (!string.IsNullOrEmpty(model.DateDocIn))
                {
                    await CreateA31AFromAT21(model);
                }

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

                // 先获取旧的记录来检查 DateDocIn 是否变化
                var oldRecordSql = "SELECT DateDocIn FROM [Quartz].[dbo].[AT21] WHERE Id = @Id";
                var oldRecord = await connection.QueryFirstOrDefaultAsync<AT21Model>(oldRecordSql, new { Id = model.Id });

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

                // 检查 DateDocIn 是否从空变为有值，如果是则创建 A31A 记录
                if (string.IsNullOrEmpty(oldRecord?.DateDocIn) && !string.IsNullOrEmpty(model.DateDocIn))
                {
                    await CreateA31AFromAT21(model);
                }

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

        // 新增方法：从 AT21 创建 A31A 记录
        private async Task CreateA31AFromAT21(AT21Model at21Model)
        {
            try
            {
                Console.WriteLine($"Creating A31A record from AT21 record (Company: {at21Model.CompanyName})");

                using var connection = new SqlConnection(_connectionString);

                var sql = @"INSERT INTO [Quartz].[dbo].[A31A] 
                ([Client], [YearEnded], [DateReceived], [NoOfBagBox], 
                 [ByWhom], [UploadLetter], [Remark], [DateSendToAD], 
                 [Date], [NoOfBoxBag], [ByWhoam2], [UploadLetter2], [Remark2])
                VALUES 
                (@Client, @YearEnded, @DateReceived, @NoOfBagBox, 
                 @ByWhom, @UploadLetter, @Remark, @DateSendToAD, 
                 @Date, @NoOfBoxBag, @ByWhoam2, @UploadLetter2, @Remark2)";

                var a31aModel = new A31AModel
                {
                    Client = at21Model.CompanyName,
                    YearEnded = at21Model.YearEnd?.ToString("yyyy-MM-dd") ?? string.Empty,
                    DateReceived = at21Model.DateDocIn, // 现在直接使用 string，不需要转换了
                    NoOfBagBox = null,
                    ByWhom = null,
                    UploadLetter = null,
                    Remark = $"Auto-created from AT21 record (ID: {at21Model.Id})",
                    DateSendToAD = null,
                    Date = null,
                    NoOfBoxBag = null,
                    ByWhoam2 = null,
                    UploadLetter2 = null,
                    Remark2 = null
                };

                await connection.ExecuteAsync(sql, a31aModel);
                Console.WriteLine($"A31A record created successfully for company: {at21Model.CompanyName}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating A31A record from AT21: {ex.Message}");
                Console.WriteLine($"Stack Trace: {ex.StackTrace}");
                // 这里不抛出异常，因为 AT21 记录创建是主要的，A31A 创建是附加功能
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

                // 检查是否有 DateDocIn，如果有则创建 A31A 记录
                if (!string.IsNullOrEmpty(model.DateDocIn))
                {
                    await CreateA31AFromAT22(model);
                }

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

                // 先获取旧的记录来检查 DateDocIn 是否变化
                var oldRecordSql = "SELECT DateDocIn FROM [Quartz].[dbo].[AT22] WHERE Id = @Id";
                var oldRecord = await connection.QueryFirstOrDefaultAsync<AT22Model>(oldRecordSql, new { Id = model.Id });

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

                // 检查 DateDocIn 是否从空变为有值，如果是则创建 A31A 记录
                if (string.IsNullOrEmpty(oldRecord?.DateDocIn) && !string.IsNullOrEmpty(model.DateDocIn))
                {
                    await CreateA31AFromAT22(model);
                }

                return Json(new { success = true, message = "Record updated successfully" });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in UpdateAT22: {ex.Message}");
                return Json(new { success = false, message = ex.Message });
            }
        }

        // 新增方法：从 AT22 创建 A31A 记录
        private async Task CreateA31AFromAT22(AT22Model at22Model)
        {
            try
            {
                Console.WriteLine($"Creating A31A record from AT22 record (Company: {at22Model.CompanyName})");

                using var connection = new SqlConnection(_connectionString);

                var sql = @"INSERT INTO [Quartz].[dbo].[A31A] 
                ([Client], [YearEnded], [DateReceived], [NoOfBagBox], 
                 [ByWhom], [UploadLetter], [Remark], [DateSendToAD], 
                 [Date], [NoOfBoxBag], [ByWhoam2], [UploadLetter2], [Remark2])
                VALUES 
                (@Client, @YearEnded, @DateReceived, @NoOfBagBox, 
                 @ByWhom, @UploadLetter, @Remark, @DateSendToAD, 
                 @Date, @NoOfBoxBag, @ByWhoam2, @UploadLetter2, @Remark2)";

                var a31aModel = new A31AModel
                {
                    Client = at22Model.CompanyName,
                    YearEnded = at22Model.YearEnd?.ToString("yyyy-MM-dd") ?? string.Empty,
                    DateReceived = at22Model.DateDocIn, // 将 AT22 的 DateDocIn 存入 A31A 的 DateReceived
                    NoOfBagBox = null,
                    ByWhom = null,
                    UploadLetter = null,
                    Remark = $"Auto-created from AT22 record (ID: {at22Model.Id})",
                    DateSendToAD = null,
                    Date = null,
                    NoOfBoxBag = null,
                    ByWhoam2 = null,
                    UploadLetter2 = null,
                    Remark2 = null
                };

                await connection.ExecuteAsync(sql, a31aModel);
                Console.WriteLine($"A31A record created successfully for company: {at22Model.CompanyName}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating A31A record from AT22: {ex.Message}");
                Console.WriteLine($"Stack Trace: {ex.StackTrace}");
                // 这里不抛出异常，因为 AT22 记录创建是主要的，A31A 创建是附加功能
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

                // 检查是否有必要字段，如果有则创建 AT31 记录
                if (!string.IsNullOrEmpty(model.StartDate) || !string.IsNullOrEmpty(model.EndDate) ||
                    !string.IsNullOrEmpty(model.NoOfDays) || !string.IsNullOrEmpty(model.TotalPercent) ||
                    !string.IsNullOrEmpty(model.ReviewDateSent) || !string.IsNullOrEmpty(model.KKdateSent))
                {
                    await CreateAT31FromAT32(model);
                }

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

                // 先获取旧的记录来检查字段是否变化
                var oldRecordSql = @"SELECT StartDate, EndDate, NoOfDays, TotalPercent, ReviewDateSent, KKdateSent 
                           FROM [Quartz].[dbo].[AT32] WHERE Id = @Id";
                var oldRecord = await connection.QueryFirstOrDefaultAsync<AT32Model>(oldRecordSql, new { Id = model.Id });

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

                // 检查相关字段是否从空变为有值，如果是则创建 AT31 记录
                bool shouldCreateAT31 =
                    (string.IsNullOrEmpty(oldRecord?.StartDate) && !string.IsNullOrEmpty(model.StartDate)) ||
                    (string.IsNullOrEmpty(oldRecord?.EndDate) && !string.IsNullOrEmpty(model.EndDate)) ||
                    (string.IsNullOrEmpty(oldRecord?.NoOfDays) && !string.IsNullOrEmpty(model.NoOfDays)) ||
                    (string.IsNullOrEmpty(oldRecord?.TotalPercent) && !string.IsNullOrEmpty(model.TotalPercent)) ||
                    (string.IsNullOrEmpty(oldRecord?.ReviewDateSent) && !string.IsNullOrEmpty(model.ReviewDateSent)) ||
                    (string.IsNullOrEmpty(oldRecord?.KKdateSent) && !string.IsNullOrEmpty(model.KKdateSent));

                if (shouldCreateAT31)
                {
                    await CreateAT31FromAT32(model);
                }

                return Json(new { success = true, message = "Record updated successfully" });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in UpdateAT32: {ex.Message}");
                return Json(new { success = false, message = ex.Message });
            }
        }

        // 新增方法：从 AT32 创建 AT31 记录
        private async Task CreateAT31FromAT32(AT32Model at32Model)
        {
            try
            {
                Console.WriteLine($"Creating AT31 record from AT32 record (Company: {at32Model.CompanyName})");

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
         @Binded, @DespatchDateToClient)";

                var at31Model = new AT31Model
                {
                    // 只设置这些字段，其他字段会自动为 null 或默认值
                    StartDate = at32Model.StartDate,
                    EndDate = at32Model.EndDate,
                    DaysDone = at32Model.NoOfDays,
                    DonePercent = at32Model.TotalPercent,
                    DateSent = at32Model.ReviewDateSent,
                    DateSentToKK = at32Model.KKdateSent,
                    ReviewResultofDays = at32Model.TotalReviewDays,

                    // 其他字段不需要设置，会保持为 null
                    CompanyName = null,
                    Activity = null,
                    YEtoDo = null,
                    QuartertoDo = null,
                    PIC = null,
                    MthDue = null,
                    Status = null,
                    DocInwardsDate = null,
                    Revenue = null,
                    Profit = null,
                    AuditFee = null,
                    DateBilled = null,
                    Completed = null,
                    DateReceiveFromKK = null,
                    WhoMeetClientDate = null,
                    DateSenttoClient = null,
                    DateReceiveBack = null,
                    TaxDueDate = null,
                    PasstoDept = null,
                    SSMdueDate = null,
                    DatePasstoSecDept = null,
                    Binded = null,
                    DespatchDateToClient = null
                };

                await connection.ExecuteAsync(sql, at31Model);
                Console.WriteLine($"AT31 record created successfully for company: {at32Model.CompanyName}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating AT31 record from AT32: {ex.Message}");
                Console.WriteLine($"Stack Trace: {ex.StackTrace}");
                // 这里不抛出异常，因为 AT32 记录创建是主要的，AT31 创建是附加功能
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

                // 调试：检查第一个记录的 MBRSgenerated 值
                if (records != null && records.Any())
                {
                    var firstRecord = records.First();
                    Console.WriteLine($"First record MBRSgenerated value: '{firstRecord.MBRSgenerated}'");
                    Console.WriteLine($"First record MBRSgenerated is null: {firstRecord.MBRSgenerated == null}");
                    Console.WriteLine($"First record MBRSgenerated is empty: {string.IsNullOrEmpty(firstRecord.MBRSgenerated)}");
                }

                // 使用 Newtonsoft.Json 的正确方式
                var jsonSettings = new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Include,
                    DefaultValueHandling = DefaultValueHandling.Include,
                    ContractResolver = new DefaultContractResolver
                    {
                        NamingStrategy = null // 保持属性名原样
                    }
                };

                return new JsonResult(new { success = true, data = records }, jsonSettings);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"!!! ERROR in GetAllAT33Records !!!");
                Console.WriteLine($"Message: {ex.Message}");
                Console.WriteLine($"Stack Trace: {ex.StackTrace}");

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

                // 检查是否有 DateReceivedAFS，如果有则创建 AT31 记录
                if (!string.IsNullOrEmpty(model.DateReceivedAFS))
                {
                    await CreateAT31FromAT33(model);
                }

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

                // 先获取旧的记录来检查 DateReceivedAFS 是否变化
                var oldRecordSql = "SELECT DateReceivedAFS FROM [Quartz].[dbo].[AT33] WHERE Id = @Id";
                var oldRecord = await connection.QueryFirstOrDefaultAsync<AT33Model>(oldRecordSql, new { Id = model.Id });

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

                // 检查 DateReceivedAFS 是否从空变为有值，如果是则创建 AT31 记录
                if (string.IsNullOrEmpty(oldRecord?.DateReceivedAFS) && !string.IsNullOrEmpty(model.DateReceivedAFS))
                {
                    await CreateAT31FromAT33(model);
                }

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

        // 新增方法：从 AT33 创建 AT31 记录
        private async Task CreateAT31FromAT33(AT33Model at33Model)
        {
            try
            {
                Console.WriteLine($"Creating AT31 record from AT33 record (Company: {at33Model.CompanyName})");

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
                 @Binded, @DespatchDateToClient)";

                var at31Model = new AT31Model
                {
                    // 将 AT33 的 DateReceivedAFS 存入 AT31 的 DateReceiveFromKK
                    DateReceiveFromKK = at33Model.DateReceivedAFS,

                    // 其他字段保持为 null
                    CompanyName = null,
                    Activity = null,
                    YEtoDo = null,
                    QuartertoDo = null,
                    PIC = null,
                    MthDue = null,
                    Status = null,
                    DocInwardsDate = null,
                    Revenue = null,
                    Profit = null,
                    AuditFee = null,
                    DateBilled = null,
                    StartDate = null,
                    EndDate = null,
                    DaysDone = null,
                    DonePercent = null,
                    Completed = null,
                    DateSent = null,
                    DateSentToKK = null,
                    ReviewResultofDays = null,
                    WhoMeetClientDate = null,
                    DateSenttoClient = null,
                    DateReceiveBack = null,
                    TaxDueDate = null,
                    PasstoDept = null,
                    SSMdueDate = null,
                    DatePasstoSecDept = null,
                    Binded = null,
                    DespatchDateToClient = null
                };

                await connection.ExecuteAsync(sql, at31Model);
                Console.WriteLine($"AT31 record created successfully for company: {at33Model.CompanyName}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating AT31 record from AT33: {ex.Message}");
                Console.WriteLine($"Stack Trace: {ex.StackTrace}");
                // 这里不抛出异常，因为 AT33 记录创建是主要的，AT31 创建是附加功能
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

                // 检查是否有 DateSent 或 DateReceived，如果有则创建或更新 AT31 记录
                if (!string.IsNullOrEmpty(model.DateSent) || !string.IsNullOrEmpty(model.DateReceived))
                {
                    await CreateOrUpdateAT31FromAT34(model);
                }

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

                // 先获取旧的记录来检查 DateSent 和 DateReceived 是否变化
                var oldRecordSql = "SELECT DateSent, DateReceived FROM [Quartz].[dbo].[AT34] WHERE Id = @Id";
                var oldRecord = await connection.QueryFirstOrDefaultAsync<AT34Model>(oldRecordSql, new { Id = model.Id });

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

                // 检查 DateSent 或 DateReceived 是否从空变为有值，如果是则创建或更新 AT31 记录
                bool shouldUpdateAT31 =
                    (string.IsNullOrEmpty(oldRecord?.DateSent) && !string.IsNullOrEmpty(model.DateSent)) ||
                    (string.IsNullOrEmpty(oldRecord?.DateReceived) && !string.IsNullOrEmpty(model.DateReceived)) ||
                    (!string.IsNullOrEmpty(oldRecord?.DateSent) && !string.IsNullOrEmpty(model.DateSent) && oldRecord.DateSent != model.DateSent) ||
                    (!string.IsNullOrEmpty(oldRecord?.DateReceived) && !string.IsNullOrEmpty(model.DateReceived) && oldRecord.DateReceived != model.DateReceived);

                if (shouldUpdateAT31)
                {
                    await CreateOrUpdateAT31FromAT34(model);
                }

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

        // 新增方法：从 AT34 创建或更新 AT31 记录
        private async Task CreateOrUpdateAT31FromAT34(AT34Model at34Model)
        {
            try
            {
                Console.WriteLine($"Creating or updating AT31 record from AT34 record (Company: {at34Model.CompanyName})");

                using var connection = new SqlConnection(_connectionString);

                // 首先检查是否已存在相同公司名的 AT31 记录
                var checkSql = "SELECT Id FROM [Quartz].[dbo].[AT31] WHERE CompanyName = @CompanyName";
                var existingId = await connection.ExecuteScalarAsync<int?>(checkSql, new { CompanyName = at34Model.CompanyName });

                if (existingId.HasValue)
                {
                    // 更新已存在的记录
                    var updateSql = @"UPDATE [Quartz].[dbo].[AT31] SET 
                            [DateSenttoClient] = @DateSenttoClient,
                            [DateReceiveBack] = @DateReceiveBack,
                            [Binded] = @Binded
                            WHERE CompanyName = @CompanyName";

                    var at31UpdateModel = new
                    {
                        DateSenttoClient = at34Model.DateSent,
                        DateReceiveBack = at34Model.DateReceived,
                        Binded = !string.IsNullOrEmpty(at34Model.CommofOathsDate) ? "Yes" : null, // 如果有 CommofOathsDate 则认为已绑定
                        CompanyName = at34Model.CompanyName
                    };

                    await connection.ExecuteAsync(updateSql, at31UpdateModel);
                    Console.WriteLine($"AT31 record updated successfully for company: {at34Model.CompanyName}");
                }
                else
                {
                    // 创建新记录
                    var insertSql = @"INSERT INTO [Quartz].[dbo].[AT31] 
                            ([CompanyName], [DateSenttoClient], [DateReceiveBack], [Binded])
                            VALUES 
                            (@CompanyName, @DateSenttoClient, @DateReceiveBack, @Binded)";

                    var at31InsertModel = new
                    {
                        CompanyName = at34Model.CompanyName,
                        DateSenttoClient = at34Model.DateSent,
                        DateReceiveBack = at34Model.DateReceived,
                        Binded = !string.IsNullOrEmpty(at34Model.CommofOathsDate) ? "Yes" : null
                    };

                    await connection.ExecuteAsync(insertSql, at31InsertModel);
                    Console.WriteLine($"AT31 record created successfully for company: {at34Model.CompanyName}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating/updating AT31 record from AT34: {ex.Message}");
                Console.WriteLine($"Stack Trace: {ex.StackTrace}");
                // 这里不抛出异常，因为 AT34 记录创建是主要的，AT31 创建是附加功能
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

                var records = await connection.QueryAsync<AEX11Model>(sql);
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
                var record = await connection.QueryFirstOrDefaultAsync<AEX11Model>(sql, new { Id = id });

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
        public async Task<IActionResult> CreateAEX12([FromBody] AEX11Model model)
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
        public async Task<IActionResult> UpdateAEX12([FromBody] AEX11Model model)
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

                // 新增逻辑：检查是否有 DateDocIn，如果有则创建 A31A 记录
                if (!string.IsNullOrEmpty(model.DateDocIn))
                {
                    await CreateA31AFromAEX41(model);
                }

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

                // 先获取旧的记录来检查 DateDocIn 是否变化
                var oldRecordSql = "SELECT DateDocIn FROM [Quartz].[dbo].[AEX41] WHERE Id = @Id";
                var oldRecord = await connection.QueryFirstOrDefaultAsync<AEX41Model>(oldRecordSql, new { Id = model.Id });

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

                // 新增逻辑：检查 DateDocIn 是否从空变为有值，如果是则创建 A31A 记录
                if (string.IsNullOrEmpty(oldRecord?.DateDocIn) && !string.IsNullOrEmpty(model.DateDocIn))
                {
                    await CreateA31AFromAEX41(model);
                }

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

        // 新增方法：从 AEX41 创建 A31A 记录
        private async Task CreateA31AFromAEX41(AEX41Model aex41Model)
        {
            try
            {
                Console.WriteLine($"Creating A31A record from AEX41 record (Company: {aex41Model.CompanyName})");

                using var connection = new SqlConnection(_connectionString);

                var sql = @"INSERT INTO [Quartz].[dbo].[A31A] 
                ([Client], [YearEnded], [DateReceived], [NoOfBagBox], 
                 [ByWhom], [UploadLetter], [Remark], [DateSendToAD], 
                 [Date], [NoOfBoxBag], [ByWhoam2], [UploadLetter2], [Remark2])
                VALUES 
                (@Client, @YearEnded, @DateReceived, @NoOfBagBox, 
                 @ByWhom, @UploadLetter, @Remark, @DateSendToAD, 
                 @Date, @NoOfBoxBag, @ByWhoam2, @UploadLetter2, @Remark2)";

                var a31aModel = new A31AModel
                {
                    Client = aex41Model.CompanyName,
                    YearEnded = aex41Model.YearEnd ?? string.Empty,
                    DateReceived = aex41Model.DateDocIn, // 将 AEX41 的 DateDocIn 存入 A31A 的 DateReceived
                    NoOfBagBox = null,
                    ByWhom = null,
                    UploadLetter = null,
                    Remark = $"Auto-created from AEX41 record (ID: {aex41Model.Id})", // 按照要求写入 remark
                    DateSendToAD = null,
                    Date = null,
                    NoOfBoxBag = null,
                    ByWhoam2 = null,
                    UploadLetter2 = null,
                    Remark2 = null
                };

                await connection.ExecuteAsync(sql, a31aModel);
                Console.WriteLine($"A31A record created successfully for company: {aex41Model.CompanyName}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating A31A record from AEX41: {ex.Message}");
                Console.WriteLine($"Stack Trace: {ex.StackTrace}");
                // 这里不抛出异常，因为 AEX41 记录创建是主要的，A31A 创建是附加功能
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

                // 新增逻辑：检查是否有 DateDocIn，如果有则创建 A31A 记录
                if (!string.IsNullOrEmpty(model.DateDocIn))
                {
                    await CreateA31AFromAEX42(model);
                }

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

                // 先获取旧的记录来检查 DateDocIn 是否变化
                var oldRecordSql = "SELECT DateDocIn FROM [Quartz].[dbo].[AEX42] WHERE Id = @Id";
                var oldRecord = await connection.QueryFirstOrDefaultAsync<AEX42Model>(oldRecordSql, new { Id = model.Id });

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

                // 新增逻辑：检查 DateDocIn 是否从空变为有值，如果是则创建 A31A 记录
                if (string.IsNullOrEmpty(oldRecord?.DateDocIn) && !string.IsNullOrEmpty(model.DateDocIn))
                {
                    await CreateA31AFromAEX42(model);
                }

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

        // 新增方法：从 AEX42 创建 A31A 记录
        private async Task CreateA31AFromAEX42(AEX42Model aex42Model)
        {
            try
            {
                Console.WriteLine($"Creating A31A record from AEX42 record (Company: {aex42Model.CompanyName})");

                using var connection = new SqlConnection(_connectionString);

                var sql = @"INSERT INTO [Quartz].[dbo].[A31A] 
                ([Client], [YearEnded], [DateReceived], [NoOfBagBox], 
                 [ByWhom], [UploadLetter], [Remark], [DateSendToAD], 
                 [Date], [NoOfBoxBag], [ByWhoam2], [UploadLetter2], [Remark2])
                VALUES 
                (@Client, @YearEnded, @DateReceived, @NoOfBagBox, 
                 @ByWhom, @UploadLetter, @Remark, @DateSendToAD, 
                 @Date, @NoOfBoxBag, @ByWhoam2, @UploadLetter2, @Remark2)";

                var a31aModel = new A31AModel
                {
                    Client = aex42Model.CompanyName,
                    YearEnded = aex42Model.YearEnd ?? string.Empty,
                    DateReceived = aex42Model.DateDocIn, // 将 AEX42 的 DateDocIn 存入 A31A 的 DateReceived
                    NoOfBagBox = null,
                    ByWhom = null,
                    UploadLetter = null,
                    Remark = $"Auto-created from AEX42 record (ID: {aex42Model.Id})", // 按照要求写入 remark
                    DateSendToAD = null,
                    Date = null,
                    NoOfBoxBag = null,
                    ByWhoam2 = null,
                    UploadLetter2 = null,
                    Remark2 = null
                };

                await connection.ExecuteAsync(sql, a31aModel);
                Console.WriteLine($"A31A record created successfully for company: {aex42Model.CompanyName}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating A31A record from AEX42: {ex.Message}");
                Console.WriteLine($"Stack Trace: {ex.StackTrace}");
                // 这里不抛出异常，因为 AEX42 记录创建是主要的，A31A 创建是附加功能
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

                // 新增逻辑：检查相关字段，创建对应记录
                await CreateRelatedRecordsFromAEX51(model);

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

                // 先获取旧的记录来检查字段是否变化
                var oldRecordSql = @"SELECT DocInwardsDate, DateSentClient, DateReceivedBack, PasstoTaxDept 
                           FROM [Quartz].[dbo].[AEX51] WHERE Id = @Id";
                var oldRecord = await connection.QueryFirstOrDefaultAsync<AEX51Model>(oldRecordSql, new { Id = model.Id });

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

                // 检查相关字段是否从空变为有值，如果是则创建对应记录
                bool shouldCreateRecords =
                    (string.IsNullOrEmpty(oldRecord?.DocInwardsDate) && !string.IsNullOrEmpty(model.DocInwardsDate)) ||
                    (string.IsNullOrEmpty(oldRecord?.DateSentClient) && !string.IsNullOrEmpty(model.DateSentClient)) ||
                    (string.IsNullOrEmpty(oldRecord?.DateReceivedBack) && !string.IsNullOrEmpty(model.DateReceivedBack)) ||
                    (string.IsNullOrEmpty(oldRecord?.PasstoTaxDept) && !string.IsNullOrEmpty(model.PasstoTaxDept));

                if (shouldCreateRecords)
                {
                    await CreateRelatedRecordsFromAEX51(model);
                }

                return Json(new { success = true, data = model });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        private async Task CreateRelatedRecordsFromAEX51(AEX51Model aex51Model)
        {
            try
            {
                Console.WriteLine($"Creating related records from AEX51 record (Company: {aex51Model.CompanyName})");

                using var connection = new SqlConnection(_connectionString);

                // 日期格式转换函数：将 12/21/2002 转换为 12-12-2002
                string ConvertDateFormat(string dateString)
                {
                    if (string.IsNullOrEmpty(dateString)) return null;

                    try
                    {
                        // 如果已经是 DD-MM-YYYY 格式，直接返回
                        if (dateString.Contains('-') && dateString.Length == 10)
                            return dateString;

                        // 将 MM/DD/YYYY 或 DD/MM/YYYY 转换为 DD-MM-YYYY
                        if (dateString.Contains('/'))
                        {
                            var parts = dateString.Split('/');
                            if (parts.Length == 3)
                            {
                                // 假设格式是 MM/DD/YYYY，转换为 DD-MM-YYYY
                                var month = parts[0];
                                var day = parts[1];
                                var year = parts[2];

                                // 确保日期格式正确
                                return $"{day.PadLeft(2, '0')}-{month.PadLeft(2, '0')}-{year}";
                            }
                        }

                        return dateString;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error converting date format '{dateString}': {ex.Message}");
                        return dateString; // 如果转换失败，返回原值
                    }
                }

                // 1. 将 DocInwardsDate 存进 A31A 的 Date
                if (!string.IsNullOrEmpty(aex51Model.DocInwardsDate))
                {
                    var convertedDocInwardsDate = ConvertDateFormat(aex51Model.DocInwardsDate);

                    var a31aSql = @"INSERT INTO [Quartz].[dbo].[A31A] 
            ([Client], [YearEnded], [DateReceived], [NoOfBagBox], 
             [ByWhom], [UploadLetter], [Remark], [DateSendToAD], 
             [Date], [NoOfBoxBag], [ByWhoam2], [UploadLetter2], [Remark2])
            VALUES 
            (@Client, @YearEnded, @DateReceived, @NoOfBagBox, 
             @ByWhom, @UploadLetter, @Remark, @DateSendToAD, 
             @Date, @NoOfBoxBag, @ByWhoam2, @UploadLetter2, @Remark2)";

                    var a31aModel = new A31AModel
                    {
                        Client = aex51Model.CompanyName,
                        YearEnded = null,
                        DateReceived = null,
                        NoOfBagBox = null,
                        ByWhom = null,
                        UploadLetter = null,
                        Remark = "Auto-created from AEX51 record",
                        DateSendToAD = null,
                        Date = convertedDocInwardsDate, // 使用转换后的日期
                        NoOfBoxBag = null,
                        ByWhoam2 = null,
                        UploadLetter2 = null,
                        Remark2 = null
                    };

                    await connection.ExecuteAsync(a31aSql, a31aModel);
                    Console.WriteLine($"A31A record created with Date: {convertedDocInwardsDate} (original: {aex51Model.DocInwardsDate})");
                }

                // 2. 将 DateSentClient 存进 AT34 的 DateReceived
                if (!string.IsNullOrEmpty(aex51Model.DateSentClient))
                {
                    var convertedDateSentClient = ConvertDateFormat(aex51Model.DateSentClient);

                    var at34Sql = @"INSERT INTO [Quartz].[dbo].[AT34] 
            ([CompanyName], [YEtodo], [PIC], [Active], [AEX], 
             [DateSent], [FlwUpDates], [First18mthDate], [DateReceived], [CommofOathsDate])
            VALUES 
            (@CompanyName, @YEtodo, @PIC, @Active, @AEX, 
             @DateSent, @FlwUpDates, @First18mthDate, @DateReceived, @CommofOathsDate)";

                    var at34Model = new AT34Model
                    {
                        CompanyName = aex51Model.CompanyName,
                        YEtodo = null,
                        PIC = aex51Model.PIC,
                        Active = "Yes",
                        AEX = "Yes",
                        DateSent = convertedDateSentClient, // 使用转换后的日期
                        FlwUpDates = null,
                        First18mthDate = null,
                        DateReceived = convertedDateSentClient, // 使用转换后的日期
                        CommofOathsDate = null
                    };

                    await connection.ExecuteAsync(at34Sql, at34Model);
                    Console.WriteLine($"AT34 record created with DateReceived: {convertedDateSentClient} (original: {aex51Model.DateSentClient})");
                }

                // 3. 将 DateReceivedBack 存进 AT34 的 CommofOathsDate
                if (!string.IsNullOrEmpty(aex51Model.DateReceivedBack))
                {
                    var convertedDateReceivedBack = ConvertDateFormat(aex51Model.DateReceivedBack);

                    var checkAt34Sql = "SELECT Id FROM [Quartz].[dbo].[AT34] WHERE CompanyName = @CompanyName";
                    var existingAt34Id = await connection.ExecuteScalarAsync<int?>(checkAt34Sql, new { CompanyName = aex51Model.CompanyName });

                    if (existingAt34Id.HasValue)
                    {
                        var updateAt34Sql = @"UPDATE [Quartz].[dbo].[AT34] SET 
                        [CommofOathsDate] = @CommofOathsDate
                        WHERE CompanyName = @CompanyName";

                        await connection.ExecuteAsync(updateAt34Sql, new
                        {
                            CommofOathsDate = convertedDateReceivedBack, // 使用转换后的日期
                            CompanyName = aex51Model.CompanyName
                        });
                        Console.WriteLine($"AT34 record updated with CommofOathsDate: {convertedDateReceivedBack} (original: {aex51Model.DateReceivedBack})");
                    }
                    else
                    {
                        var insertAt34Sql = @"INSERT INTO [Quartz].[dbo].[AT34] 
                        ([CompanyName], [YEtodo], [PIC], [Active], [AEX], 
                         [DateSent], [FlwUpDates], [First18mthDate], [DateReceived], [CommofOathsDate])
                        VALUES 
                        (@CompanyName, @YEtodo, @PIC, @Active, @AEX, 
                         @DateSent, @FlwUpDates, @First18mthDate, @DateReceived, @CommofOathsDate)";

                        var at34Model = new AT34Model
                        {
                            CompanyName = aex51Model.CompanyName,
                            YEtodo = null,
                            PIC = aex51Model.PIC,
                            Active = "Yes",
                            AEX = "Yes",
                            DateSent = null,
                            FlwUpDates = null,
                            First18mthDate = null,
                            DateReceived = null,
                            CommofOathsDate = convertedDateReceivedBack // 使用转换后的日期
                        };

                        await connection.ExecuteAsync(insertAt34Sql, at34Model);
                        Console.WriteLine($"AT34 record created with CommofOathsDate: {convertedDateReceivedBack} (original: {aex51Model.DateReceivedBack})");
                    }
                }

                // 4. 将 PasstoTaxDept 存进 TX2 的 AEXOT
                if (!string.IsNullOrEmpty(aex51Model.PasstoTaxDept))
                {
                    try
                    {
                        var convertedPassToTax = ConvertDateFormat(aex51Model.PasstoTaxDept);
                        var convertedTaxDueDate = ConvertDateFormat(aex51Model.TaxDueDate);
                        var convertedStartDate = ConvertDateFormat(aex51Model.StartDate);

                        Console.WriteLine($"Attempting to insert into TX2 with CompanyName: {aex51Model.CompanyName}, AEXOT: {convertedPassToTax}");

                        var tx2Sql = @"
                INSERT INTO [Quartz].[dbo].[TX2] 
                ([CompanyName], [Activity], [AEXOT], [RAKC], [BTM], [YearEnd], 
                 [TaxDueDate], [EstMthTodo], [TransferToWIPTX3], [Revenue], 
                 [NetProfit], [StartDate], [TotalPercent], [DocPassFrAuditDept], 
                 [DateMgmtAccAvailable])
                VALUES 
                (@CompanyName, @Activity, @AEXOT, @RAKC, @BTM, @YearEnd, 
                 @TaxDueDate, @EstMthTodo, @TransferToWIPTX3, @Revenue, 
                 @NetProfit, @StartDate, @TotalPercent, @DocPassFrAuditDept, 
                 @DateMgmtAccAvailable)";

                        var tx2Model = new
                        {
                            CompanyName = aex51Model.CompanyName,
                            Activity = aex51Model.Activity ?? "Audit",
                            AEXOT = convertedPassToTax, // 使用转换后的日期
                            RAKC = null as string,
                            BTM = null as string,
                            YearEnd = null as string,
                            TaxDueDate = convertedTaxDueDate, // 使用转换后的日期
                            EstMthTodo = null as string,
                            TransferToWIPTX3 = null as string,
                            Revenue = aex51Model.Revenue,
                            NetProfit = aex51Model.Profit,
                            StartDate = convertedStartDate, // 使用转换后的日期
                            TotalPercent = aex51Model.DonePercent,
                            DocPassFrAuditDept = convertedPassToTax, // 使用转换后的日期
                            DateMgmtAccAvailable = null as string
                        };

                        var affectedRows = await connection.ExecuteAsync(tx2Sql, tx2Model);
                        Console.WriteLine($"✅ TX2 record created successfully. Affected rows: {affectedRows}");
                    }
                    catch (Exception tx2Ex)
                    {
                        Console.WriteLine($"❌ Error creating TX2 record: {tx2Ex.Message}");

                        // 简化版本
                        try
                        {
                            Console.WriteLine("🔄 Trying simplified TX2 insert...");
                            var simpleTx2Sql = @"
                    INSERT INTO [Quartz].[dbo].[TX2] 
                    ([CompanyName], [AEXOT])
                    VALUES 
                    (@CompanyName, @AEXOT)";

                            var simpleModel = new
                            {
                                CompanyName = aex51Model.CompanyName,
                                AEXOT = ConvertDateFormat(aex51Model.PasstoTaxDept) // 使用转换后的日期
                            };

                            var simpleAffectedRows = await connection.ExecuteAsync(simpleTx2Sql, simpleModel);
                            Console.WriteLine($"✅ Simplified TX2 insert successful. Affected rows: {simpleAffectedRows}");
                        }
                        catch (Exception simpleEx)
                        {
                            Console.WriteLine($"❌ Simplified TX2 insert also failed: {simpleEx.Message}");
                        }
                    }
                }

                Console.WriteLine($"All related records creation completed for company: {aex51Model.CompanyName}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating related records from AEX51: {ex.Message}");
                Console.WriteLine($"Stack Trace: {ex.StackTrace}");
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

                // 新增逻辑：检查相关字段，创建或更新 AEX51 记录
                await CreateOrUpdateAEX51FromAEX52(model);

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

                // 先获取旧的记录来检查字段是否变化
                var oldRecordSql = @"SELECT NoofDays, ResultOverUnder, ReviewDateSent 
                           FROM [Quartz].[dbo].[AEX52] WHERE Id = @Id";
                var oldRecord = await connection.QueryFirstOrDefaultAsync<AEX52Model>(oldRecordSql, new { Id = model.Id });

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

                // 检查相关字段是否变化，如果是则更新 AEX51 记录
                bool shouldUpdateAEX51 =
                    (string.IsNullOrEmpty(oldRecord?.NoofDays) && !string.IsNullOrEmpty(model.NoofDays)) ||
                    (string.IsNullOrEmpty(oldRecord?.ResultOverUnder) && !string.IsNullOrEmpty(model.ResultOverUnder)) ||
                    (string.IsNullOrEmpty(oldRecord?.ReviewDateSent) && !string.IsNullOrEmpty(model.ReviewDateSent)) ||
                    (!string.IsNullOrEmpty(oldRecord?.NoofDays) && !string.IsNullOrEmpty(model.NoofDays) && oldRecord.NoofDays != model.NoofDays) ||
                    (!string.IsNullOrEmpty(oldRecord?.ResultOverUnder) && !string.IsNullOrEmpty(model.ResultOverUnder) && oldRecord.ResultOverUnder != model.ResultOverUnder) ||
                    (!string.IsNullOrEmpty(oldRecord?.ReviewDateSent) && !string.IsNullOrEmpty(model.ReviewDateSent) && oldRecord.ReviewDateSent != model.ReviewDateSent);

                if (shouldUpdateAEX51)
                {
                    await CreateOrUpdateAEX51FromAEX52(model);
                }

                return Json(new { success = true, data = model });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        // 新增方法：从 AEX52 创建或更新 AEX51 记录
        private async Task CreateOrUpdateAEX51FromAEX52(AEX52Model aex52Model)
        {
            try
            {
                Console.WriteLine($"Creating or updating AEX51 record from AEX52 record (Company: {aex52Model.CompanyName})");

                using var connection = new SqlConnection(_connectionString);

                // 首先检查是否已存在相同公司名的 AEX51 记录
                var checkSql = "SELECT Id FROM [Quartz].[dbo].[AEX51] WHERE CompanyName = @CompanyName";
                var existingId = await connection.ExecuteScalarAsync<int?>(checkSql, new { CompanyName = aex52Model.CompanyName });

                if (existingId.HasValue)
                {
                    // 更新已存在的 AEX51 记录
                    var updateSql = @"UPDATE [Quartz].[dbo].[AEX51] SET 
                        [DonePercent] = @DonePercent,
                        [ResultOverUnder] = @ResultOverUnder,
                        [DateSent] = @DateSent,
                        [Remark] = @Remark
                        WHERE CompanyName = @CompanyName";

                    var aex51UpdateModel = new
                    {
                        DonePercent = aex52Model.NoofDays, // AEX52 的 NoofDays 存入 AEX51 的 DonePercent
                        ResultOverUnder = aex52Model.ResultOverUnder, // AEX52 的 ResultOverUnder 存入 AEX51 的 ResultOverUnder
                        DateSent = aex52Model.ReviewDateSent, // AEX52 的 ReviewDateSent 存入 AEX51 的 DateSent
                        Remark = $"Auto-updated from AEX52 record (ID: {aex52Model.Id})",
                        CompanyName = aex52Model.CompanyName
                    };

                    var affectedRows = await connection.ExecuteAsync(updateSql, aex51UpdateModel);
                    Console.WriteLine($"AEX51 record updated successfully. Affected rows: {affectedRows}");
                    Console.WriteLine($"AEX51 updated with - DonePercent: {aex52Model.NoofDays}, ResultOverUnder: {aex52Model.ResultOverUnder}, DateSent: {aex52Model.ReviewDateSent}");
                }
                else
                {
                    // 创建新的 AEX51 记录
                    var insertSql = @"INSERT INTO [Quartz].[dbo].[AEX51] 
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
                         @PasstoTaxDept, @SSMdueDate, @DatePassToSecDept, @Binded, @DespatachDateClient)";

                    var aex51Model = new AEX51Model
                    {
                        CompanyName = aex52Model.CompanyName,
                        Activity = aex52Model.Activity,
                        YEtodo = aex52Model.Yeartodo,
                        Quartertodo = aex52Model.Quartertodo,
                        PIC = aex52Model.PIC,
                        First18mthDue = null,
                        Status = aex52Model.Status,
                        DocInwardsDate = null,
                        Revenue = aex52Model.AuditFee, // 使用 AuditFee 作为 Revenue
                        Profit = null,
                        AuditFee = aex52Model.AuditFee,
                        DateBilled = aex52Model.DateBilled,
                        StartDate = aex52Model.StartDate,
                        EndDate = aex52Model.EndDate,
                        DonePercent = aex52Model.NoofDays, // AEX52 的 NoofDays 存入 AEX51 的 DonePercent
                        ResultOverUnder = aex52Model.ResultOverUnder, // AEX52 的 ResultOverUnder 存入 AEX51 的 ResultOverUnder
                        Completed = "No",
                        DateSent = aex52Model.ReviewDateSent, // AEX52 的 ReviewDateSent 存入 AEX51 的 DateSent
                        DateSenttoKK = aex52Model.KKDateSent,
                        ReviewResult = aex52Model.ReviewResultOverUnder,
                        DateReceivedfrKK = null,
                        WhomeetClientDate = null,
                        DateSentClient = null,
                        DateReceivedBack = null,
                        TaxDueDate = null,
                        PasstoTaxDept = null,
                        SSMdueDate = null,
                        DatePassToSecDept = null,
                        Binded = null,
                        DespatachDateClient = null
                    };

                    await connection.ExecuteAsync(insertSql, aex51Model);
                    Console.WriteLine($"AEX51 record created successfully for company: {aex52Model.CompanyName}");
                    Console.WriteLine($"AEX51 created with - DonePercent: {aex52Model.NoofDays}, ResultOverUnder: {aex52Model.ResultOverUnder}, DateSent: {aex52Model.ReviewDateSent}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating/updating AEX51 record from AEX52: {ex.Message}");
                Console.WriteLine($"Stack Trace: {ex.StackTrace}");
                // 这里不抛出异常，因为 AEX52 记录创建是主要的，AEX51 创建是附加功能
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


        #region Link Client List
        [AllowAnonymous]
        [HttpGet("api/auditdept/clients/get-list")]
        public async Task<IActionResult> GetClientList()
        {
            try
            {
                // 直接复用 ClientService 里的逻辑
                // 这会返回包含 SdnBhd, LLP, Enterprise, Individual 的完整列表
                var list = await _clientService.GetAllClientsListAsync();

                return Json(new { success = true, data = list });
            }
            catch (Exception ex)
            {
                // 简单的错误记录
                Console.WriteLine($"Error getting client list: {ex.Message}");
                return Json(new { success = false, message = ex.Message });
            }
        }
        #endregion
    }
}
