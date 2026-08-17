using Microsoft.AspNetCore.Mvc;
using Dapper;
using Microsoft.Data.SqlClient;
using SWSA.MvcPortal.Models.SecDeptModel;
using System.Data;
using SWSA.MvcPortal.Services.Clients;
using SWSA.MvcPortal.Services.Interfaces.Clients;

namespace SWSA.MvcPortal.Controllers.Templates
{
    public class LLPMastersecScheduleController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;
        private readonly IClientService _clientService;

        public LLPMastersecScheduleController(IConfiguration configuration, IClientService clientService)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("SwsaConntection");
            _clientService = clientService;
        }

        // GET: 显示页面
        public IActionResult LLPMastersecSchedule()
        {
            return View();
        }

        // GET: 获取所有记录
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    var sql = @"SELECT s.[Id], s.[Grouping], s.[Referral], s.[SecFileNo], 
                                      s.[CompanyName], s.[YearEnd], s.[IncorpDate], 
                                      s.[CompanyStatus], s.[ActiveCoActivitySize],
                                      b.[CreditRating] AS [CreditRating],
                                      s.[YEtodo], s.[ACCmthTodo], s.[SSMextensionDate], 
                                      s.[ADdueDate], s.[AccReady], s.[ADsubmitDate], 
                                      s.[JobCompleted]
                               FROM [dbo].[S13B] s
                               LEFT JOIN [dbo].[Clients] c ON s.[CompanyName] = c.[Name]
                               LEFT JOIN [dbo].[BaseCompanies] b ON c.[Id] = b.[Id]
                               ORDER BY s.[Id] DESC";

                    var records = await connection.QueryAsync<S13BModel>(sql);
                    return Json(new { success = true, data = records });
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        // GET: 根据ID获取单条记录
        [HttpGet]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    var sql = @"SELECT s.[Id], s.[Grouping], s.[Referral], s.[SecFileNo], 
                                      s.[CompanyName], s.[YearEnd], s.[IncorpDate], 
                                      s.[CompanyStatus], s.[ActiveCoActivitySize],
                                      b.[CreditRating] AS [CreditRating],
                                      s.[YEtodo], s.[ACCmthTodo], s.[SSMextensionDate], 
                                      s.[ADdueDate], s.[AccReady], s.[ADsubmitDate], 
                                      s.[JobCompleted]
                               FROM [dbo].[S13B] s
                               LEFT JOIN [dbo].[Clients] c ON s.[CompanyName] = c.[Name]
                               LEFT JOIN [dbo].[BaseCompanies] b ON c.[Id] = b.[Id]
                               WHERE s.[Id] = @Id";

                    var record = await connection.QueryFirstOrDefaultAsync<S13BModel>(sql, new { Id = id });

                    if (record == null)
                    {
                        return Json(new { success = false, message = "Record not found" });
                    }

                    return Json(new { success = true, data = record });
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        // POST: 创建新记录
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] S13BModel model)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    var sql = @"INSERT INTO [dbo].[S13B] 
                                ([Grouping], [Referral], [SecFileNo], [CompanyName], 
                                 [YearEnd], [IncorpDate], [CompanyStatus], 
                                 [ActiveCoActivitySize], 
                                 [YEtodo], [ACCmthTodo], 
                                 [SSMextensionDate], [ADdueDate], [AccReady], 
                                 [ADsubmitDate], [JobCompleted])
                                VALUES 
                                (@Grouping, @Referral, @SecFileNo, @CompanyName, 
                                 @YearEnd, @IncorpDate, @CompanyStatus, 
                                 @ActiveCoActivitySize, 
                                 @YEtodo, @ACCmthTodo, 
                                 @SSMextensionDate, @ADdueDate, @AccReady, 
                                 @ADsubmitDate, @JobCompleted);
                                SELECT CAST(SCOPE_IDENTITY() as int)";

                    var id = await connection.ExecuteScalarAsync<int>(sql, model);

                    // Note 3: Auto-link SSMextensionDate to F1.2 (BP22.ExtensionDate)
                    await SyncExtensionDateToBP22(model);

                    return Json(new { success = true, message = "Record created successfully", id = id });
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        // PUT: 更新记录
        [HttpPost]
        public async Task<IActionResult> Update([FromBody] S13BModel model)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    var sql = @"UPDATE [dbo].[S13B] 
                                SET [Grouping] = @Grouping,
                                    [Referral] = @Referral,
                                    [SecFileNo] = @SecFileNo,
                                    [CompanyName] = @CompanyName,
                                    [YearEnd] = @YearEnd,
                                    [IncorpDate] = @IncorpDate,
                                    [CompanyStatus] = @CompanyStatus,
                                    [ActiveCoActivitySize] = @ActiveCoActivitySize,
                                    [YEtodo] = @YEtodo,
                                    [ACCmthTodo] = @ACCmthTodo,
                                    [SSMextensionDate] = @SSMextensionDate,
                                    [ADdueDate] = @ADdueDate,
                                    [AccReady] = @AccReady,
                                    [ADsubmitDate] = @ADsubmitDate,
                                    [JobCompleted] = @JobCompleted
                                WHERE [Id] = @Id";

                    var rowsAffected = await connection.ExecuteAsync(sql, model);

                    if (rowsAffected == 0)
                    {
                        return Json(new { success = false, message = "Record not found" });
                    }

                    // Note 3: Auto-link SSMextensionDate to F1.2 (BP22.ExtensionDate)
                    await SyncExtensionDateToBP22(model);

                    return Json(new { success = true, message = "Record updated successfully" });
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        private async Task SyncExtensionDateToBP22(S13BModel model)
        {
            if (string.IsNullOrWhiteSpace(model.CompanyName) || string.IsNullOrWhiteSpace(model.SSMextensionDate))
                return;

            try
            {
                using var connection = new SqlConnection(_connectionString);
                var checkSql = "SELECT TOP 1 Id FROM [Quartz2].[dbo].[BP22] WHERE CompanyName = @Client ORDER BY Id DESC";
                var bpId = await connection.QueryFirstOrDefaultAsync<int?>(checkSql, new { Client = model.CompanyName });

                if (bpId.HasValue && bpId.Value > 0)
                {
                    await connection.ExecuteAsync(
                        "UPDATE [Quartz2].[dbo].[BP22] SET ExtensionDate = @ExtensionDate WHERE Id = @Id",
                        new { ExtensionDate = model.SSMextensionDate, Id = bpId.Value }
                    );
                }
                else
                {
                    var insertSql = "INSERT INTO [Quartz2].[dbo].[BP22] ([CompanyName], [YearEnd], [ExtensionDate]) VALUES (@CompanyName, @YearEnd, @ExtensionDate)";
                    await connection.ExecuteAsync(insertSql, new { CompanyName = model.CompanyName, YearEnd = model.YearEnd, ExtensionDate = model.SSMextensionDate });
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[SyncExtensionDateToBP22 Error]: {ex.Message}");
            }
        }

        // DELETE: 删除记录
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    var sql = @"DELETE FROM [dbo].[S13B] WHERE [Id] = @Id";

                    var rowsAffected = await connection.ExecuteAsync(sql, new { Id = id });

                    if (rowsAffected == 0)
                    {
                        return Json(new { success = false, message = "Record not found" });
                    }

                    return Json(new { success = true, message = "Record deleted successfully" });
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        [HttpGet("api/get/llp-company-options")]
        public async Task<IActionResult> GetLlpCompanyOptions()
        {
            try
            {
                var list = await _clientService.GetLlpCompanyOptionsAsync();
                return Json(new { success = true, data = list });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }
    }
}