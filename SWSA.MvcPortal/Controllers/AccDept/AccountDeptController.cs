using Dapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using SWSA.MvcPortal.Models;
using SWSA.MvcPortal.Models.AccDeptModel;

namespace SWSA.MvcPortal.Controllers.AccDept
{
    public class AccountDeptController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;

        public AccountDeptController(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("SwsaConntection");
        }

        public IActionResult SdnBhdMasterScheduleList()
        {
            return View();
        }

        public IActionResult LLPMasterScheduleList()
        {
            return View();
        }

        public IActionResult FormBnPMasterScheduleList()
        {
            return View();
        }

        public IActionResult ClientAccAuditMasterSchedulingList()
        {
            return View();
        }

        public IActionResult IndividuaFormBEMasterSchedulingList()
        {
            return View();
        }

        public IActionResult FormEMasterSchedule()
        {
            return View();
        }
        public IActionResult SdnBhdBacklog()
        {
            return View();
        }
        public IActionResult LLPBacklog()
        {
            return View();
        }
        public IActionResult FormBnPBacklog()
        {
            return View();
        }
        public IActionResult BP32()
        {
            return View();
        }
        public IActionResult BP33()
        {
            return View();
        }
        public IActionResult BP34()
        {
            return View();
        }

        #region BP21 (Sdn Bhd Master Schedule) CRUD Methods
        [AllowAnonymous]
        [HttpGet("api/bp21/get-all")]
        public async Task<IActionResult> GetAllBP21Records()
        {
            try
            {
                Console.WriteLine("=== Starting GetAllBP21Records ===");
                using var connection = new SqlConnection(_connectionString);

                await connection.OpenAsync();
                Console.WriteLine("Database connection successful");

                var sql = "SELECT * FROM [Quartz].[dbo].[BP21] ORDER BY Id DESC";
                Console.WriteLine($"Executing SQL: {sql}");

                var records = await connection.QueryAsync<BP21Model>(sql);
                Console.WriteLine($"Query executed successfully. Found {records?.Count() ?? 0} records");

                return Json(new { success = true, data = records });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"!!! ERROR in GetAllBP21Records !!!");
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
        [HttpGet("api/bp21/get/{id}")]
        public async Task<IActionResult> GetBP21ById(int id)
        {
            try
            {
                using var connection = new SqlConnection(_connectionString);
                var sql = "SELECT * FROM [Quartz].[dbo].[BP21] WHERE Id = @Id";
                var record = await connection.QueryFirstOrDefaultAsync<BP21Model>(sql, new { Id = id });

                if (record == null)
                    return Json(new { success = false, message = "Record not found" });

                return Json(new { success = true, data = record });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in GetBP21ById: {ex.Message}");
                return Json(new { success = false, message = ex.Message });
            }
        }

        [AllowAnonymous]
        [HttpPost("api/bp21/create")]
        public async Task<IActionResult> CreateBP21([FromBody] BP21Model model)
        {
            try
            {
                Console.WriteLine("Creating new BP21 record...");
                using var connection = new SqlConnection(_connectionString);
                var sql = @"INSERT INTO [Quartz].[dbo].[BP21] 
                    ([Grouping], [Refferal], [FileNo], [CompanyName], [YearEnd],
                     [IncorpDate], [CO], [Enumber], [TINnumber], [Code],
                     [Description], [ServiceType], [CoStatus], [ActiveCoActivitySize],
                     [YEtodo], [AuditDeptMth], [DueDate], [ESTmthToDo], [DateDocIn], [Staff],
                     [AllocateToWkSch], [Completed])
                    VALUES 
                    (@Grouping, @Refferal, @FileNo, @CompanyName, @YearEnd,
                     @IncorpDate, @CO, @Enumber, @TINnumber, @Code,
                     @Description, @ServiceType, @CoStatus, @ActiveCoActivitySize,
                     @YEtodo, @AuditDeptMth, @DueDate, @ESTmthToDo, @DateDocIn, @Staff,
                     @AllocateToWkSch, @Completed);
                    SELECT CAST(SCOPE_IDENTITY() as int);";

                var id = await connection.ExecuteScalarAsync<int>(sql, model);
                Console.WriteLine($"Record created successfully with ID: {id}");
                return Json(new { success = true, id = id, message = "Record created successfully" });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in CreateBP21: {ex.Message}");
                return Json(new { success = false, message = ex.Message });
            }
        }

        [AllowAnonymous]
        [HttpPut("api/bp21/update")]
        public async Task<IActionResult> UpdateBP21([FromBody] BP21Model model)
        {
            try
            {
                using var connection = new SqlConnection(_connectionString);
                var sql = @"UPDATE [Quartz].[dbo].[BP21] SET 
                    [Grouping] = @Grouping, [Refferal] = @Refferal, [FileNo] = @FileNo,
                    [CompanyName] = @CompanyName, [YearEnd] = @YearEnd,
                    [IncorpDate] = @IncorpDate, [CO] = @CO, [Enumber] = @Enumber,
                    [TINnumber] = @TINnumber, [Code] = @Code, [Description] = @Description,
                    [ServiceType] = @ServiceType, [CoStatus] = @CoStatus,
                    [ActiveCoActivitySize] = @ActiveCoActivitySize, [YEtodo] = @YEtodo,
                    [AuditDeptMth] = @AuditDeptMth, [DueDate] = @DueDate, 
                    [ESTmthToDo] = @ESTmthToDo, [DateDocIn] = @DateDocIn, [Staff] = @Staff,
                    [AllocateToWkSch] = @AllocateToWkSch, [Completed] = @Completed
                    WHERE Id = @Id";

                var affectedRows = await connection.ExecuteAsync(sql, model);
                if (affectedRows == 0)
                    return Json(new { success = false, message = "Record not found" });

                return Json(new { success = true, message = "Record updated successfully" });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in UpdateBP21: {ex.Message}");
                return Json(new { success = false, message = ex.Message });
            }
        }

        [AllowAnonymous]
        [HttpDelete("api/bp21/delete/{id}")]
        public async Task<IActionResult> DeleteBP21(int id)
        {
            try
            {
                using var connection = new SqlConnection(_connectionString);
                var sql = "DELETE FROM [Quartz].[dbo].[BP21] WHERE Id = @Id";
                var affectedRows = await connection.ExecuteAsync(sql, new { Id = id });

                if (affectedRows == 0)
                    return Json(new { success = false, message = "Record not found" });

                return Json(new { success = true, message = "Record deleted successfully" });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in DeleteBP21: {ex.Message}");
                return Json(new { success = false, message = ex.Message });
            }
        }
        #endregion

        #region BP22 (LLP Master Schedule) CRUD Methods
        [AllowAnonymous]
        [HttpGet("api/bp22/get-all")]
        public async Task<IActionResult> GetAllBP22Records()
        {
            try
            {
                Console.WriteLine("=== Starting GetAllBP22Records ===");
                using var connection = new SqlConnection(_connectionString);

                await connection.OpenAsync();
                Console.WriteLine("Database connection successful");

                var sql = "SELECT * FROM [Quartz].[dbo].[BP22] ORDER BY Id DESC";
                Console.WriteLine($"Executing SQL: {sql}");

                var records = await connection.QueryAsync<BP22Model>(sql);
                Console.WriteLine($"Query executed successfully. Found {records?.Count() ?? 0} records");

                return Json(new { success = true, data = records });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"!!! ERROR in GetAllBP22Records !!!");
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
        [HttpGet("api/bp22/get/{id}")]
        public async Task<IActionResult> GetBP22ById(int id)
        {
            try
            {
                using var connection = new SqlConnection(_connectionString);
                var sql = "SELECT * FROM [Quartz].[dbo].[BP22] WHERE Id = @Id";
                var record = await connection.QueryFirstOrDefaultAsync<BP22Model>(sql, new { Id = id });

                if (record == null)
                    return Json(new { success = false, message = "Record not found" });

                return Json(new { success = true, data = record });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in GetBP22ById: {ex.Message}");
                return Json(new { success = false, message = ex.Message });
            }
        }

        [AllowAnonymous]
        [HttpPost("api/bp22/create")]
        public async Task<IActionResult> CreateBP22([FromBody] BP22Model model)
        {
            try
            {
                Console.WriteLine("Creating new BP22 record...");
                using var connection = new SqlConnection(_connectionString);
                var sql = @"INSERT INTO [Quartz].[dbo].[BP22] 
                    ([Grouping], [Refferal], [FileNo], [CompanyName], [YearEnd],
                     [IncorpDate], [CO], [Enumber], [TINnumber], [Code],
                     [Description], [ServicesType], [ActiveCoActivitySize], [YEtodo],
                     [ADdueDate], [ExtensionDate], [DateDocIn], [MthToDo], [Staff],
                     [AllocateToWkSch], [Completed])
                    VALUES 
                    (@Grouping, @Refferal, @FileNo, @CompanyName, @YearEnd,
                     @IncorpDate, @CO, @Enumber, @TINnumber, @Code,
                     @Description, @ServicesType, @ActiveCoActivitySize, @YEtodo,
                     @ADdueDate, @ExtensionDate, @DateDocIn, @MthToDo, @Staff,
                     @AllocateToWkSch, @Completed);
                    SELECT CAST(SCOPE_IDENTITY() as int);";

                var id = await connection.ExecuteScalarAsync<int>(sql, model);
                Console.WriteLine($"Record created successfully with ID: {id}");
                return Json(new { success = true, id = id, message = "Record created successfully" });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in CreateBP22: {ex.Message}");
                return Json(new { success = false, message = ex.Message });
            }
        }

        [AllowAnonymous]
        [HttpPut("api/bp22/update")]
        public async Task<IActionResult> UpdateBP22([FromBody] BP22Model model)
        {
            try
            {
                using var connection = new SqlConnection(_connectionString);
                var sql = @"UPDATE [Quartz].[dbo].[BP22] SET 
                    [Grouping] = @Grouping, [Refferal] = @Refferal, [FileNo] = @FileNo,
                    [CompanyName] = @CompanyName, [YearEnd] = @YearEnd,
                    [IncorpDate] = @IncorpDate, [CO] = @CO, [Enumber] = @Enumber,
                    [TINnumber] = @TINnumber, [Code] = @Code, [Description] = @Description,
                    [ServicesType] = @ServicesType, [ActiveCoActivitySize] = @ActiveCoActivitySize,
                    [YEtodo] = @YEtodo, [ADdueDate] = @ADdueDate, [ExtensionDate] = @ExtensionDate,
                    [DateDocIn] = @DateDocIn, [MthToDo] = @MthToDo, [Staff] = @Staff,
                    [AllocateToWkSch] = @AllocateToWkSch, [Completed] = @Completed
                    WHERE Id = @Id";

                var affectedRows = await connection.ExecuteAsync(sql, model);
                if (affectedRows == 0)
                    return Json(new { success = false, message = "Record not found" });

                return Json(new { success = true, message = "Record updated successfully" });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in UpdateBP22: {ex.Message}");
                return Json(new { success = false, message = ex.Message });
            }
        }

        [AllowAnonymous]
        [HttpDelete("api/bp22/delete/{id}")]
        public async Task<IActionResult> DeleteBP22(int id)
        {
            try
            {
                using var connection = new SqlConnection(_connectionString);
                var sql = "DELETE FROM [Quartz].[dbo].[BP22] WHERE Id = @Id";
                var affectedRows = await connection.ExecuteAsync(sql, new { Id = id });

                if (affectedRows == 0)
                    return Json(new { success = false, message = "Record not found" });

                return Json(new { success = true, message = "Record deleted successfully" });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in DeleteBP22: {ex.Message}");
                return Json(new { success = false, message = ex.Message });
            }
        }
        #endregion

        #region BP23 (Form BnP Master Schedule) CRUD Methods
        [AllowAnonymous]
        [HttpGet("api/bp23/get-all")]
        public async Task<IActionResult> GetAllBP23Records()
        {
            try
            {
                Console.WriteLine("=== Starting GetAllBP23Records ===");
                using var connection = new SqlConnection(_connectionString);

                await connection.OpenAsync();
                Console.WriteLine("Database connection successful");

                var sql = "SELECT * FROM [Quartz].[dbo].[BP23] ORDER BY Id DESC";
                Console.WriteLine($"Executing SQL: {sql}");

                var records = await connection.QueryAsync<BP23Model>(sql);
                Console.WriteLine($"Query executed successfully. Found {records?.Count() ?? 0} records");

                return Json(new { success = true, data = records });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"!!! ERROR in GetAllBP23Records !!!");
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
        [HttpGet("api/bp23/get/{id}")]
        public async Task<IActionResult> GetBP23ById(int id)
        {
            try
            {
                using var connection = new SqlConnection(_connectionString);
                var sql = "SELECT * FROM [Quartz].[dbo].[BP23] WHERE Id = @Id";
                var record = await connection.QueryFirstOrDefaultAsync<BP23Model>(sql, new { Id = id });

                if (record == null)
                    return Json(new { success = false, message = "Record not found" });

                return Json(new { success = true, data = record });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in GetBP23ById: {ex.Message}");
                return Json(new { success = false, message = ex.Message });
            }
        }

        [AllowAnonymous]
        [HttpPost("api/bp23/create")]
        public async Task<IActionResult> CreateBP23([FromBody] BP23Model model)
        {
            try
            {
                Console.WriteLine("Creating new BP23 record...");
                using var connection = new SqlConnection(_connectionString);
                var sql = @"INSERT INTO [Quartz].[dbo].[BP23] 
                    ([Grouping], [Refferal], [FileNo], [CompanyName], [YearEnd],
                     [RegistrationDate], [CO], [Enumber], [TINnumber], [Login],
                     [Password], [Code], [Description], [JobService], [ActiveCoActivitySize],
                     [YEtoDo], [DateDocIn], [MthTodo], [Staff], [AllocateToWkSch], [Completed])
                    VALUES 
                    (@Grouping, @Refferal, @FileNo, @CompanyName, @YearEnd,
                     @RegistrationDate, @CO, @Enumber, @TINnumber, @Login,
                     @Password, @Code, @Description, @JobService, @ActiveCoActivitySize,
                     @YEtoDo, @DateDocIn, @MthTodo, @Staff, @AllocateToWkSch, @Completed);
                    SELECT CAST(SCOPE_IDENTITY() as int);";

                var id = await connection.ExecuteScalarAsync<int>(sql, model);
                Console.WriteLine($"Record created successfully with ID: {id}");
                return Json(new { success = true, id = id, message = "Record created successfully" });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in CreateBP23: {ex.Message}");
                return Json(new { success = false, message = ex.Message });
            }
        }

        [AllowAnonymous]
        [HttpPut("api/bp23/update")]
        public async Task<IActionResult> UpdateBP23([FromBody] BP23Model model)
        {
            try
            {
                using var connection = new SqlConnection(_connectionString);
                var sql = @"UPDATE [Quartz].[dbo].[BP23] SET 
                    [Grouping] = @Grouping, [Refferal] = @Refferal, [FileNo] = @FileNo,
                    [CompanyName] = @CompanyName, [YearEnd] = @YearEnd,
                    [RegistrationDate] = @RegistrationDate, [CO] = @CO, [Enumber] = @Enumber,
                    [TINnumber] = @TINnumber, [Login] = @Login, [Password] = @Password,
                    [Code] = @Code, [Description] = @Description, [JobService] = @JobService,
                    [ActiveCoActivitySize] = @ActiveCoActivitySize, [YEtoDo] = @YEtoDo,
                    [DateDocIn] = @DateDocIn, [MthTodo] = @MthTodo, [Staff] = @Staff,
                    [AllocateToWkSch] = @AllocateToWkSch, [Completed] = @Completed
                    WHERE Id = @Id";

                var affectedRows = await connection.ExecuteAsync(sql, model);
                if (affectedRows == 0)
                    return Json(new { success = false, message = "Record not found" });

                return Json(new { success = true, message = "Record updated successfully" });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in UpdateBP23: {ex.Message}");
                return Json(new { success = false, message = ex.Message });
            }
        }

        [AllowAnonymous]
        [HttpDelete("api/bp23/delete/{id}")]
        public async Task<IActionResult> DeleteBP23(int id)
        {
            try
            {
                using var connection = new SqlConnection(_connectionString);
                var sql = "DELETE FROM [Quartz].[dbo].[BP23] WHERE Id = @Id";
                var affectedRows = await connection.ExecuteAsync(sql, new { Id = id });

                if (affectedRows == 0)
                    return Json(new { success = false, message = "Record not found" });

                return Json(new { success = true, message = "Record deleted successfully" });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in DeleteBP23: {ex.Message}");
                return Json(new { success = false, message = ex.Message });
            }
        }
        #endregion

        #region BP24 (Client Acc Audit Master Schedule) CRUD Methods
        [AllowAnonymous]
        [HttpGet("api/bp24/get-all")]
        public async Task<IActionResult> GetAllBP24Records()
        {
            try
            {
                Console.WriteLine("=== Starting GetAllBP24Records ===");
                using var connection = new SqlConnection(_connectionString);

                await connection.OpenAsync();
                Console.WriteLine("Database connection successful");

                var sql = "SELECT * FROM [Quartz].[dbo].[BP24] ORDER BY Id DESC";
                Console.WriteLine($"Executing SQL: {sql}");

                var records = await connection.QueryAsync<BP24Model>(sql);
                Console.WriteLine($"Query executed successfully. Found {records?.Count() ?? 0} records");

                return Json(new { success = true, data = records });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"!!! ERROR in GetAllBP24Records !!!");
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
        [HttpGet("api/bp24/get/{id}")]
        public async Task<IActionResult> GetBP24ById(int id)
        {
            try
            {
                using var connection = new SqlConnection(_connectionString);
                var sql = "SELECT * FROM [Quartz].[dbo].[BP24] WHERE Id = @Id";
                var record = await connection.QueryFirstOrDefaultAsync<BP24Model>(sql, new { Id = id });

                if (record == null)
                    return Json(new { success = false, message = "Record not found" });

                return Json(new { success = true, data = record });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in GetBP24ById: {ex.Message}");
                return Json(new { success = false, message = ex.Message });
            }
        }

        [AllowAnonymous]
        [HttpPost("api/bp24/create")]
        public async Task<IActionResult> CreateBP24([FromBody] BP24Model model)
        {
            try
            {
                Console.WriteLine("Creating new BP24 record...");
                using var connection = new SqlConnection(_connectionString);
                var sql = @"INSERT INTO [Quartz].[dbo].[BP24] 
                    ([Grouping], [Refferal], [FileNo], [CompanyName], [YearEnd],
                     [ServicesType], [ActiveCoActivitSize], [YEtodo], [DateDocIn], 
                     [MthTodo], [Staff], [AllocateToWkSch], [Completed])
                    VALUES 
                    (@Grouping, @Refferal, @FileNo, @CompanyName, @YearEnd,
                     @ServicesType, @ActiveCoActivitSize, @YEtodo, @DateDocIn, 
                     @MthTodo, @Staff, @AllocateToWkSch, @Completed);
                    SELECT CAST(SCOPE_IDENTITY() as int);";

                var id = await connection.ExecuteScalarAsync<int>(sql, model);
                Console.WriteLine($"Record created successfully with ID: {id}");
                return Json(new { success = true, id = id, message = "Record created successfully" });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in CreateBP24: {ex.Message}");
                return Json(new { success = false, message = ex.Message });
            }
        }

        [AllowAnonymous]
        [HttpPut("api/bp24/update")]
        public async Task<IActionResult> UpdateBP24([FromBody] BP24Model model)
        {
            try
            {
                using var connection = new SqlConnection(_connectionString);
                var sql = @"UPDATE [Quartz].[dbo].[BP24] SET 
                    [Grouping] = @Grouping, [Refferal] = @Refferal, [FileNo] = @FileNo,
                    [CompanyName] = @CompanyName, [YearEnd] = @YearEnd,
                    [ServicesType] = @ServicesType, [ActiveCoActivitSize] = @ActiveCoActivitSize,
                    [YEtodo] = @YEtodo, [DateDocIn] = @DateDocIn, [MthTodo] = @MthTodo,
                    [Staff] = @Staff, [AllocateToWkSch] = @AllocateToWkSch, [Completed] = @Completed
                    WHERE Id = @Id";

                var affectedRows = await connection.ExecuteAsync(sql, model);
                if (affectedRows == 0)
                    return Json(new { success = false, message = "Record not found" });

                return Json(new { success = true, message = "Record updated successfully" });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in UpdateBP24: {ex.Message}");
                return Json(new { success = false, message = ex.Message });
            }
        }

        [AllowAnonymous]
        [HttpDelete("api/bp24/delete/{id}")]
        public async Task<IActionResult> DeleteBP24(int id)
        {
            try
            {
                using var connection = new SqlConnection(_connectionString);
                var sql = "DELETE FROM [Quartz].[dbo].[BP24] WHERE Id = @Id";
                var affectedRows = await connection.ExecuteAsync(sql, new { Id = id });

                if (affectedRows == 0)
                    return Json(new { success = false, message = "Record not found" });

                return Json(new { success = true, message = "Record deleted successfully" });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in DeleteBP24: {ex.Message}");
                return Json(new { success = false, message = ex.Message });
            }
        }
        #endregion

        #region BP25 (Individual Form BE Master Schedule) CRUD Methods
        [AllowAnonymous]
        [HttpGet("api/bp25/get-all")]
        public async Task<IActionResult> GetAllBP25Records()
        {
            try
            {
                Console.WriteLine("=== Starting GetAllBP25Records ===");
                using var connection = new SqlConnection(_connectionString);

                await connection.OpenAsync();
                Console.WriteLine("Database connection successful");

                var sql = "SELECT * FROM [Quartz].[dbo].[BP25] ORDER BY Id DESC";
                Console.WriteLine($"Executing SQL: {sql}");

                var records = await connection.QueryAsync<BP25Model>(sql);
                Console.WriteLine($"Query executed successfully. Found {records?.Count() ?? 0} records");

                return Json(new { success = true, data = records });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"!!! ERROR in GetAllBP25Records !!!");
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
        [HttpGet("api/bp25/get/{id}")]
        public async Task<IActionResult> GetBP25ById(int id)
        {
            try
            {
                using var connection = new SqlConnection(_connectionString);
                var sql = "SELECT * FROM [Quartz].[dbo].[BP25] WHERE Id = @Id";
                var record = await connection.QueryFirstOrDefaultAsync<BP25Model>(sql, new { Id = id });

                if (record == null)
                    return Json(new { success = false, message = "Record not found" });

                return Json(new { success = true, data = record });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in GetBP25ById: {ex.Message}");
                return Json(new { success = false, message = ex.Message });
            }
        }

        [AllowAnonymous]
        [HttpPost("api/bp25/create")]
        public async Task<IActionResult> CreateBP25([FromBody] BP25Model model)
        {
            try
            {
                Console.WriteLine("Creating new BP25 record...");
                using var connection = new SqlConnection(_connectionString);
                var sql = @"INSERT INTO [Quartz].[dbo].[BP25] 
                    ([Grouping], [Refferal], [FileNo], [CompanyName], [YearEnd],
                     [Enumber], [TinNumber], [Login], [Password], [Code],
                     [Description], [JobServices], [YEtodo], [DateDocIn], 
                     [MthTodo], [Staff], [AllocateToWkSch], [Completed])
                    VALUES 
                    (@Grouping, @Refferal, @FileNo, @CompanyName, @YearEnd,
                     @Enumber, @TinNumber, @Login, @Password, @Code,
                     @Description, @JobServices, @YEtodo, @DateDocIn, 
                     @MthTodo, @Staff, @AllocateToWkSch, @Completed);
                    SELECT CAST(SCOPE_IDENTITY() as int);";

                var id = await connection.ExecuteScalarAsync<int>(sql, model);
                Console.WriteLine($"Record created successfully with ID: {id}");
                return Json(new { success = true, id = id, message = "Record created successfully" });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in CreateBP25: {ex.Message}");
                return Json(new { success = false, message = ex.Message });
            }
        }

        [AllowAnonymous]
        [HttpPut("api/bp25/update")]
        public async Task<IActionResult> UpdateBP25([FromBody] BP25Model model)
        {
            try
            {
                using var connection = new SqlConnection(_connectionString);
                var sql = @"UPDATE [Quartz].[dbo].[BP25] SET 
                    [Grouping] = @Grouping, [Refferal] = @Refferal, [FileNo] = @FileNo,
                    [CompanyName] = @CompanyName, [YearEnd] = @YearEnd,
                    [Enumber] = @Enumber, [TinNumber] = @TinNumber, [Login] = @Login,
                    [Password] = @Password, [Code] = @Code, [Description] = @Description,
                    [JobServices] = @JobServices, [YEtodo] = @YEtodo, [DateDocIn] = @DateDocIn,
                    [MthTodo] = @MthTodo, [Staff] = @Staff, [AllocateToWkSch] = @AllocateToWkSch,
                    [Completed] = @Completed
                    WHERE Id = @Id";

                var affectedRows = await connection.ExecuteAsync(sql, model);
                if (affectedRows == 0)
                    return Json(new { success = false, message = "Record not found" });

                return Json(new { success = true, message = "Record updated successfully" });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in UpdateBP25: {ex.Message}");
                return Json(new { success = false, message = ex.Message });
            }
        }

        [AllowAnonymous]
        [HttpDelete("api/bp25/delete/{id}")]
        public async Task<IActionResult> DeleteBP25(int id)
        {
            try
            {
                using var connection = new SqlConnection(_connectionString);
                var sql = "DELETE FROM [Quartz].[dbo].[BP25] WHERE Id = @Id";
                var affectedRows = await connection.ExecuteAsync(sql, new { Id = id });

                if (affectedRows == 0)
                    return Json(new { success = false, message = "Record not found" });

                return Json(new { success = true, message = "Record deleted successfully" });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in DeleteBP25: {ex.Message}");
                return Json(new { success = false, message = ex.Message });
            }
        }
        #endregion

        #region BP26 (Form E Master Schedule) CRUD Methods
        [AllowAnonymous]
        [HttpGet("api/bp26/get-all")]
        public async Task<IActionResult> GetAllBP26Records()
        {
            try
            {
                Console.WriteLine("=== Starting GetAllBP26Records ===");
                using var connection = new SqlConnection(_connectionString);

                await connection.OpenAsync();
                Console.WriteLine("Database connection successful");

                var sql = "SELECT * FROM [Quartz].[dbo].[BP26] ORDER BY Id DESC";
                Console.WriteLine($"Executing SQL: {sql}");

                var records = await connection.QueryAsync<BP26Model>(sql);
                Console.WriteLine($"Query executed successfully. Found {records?.Count() ?? 0} records");

                return Json(new { success = true, data = records });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"!!! ERROR in GetAllBP26Records !!!");
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
        [HttpGet("api/bp26/get/{id}")]
        public async Task<IActionResult> GetBP26ById(int id)
        {
            try
            {
                using var connection = new SqlConnection(_connectionString);
                var sql = "SELECT * FROM [Quartz].[dbo].[BP26] WHERE Id = @Id";
                var record = await connection.QueryFirstOrDefaultAsync<BP26Model>(sql, new { Id = id });

                if (record == null)
                    return Json(new { success = false, message = "Record not found" });

                return Json(new { success = true, data = record });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in GetBP26ById: {ex.Message}");
                return Json(new { success = false, message = ex.Message });
            }
        }

        [AllowAnonymous]
        [HttpPost("api/bp26/create")]
        public async Task<IActionResult> CreateBP26([FromBody] BP26Model model)
        {
            try
            {
                Console.WriteLine("Creating new BP26 record...");
                using var connection = new SqlConnection(_connectionString);
                var sql = @"INSERT INTO [Quartz].[dbo].[BP26] 
                    ([Grouping], [Refferal], [FileNo], [CompanyName], [YearEnd],
                     [Enumber], [TinNumber], [Login], [Password], [Code],
                     [Description], [JobServices], [YEtodo], [DateDocIn], 
                     [MthTodo], [Staff], [AllocateToWkSch], [Completed])
                    VALUES 
                    (@Grouping, @Refferal, @FileNo, @CompanyName, @YearEnd,
                     @Enumber, @TinNumber, @Login, @Password, @Code,
                     @Description, @JobServices, @YEtodo, @DateDocIn, 
                     @MthTodo, @Staff, @AllocateToWkSch, @Completed);
                    SELECT CAST(SCOPE_IDENTITY() as int);";

                var id = await connection.ExecuteScalarAsync<int>(sql, model);
                Console.WriteLine($"Record created successfully with ID: {id}");
                return Json(new { success = true, id = id, message = "Record created successfully" });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in CreateBP26: {ex.Message}");
                return Json(new { success = false, message = ex.Message });
            }
        }

        [AllowAnonymous]
        [HttpPut("api/bp26/update")]
        public async Task<IActionResult> UpdateBP26([FromBody] BP26Model model)
        {
            try
            {
                using var connection = new SqlConnection(_connectionString);
                var sql = @"UPDATE [Quartz].[dbo].[BP26] SET 
                    [Grouping] = @Grouping, [Refferal] = @Refferal, [FileNo] = @FileNo,
                    [CompanyName] = @CompanyName, [YearEnd] = @YearEnd,
                    [Enumber] = @Enumber, [TinNumber] = @TinNumber, [Login] = @Login,
                    [Password] = @Password, [Code] = @Code, [Description] = @Description,
                    [JobServices] = @JobServices, [YEtodo] = @YEtodo, [DateDocIn] = @DateDocIn,
                    [MthTodo] = @MthTodo, [Staff] = @Staff, [AllocateToWkSch] = @AllocateToWkSch,
                    [Completed] = @Completed
                    WHERE Id = @Id";

                var affectedRows = await connection.ExecuteAsync(sql, model);
                if (affectedRows == 0)
                    return Json(new { success = false, message = "Record not found" });

                return Json(new { success = true, message = "Record updated successfully" });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in UpdateBP26: {ex.Message}");
                return Json(new { success = false, message = ex.Message });
            }
        }

        [AllowAnonymous]
        [HttpDelete("api/bp26/delete/{id}")]
        public async Task<IActionResult> DeleteBP26(int id)
        {
            try
            {
                using var connection = new SqlConnection(_connectionString);
                var sql = "DELETE FROM [Quartz].[dbo].[BP26] WHERE Id = @Id";
                var affectedRows = await connection.ExecuteAsync(sql, new { Id = id });

                if (affectedRows == 0)
                    return Json(new { success = false, message = "Record not found" });

                return Json(new { success = true, message = "Record deleted successfully" });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in DeleteBP26: {ex.Message}");
                return Json(new { success = false, message = ex.Message });
            }
        }
        #endregion

        #region BP31 (Sdn Bhd Backlog) CRUD Methods
        [AllowAnonymous]
        [HttpGet("api/bp31/get-all")]
        public async Task<IActionResult> GetAllBP31Records()
        {
            try
            {
                Console.WriteLine("=== Starting GetAllBP31Records ===");
                using var connection = new SqlConnection(_connectionString);

                await connection.OpenAsync();
                Console.WriteLine("Database connection successful");

                var sql = "SELECT * FROM [Quartz].[dbo].[BP31] ORDER BY Id DESC";
                Console.WriteLine($"Executing SQL: {sql}");

                var records = await connection.QueryAsync<BP31Model>(sql);
                Console.WriteLine($"Query executed successfully. Found {records?.Count() ?? 0} records");

                return Json(new { success = true, data = records });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"!!! ERROR in GetAllBP31Records !!!");
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
        [HttpGet("api/bp31/get/{id}")]
        public async Task<IActionResult> GetBP31ById(int id)
        {
            try
            {
                using var connection = new SqlConnection(_connectionString);
                var sql = "SELECT * FROM [Quartz].[dbo].[BP31] WHERE Id = @Id";
                var record = await connection.QueryFirstOrDefaultAsync<BP31Model>(sql, new { Id = id });

                if (record == null)
                    return Json(new { success = false, message = "Record not found" });

                return Json(new { success = true, data = record });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in GetBP31ById: {ex.Message}");
                return Json(new { success = false, message = ex.Message });
            }
        }

        [AllowAnonymous]
        [HttpPost("api/bp31/create")]
        public async Task<IActionResult> CreateBP31([FromBody] BP31Model model)
        {
            try
            {
                Console.WriteLine("Creating new BP31 record...");
                using var connection = new SqlConnection(_connectionString);
                var sql = @"INSERT INTO [Quartz].[dbo].[BP31] 
                    ([Grouping], [Referral], [FileNo], [CompanyName], [YearEnd],
                     [IncorpDate], [CO], [Enumber], [TINnumber], [Code],
                     [Description], [ServicesType], [CoStatus], [ActiveCoActivitySize],
                     [YEtoDo], [DateDocIn], [EstMthTodo], [Staff], [AllocateToWkSch])
                    VALUES 
                    (@Grouping, @Referral, @FileNo, @CompanyName, @YearEnd,
                     @IncorpDate, @CO, @Enumber, @TINnumber, @Code,
                     @Description, @ServicesType, @CoStatus, @ActiveCoActivitySize,
                     @YEtoDo, @DateDocIn, @EstMthTodo, @Staff, @AllocateToWkSch);
                    SELECT CAST(SCOPE_IDENTITY() as int);";

                var id = await connection.ExecuteScalarAsync<int>(sql, model);
                Console.WriteLine($"Record created successfully with ID: {id}");
                return Json(new { success = true, id = id, message = "Record created successfully" });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in CreateBP31: {ex.Message}");
                return Json(new { success = false, message = ex.Message });
            }
        }

        [AllowAnonymous]
        [HttpPut("api/bp31/update")]
        public async Task<IActionResult> UpdateBP31([FromBody] BP31Model model)
        {
            try
            {
                using var connection = new SqlConnection(_connectionString);
                var sql = @"UPDATE [Quartz].[dbo].[BP31] SET 
                    [Grouping] = @Grouping, [Referral] = @Referral, [FileNo] = @FileNo,
                    [CompanyName] = @CompanyName, [YearEnd] = @YearEnd,
                    [IncorpDate] = @IncorpDate, [CO] = @CO, [Enumber] = @Enumber,
                    [TINnumber] = @TINnumber, [Code] = @Code, [Description] = @Description,
                    [ServicesType] = @ServicesType, [CoStatus] = @CoStatus,
                    [ActiveCoActivitySize] = @ActiveCoActivitySize, [YEtoDo] = @YEtoDo,
                    [DateDocIn] = @DateDocIn, [EstMthTodo] = @EstMthTodo, [Staff] = @Staff,
                    [AllocateToWkSch] = @AllocateToWkSch
                    WHERE Id = @Id";

                var affectedRows = await connection.ExecuteAsync(sql, model);
                if (affectedRows == 0)
                    return Json(new { success = false, message = "Record not found" });

                return Json(new { success = true, message = "Record updated successfully" });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in UpdateBP31: {ex.Message}");
                return Json(new { success = false, message = ex.Message });
            }
        }

        [AllowAnonymous]
        [HttpDelete("api/bp31/delete/{id}")]
        public async Task<IActionResult> DeleteBP31(int id)
        {
            try
            {
                using var connection = new SqlConnection(_connectionString);
                var sql = "DELETE FROM [Quartz].[dbo].[BP31] WHERE Id = @Id";
                var affectedRows = await connection.ExecuteAsync(sql, new { Id = id });

                if (affectedRows == 0)
                    return Json(new { success = false, message = "Record not found" });

                return Json(new { success = true, message = "Record deleted successfully" });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in DeleteBP31: {ex.Message}");
                return Json(new { success = false, message = ex.Message });
            }
        }
        #endregion

        #region BP32 (Tax Compliance Management) CRUD Methods
        [AllowAnonymous]
        [HttpGet("api/bp32/get-all")]
        public async Task<IActionResult> GetAllBP32Records()
        {
            try
            {
                Console.WriteLine("=== Starting GetAllBP32Records ===");
                using var connection = new SqlConnection(_connectionString);

                await connection.OpenAsync();
                Console.WriteLine("Database connection successful");

                var sql = "SELECT * FROM [Quartz].[dbo].[BP32] ORDER BY Id DESC";
                Console.WriteLine($"Executing SQL: {sql}");

                var records = await connection.QueryAsync<BP32Model>(sql);
                Console.WriteLine($"Query executed successfully. Found {records?.Count() ?? 0} records");

                return Json(new { success = true, data = records });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"!!! ERROR in GetAllBP32Records !!!");
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
        [HttpGet("api/bp32/get/{id}")]
        public async Task<IActionResult> GetBP32ById(int id)
        {
            try
            {
                using var connection = new SqlConnection(_connectionString);
                var sql = "SELECT * FROM [Quartz].[dbo].[BP32] WHERE Id = @Id";
                var record = await connection.QueryFirstOrDefaultAsync<BP32Model>(sql, new { Id = id });

                if (record == null)
                    return Json(new { success = false, message = "Record not found" });

                return Json(new { success = true, data = record });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in GetBP32ById: {ex.Message}");
                return Json(new { success = false, message = ex.Message });
            }
        }

        [AllowAnonymous]
        [HttpPost("api/bp32/create")]
        public async Task<IActionResult> CreateBP32([FromBody] BP32Model model)
        {
            try
            {
                Console.WriteLine("Creating new BP32 record...");
                using var connection = new SqlConnection(_connectionString);
                var sql = @"INSERT INTO [Quartz].[dbo].[BP32] 
            ([FileNo], [CompanyName], [YearEnd], [JobServices], [CoStatus],
             [ActiveCoActivitySize], [YEtodo], [MthTodo], [DocReceivedDate], [TaxARdueDate],
             [Staff], [StartDate], [EndDate], [TimeTaken], [DatePassToAudit], [DateTaxSubmited],
             [CompletedBacklog], [SingleEntry], [TaxComputation], [SortingFiling], [KeyinToExcel],
             [ReviewWorkingAcc], [DraftFinancialStatement], [DraftTaxCompleted], [ReviewTax],
             [FinalTax], [TaxComFinalSignByClient], [AmountTaxPay], [EFileDraft], [EFileFinal],
             [EFileviaSPC], [InvoiceNo], [AmountRM], [DocDespatchDate])
            VALUES 
            (@FileNo, @CompanyName, @YearEnd, @JobServices, @CoStatus,
             @ActiveCoActivitySize, @YEtodo, @MthTodo, @DocReceivedDate, @TaxARdueDate,
             @Staff, @StartDate, @EndDate, @TimeTaken, @DatePassToAudit, @DateTaxSubmited,
             @CompletedBacklog, @SingleEntry, @TaxComputation, @SortingFiling, @KeyinToExcel,
             @ReviewWorkingAcc, @DraftFinancialStatement, @DraftTaxCompleted, @ReviewTax,
             @FinalTax, @TaxComFinalSignByClient, @AmountTaxPay, @EFileDraft, @EFileFinal,
             @EFileviaSPC, @InvoiceNo, @AmountRM, @DocDespatchDate);
            SELECT CAST(SCOPE_IDENTITY() as int);";

                var id = await connection.ExecuteScalarAsync<int>(sql, model);
                Console.WriteLine($"Record created successfully with ID: {id}");
                return Json(new { success = true, id = id, message = "Record created successfully" });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in CreateBP32: {ex.Message}");
                return Json(new { success = false, message = ex.Message });
            }
        }

        [AllowAnonymous]
        [HttpPut("api/bp32/update")]
        public async Task<IActionResult> UpdateBP32([FromBody] BP32Model model)
        {
            try
            {
                using var connection = new SqlConnection(_connectionString);
                var sql = @"UPDATE [Quartz].[dbo].[BP32] SET 
            [FileNo] = @FileNo, [CompanyName] = @CompanyName, [YearEnd] = @YearEnd,
            [JobServices] = @JobServices, [CoStatus] = @CoStatus,
            [ActiveCoActivitySize] = @ActiveCoActivitySize, [YEtodo] = @YEtodo,
            [MthTodo] = @MthTodo, [DocReceivedDate] = @DocReceivedDate, [TaxARdueDate] = @TaxARdueDate,
            [Staff] = @Staff, [StartDate] = @StartDate, [EndDate] = @EndDate, [TimeTaken] = @TimeTaken,
            [DatePassToAudit] = @DatePassToAudit, [DateTaxSubmited] = @DateTaxSubmited,
            [CompletedBacklog] = @CompletedBacklog, [SingleEntry] = @SingleEntry, [TaxComputation] = @TaxComputation,
            [SortingFiling] = @SortingFiling, [KeyinToExcel] = @KeyinToExcel, [ReviewWorkingAcc] = @ReviewWorkingAcc,
            [DraftFinancialStatement] = @DraftFinancialStatement, [DraftTaxCompleted] = @DraftTaxCompleted,
            [ReviewTax] = @ReviewTax, [FinalTax] = @FinalTax, [TaxComFinalSignByClient] = @TaxComFinalSignByClient,
            [AmountTaxPay] = @AmountTaxPay, [EFileDraft] = @EFileDraft, [EFileFinal] = @EFileFinal,
            [EFileviaSPC] = @EFileviaSPC, [InvoiceNo] = @InvoiceNo, [AmountRM] = @AmountRM,
            [DocDespatchDate] = @DocDespatchDate
            WHERE Id = @Id";

                var affectedRows = await connection.ExecuteAsync(sql, model);
                if (affectedRows == 0)
                    return Json(new { success = false, message = "Record not found" });

                return Json(new { success = true, message = "Record updated successfully" });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in UpdateBP32: {ex.Message}");
                return Json(new { success = false, message = ex.Message });
            }
        }

        [AllowAnonymous]
        [HttpDelete("api/bp32/delete/{id}")]
        public async Task<IActionResult> DeleteBP32(int id)
        {
            try
            {
                using var connection = new SqlConnection(_connectionString);
                var sql = "DELETE FROM [Quartz].[dbo].[BP32] WHERE Id = @Id";
                var affectedRows = await connection.ExecuteAsync(sql, new { Id = id });

                if (affectedRows == 0)
                    return Json(new { success = false, message = "Record not found" });

                return Json(new { success = true, message = "Record deleted successfully" });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in DeleteBP32: {ex.Message}");
                return Json(new { success = false, message = ex.Message });
            }
        }
        #endregion

        #region BP33 CRUD Methods
        [AllowAnonymous]
        [HttpGet("api/bp33/get-all")]
        public async Task<IActionResult> GetAllBP33Records()
        {
            try
            {
                Console.WriteLine("=== Starting GetAllBP33Records ===");
                using var connection = new SqlConnection(_connectionString);

                await connection.OpenAsync();
                Console.WriteLine("Database connection successful");

                var sql = "SELECT * FROM [Quartz].[dbo].[BP33] ORDER BY Id DESC";
                Console.WriteLine($"Executing SQL: {sql}");

                var records = await connection.QueryAsync<BP33Model>(sql);
                Console.WriteLine($"Query executed successfully. Found {records?.Count() ?? 0} records");

                return Json(new { success = true, data = records });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"!!! ERROR in GetAllBP33Records !!!");
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
        [HttpGet("api/bp33/get/{id}")]
        public async Task<IActionResult> GetBP33ById(int id)
        {
            try
            {
                using var connection = new SqlConnection(_connectionString);
                var sql = "SELECT * FROM [Quartz].[dbo].[BP33] WHERE Id = @Id";
                var record = await connection.QueryFirstOrDefaultAsync<BP33Model>(sql, new { Id = id });

                if (record == null)
                    return Json(new { success = false, message = "Record not found" });

                return Json(new { success = true, data = record });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in GetBP33ById: {ex.Message}");
                return Json(new { success = false, message = ex.Message });
            }
        }

        [AllowAnonymous]
        [HttpPost("api/bp33/create")]
        public async Task<IActionResult> CreateBP33([FromBody] BP33Model model)
        {
            try
            {
                Console.WriteLine("Creating new BP33 record...");
                using var connection = new SqlConnection(_connectionString);
                var sql = @"INSERT INTO [Quartz].[dbo].[BP33] 
                    ([Item], [Grouping], [CompanyName], [DraftTaxCompleted], [ReviewTax],
                     [FinalTax], [TaxComFinalSignByClient], [AmountofTaxPay], [EFileDraft],
                     [EFileFinal], [TaxReferennceNo], [Login], [Password], [TypeofForm],
                     [SPC], [InvoicesNo], [DocDespatchDate])
                    VALUES 
                    (@Item, @Grouping, @CompanyName, @DraftTaxCompleted, @ReviewTax,
                     @FinalTax, @TaxComFinalSignByClient, @AmountofTaxPay, @EFileDraft,
                     @EFileFinal, @TaxReferennceNo, @Login, @Password, @TypeofForm,
                     @SPC, @InvoicesNo, @DocDespatchDate);
                    SELECT CAST(SCOPE_IDENTITY() as int);";

                var id = await connection.ExecuteScalarAsync<int>(sql, model);
                Console.WriteLine($"Record created successfully with ID: {id}");
                return Json(new { success = true, id = id, message = "Record created successfully" });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in CreateBP33: {ex.Message}");
                return Json(new { success = false, message = ex.Message });
            }
        }

        [AllowAnonymous]
        [HttpPut("api/bp33/update")]
        public async Task<IActionResult> UpdateBP33([FromBody] BP33Model model)
        {
            try
            {
                using var connection = new SqlConnection(_connectionString);
                var sql = @"UPDATE [Quartz].[dbo].[BP33] SET 
                    [Item] = @Item, [Grouping] = @Grouping, [CompanyName] = @CompanyName,
                    [DraftTaxCompleted] = @DraftTaxCompleted, [ReviewTax] = @ReviewTax,
                    [FinalTax] = @FinalTax, [TaxComFinalSignByClient] = @TaxComFinalSignByClient,
                    [AmountofTaxPay] = @AmountofTaxPay, [EFileDraft] = @EFileDraft,
                    [EFileFinal] = @EFileFinal, [TaxReferennceNo] = @TaxReferennceNo,
                    [Login] = @Login, [Password] = @Password, [TypeofForm] = @TypeofForm,
                    [SPC] = @SPC, [InvoicesNo] = @InvoicesNo, [DocDespatchDate] = @DocDespatchDate
                    WHERE Id = @Id";

                var affectedRows = await connection.ExecuteAsync(sql, model);
                if (affectedRows == 0)
                    return Json(new { success = false, message = "Record not found" });

                return Json(new { success = true, message = "Record updated successfully" });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in UpdateBP33: {ex.Message}");
                return Json(new { success = false, message = ex.Message });
            }
        }

        [AllowAnonymous]
        [HttpDelete("api/bp33/delete/{id}")]
        public async Task<IActionResult> DeleteBP33(int id)
        {
            try
            {
                using var connection = new SqlConnection(_connectionString);
                var sql = "DELETE FROM [Quartz].[dbo].[BP33] WHERE Id = @Id";
                var affectedRows = await connection.ExecuteAsync(sql, new { Id = id });

                if (affectedRows == 0)
                    return Json(new { success = false, message = "Record not found" });

                return Json(new { success = true, message = "Record deleted successfully" });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in DeleteBP33: {ex.Message}");
                return Json(new { success = false, message = ex.Message });
            }
        }
        #endregion

        #region BP34 CRUD Methods
        [AllowAnonymous]
        [HttpGet("api/bp34/get-all")]
        public async Task<IActionResult> GetAllBP34Records()
        {
            try
            {
                Console.WriteLine("=== Starting GetAllBP34Records ===");
                using var connection = new SqlConnection(_connectionString);

                await connection.OpenAsync();
                Console.WriteLine("Database connection successful");

                var sql = "SELECT * FROM [Quartz].[dbo].[BP34] ORDER BY Id DESC";
                Console.WriteLine($"Executing SQL: {sql}");

                var records = await connection.QueryAsync<BP34Model>(sql);
                Console.WriteLine($"Query executed successfully. Found {records?.Count() ?? 0} records");

                return Json(new { success = true, data = records });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"!!! ERROR in GetAllBP34Records !!!");
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
        [HttpGet("api/bp34/get/{id}")]
        public async Task<IActionResult> GetBP34ById(int id)
        {
            try
            {
                using var connection = new SqlConnection(_connectionString);
                var sql = "SELECT * FROM [Quartz].[dbo].[BP34] WHERE Id = @Id";
                var record = await connection.QueryFirstOrDefaultAsync<BP34Model>(sql, new { Id = id });

                if (record == null)
                    return Json(new { success = false, message = "Record not found" });

                return Json(new { success = true, data = record });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in GetBP34ById: {ex.Message}");
                return Json(new { success = false, message = ex.Message });
            }
        }

        [AllowAnonymous]
        [HttpPost("api/bp34/create")]
        public async Task<IActionResult> CreateBP34([FromBody] BP34Model model)
        {
            try
            {
                Console.WriteLine("Creating new BP34 record...");
                using var connection = new SqlConnection(_connectionString);
                var sql = @"INSERT INTO [Quartz].[dbo].[BP34] 
                    ([Item], [Grouping], [CompanyName], [DraftTaxCompleted], [ReviewTax],
                     [FinalTax], [TaxComFinalSignbyClient], [AmountTaxPay], [EFileDraft],
                     [EFileFinal], [TaxRefferanceNo], [Login], [Password], [TypeofForm],
                     [SPC], [InvoiceNo], [DocDespatchDate])
                    VALUES 
                    (@Item, @Grouping, @CompanyName, @DraftTaxCompleted, @ReviewTax,
                     @FinalTax, @TaxComFinalSignbyClient, @AmountTaxPay, @EFileDraft,
                     @EFileFinal, @TaxRefferanceNo, @Login, @Password, @TypeofForm,
                     @SPC, @InvoiceNo, @DocDespatchDate);
                    SELECT CAST(SCOPE_IDENTITY() as int);";

                var id = await connection.ExecuteScalarAsync<int>(sql, model);
                Console.WriteLine($"Record created successfully with ID: {id}");
                return Json(new { success = true, id = id, message = "Record created successfully" });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in CreateBP34: {ex.Message}");
                return Json(new { success = false, message = ex.Message });
            }
        }

        [AllowAnonymous]
        [HttpPut("api/bp34/update")]
        public async Task<IActionResult> UpdateBP34([FromBody] BP34Model model)
        {
            try
            {
                using var connection = new SqlConnection(_connectionString);
                var sql = @"UPDATE [Quartz].[dbo].[BP34] SET 
                    [Item] = @Item, [Grouping] = @Grouping, [CompanyName] = @CompanyName,
                    [DraftTaxCompleted] = @DraftTaxCompleted, [ReviewTax] = @ReviewTax,
                    [FinalTax] = @FinalTax, [TaxComFinalSignbyClient] = @TaxComFinalSignbyClient,
                    [AmountTaxPay] = @AmountTaxPay, [EFileDraft] = @EFileDraft,
                    [EFileFinal] = @EFileFinal, [TaxRefferanceNo] = @TaxRefferanceNo,
                    [Login] = @Login, [Password] = @Password, [TypeofForm] = @TypeofForm,
                    [SPC] = @SPC, [InvoiceNo] = @InvoiceNo, [DocDespatchDate] = @DocDespatchDate
                    WHERE Id = @Id";

                var affectedRows = await connection.ExecuteAsync(sql, model);
                if (affectedRows == 0)
                    return Json(new { success = false, message = "Record not found" });

                return Json(new { success = true, message = "Record updated successfully" });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in UpdateBP34: {ex.Message}");
                return Json(new { success = false, message = ex.Message });
            }
        }

        [AllowAnonymous]
        [HttpDelete("api/bp34/delete/{id}")]
        public async Task<IActionResult> DeleteBP34(int id)
        {
            try
            {
                using var connection = new SqlConnection(_connectionString);
                var sql = "DELETE FROM [Quartz].[dbo].[BP34] WHERE Id = @Id";
                var affectedRows = await connection.ExecuteAsync(sql, new { Id = id });

                if (affectedRows == 0)
                    return Json(new { success = false, message = "Record not found" });

                return Json(new { success = true, message = "Record deleted successfully" });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in DeleteBP34: {ex.Message}");
                return Json(new { success = false, message = ex.Message });
            }
        }
        #endregion

    }
}