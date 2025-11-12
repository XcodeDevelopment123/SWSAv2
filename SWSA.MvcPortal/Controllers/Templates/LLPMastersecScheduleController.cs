using Microsoft.AspNetCore.Mvc;
using Dapper;
using Microsoft.Data.SqlClient;
using SWSA.MvcPortal.Models.SecDeptModel;
using System.Data;

namespace SWSA.MvcPortal.Controllers.Templates
{
    public class LLPMastersecScheduleController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;

        public LLPMastersecScheduleController(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("SwsaConntection");
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
                    var sql = @"SELECT [Id], [Grouping], [Referral], [SecFileNo], 
                                      [CompanyName], [YearEnd], [IncorpDate], 
                                      [CompanyStatus], [ActiveCoActivitySize], 
                                      [YEtodo], [ACCmthTodo], [SSMextensionDate], 
                                      [ADdueDate], [AccReady], [ADsubmitDate], 
                                      [JobCompleted]
                               FROM [dbo].[S13B]
                               ORDER BY [Id] DESC";

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
                    var sql = @"SELECT [Id], [Grouping], [Referral], [SecFileNo], 
                                      [CompanyName], [YearEnd], [IncorpDate], 
                                      [CompanyStatus], [ActiveCoActivitySize], 
                                      [YEtodo], [ACCmthTodo], [SSMextensionDate], 
                                      [ADdueDate], [AccReady], [ADsubmitDate], 
                                      [JobCompleted]
                               FROM [dbo].[S13B]
                               WHERE [Id] = @Id";

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
                                [ActiveCoActivitySize], [YEtodo], [ACCmthTodo], 
                                [SSMextensionDate], [ADdueDate], [AccReady], 
                                [ADsubmitDate], [JobCompleted])
                               VALUES 
                               (@Grouping, @Referral, @SecFileNo, @CompanyName, 
                                @YearEnd, @IncorpDate, @CompanyStatus, 
                                @ActiveCoActivitySize, @YEtodo, @ACCmthTodo, 
                                @SSMextensionDate, @ADdueDate, @AccReady, 
                                @ADsubmitDate, @JobCompleted);
                               SELECT CAST(SCOPE_IDENTITY() as int)";

                    var id = await connection.ExecuteScalarAsync<int>(sql, model);

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

                    return Json(new { success = true, message = "Record updated successfully" });
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
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
    }
}