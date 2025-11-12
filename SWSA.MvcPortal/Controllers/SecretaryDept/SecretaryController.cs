using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Dapper;
using SWSA.MvcPortal.Entities.Templates;
using SWSA.MvcPortal.Persistence;
using SWSA.MvcPortal.Models.SecDeptModel;

namespace SWSA.MvcPortal.Controllers.SecretaryDept
{
    [Route("secretary-dept")]
    public class SecretaryController : BaseController
    {
        private readonly AppDbContext _db;
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;
        private readonly DbSet<SecDeptTaskTemplate> _tasks;

        public SecretaryController(AppDbContext db, IConfiguration configuration)
        {
            _db = db;
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("SwsaConntection");
            _tasks = _db.Set<SecDeptTaskTemplate>();
        }

        #region Page/View
        [Route("landing")]
        public async Task<IActionResult> Landing()
        {
            var data = await _tasks.Include(c => c.Client).ToListAsync();
            return View(data);
        }

        [Route("mastersec")]
        public async Task<IActionResult> SdnBhdMasterSecSchedule()
        {
            return View();
        }

        [Route("ARSubmisionReport")]
        public async Task<IActionResult> ARSubmisionReport()
        {
            return View();
        }

        [Route("AASR")]
        public async Task<IActionResult> AuditedAccSubmissionReport()
        {
            return View();
        }

        [Route("LLPSR")]
        public async Task<IActionResult> LLPSubmissionReport()
        {
            return View();
        }

        [Route("StrikeOffSchedule")]
        public async Task<IActionResult> StrikeOffSchedule()
        {
            return View();
        }
        #endregion



        #region API/Ajax
        [HttpGet("api/s16/get-all")]
        public async Task<IActionResult> GetAllS16Records()
        {
            try
            {
                using var connection = new SqlConnection(_connectionString);
                var sql = "SELECT * FROM [Quartz].[dbo].[S16] ORDER BY Id DESC";
                var records = await connection.QueryAsync<S16Model>(sql);

                return Json(new { success = true, data = records });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        [HttpGet("api/s16/get/{id}")]
        public async Task<IActionResult> GetS16ById(int id)
        {
            try
            {
                using var connection = new SqlConnection(_connectionString);
                var sql = "SELECT * FROM [Quartz].[dbo].[S16] WHERE Id = @Id";
                var record = await connection.QueryFirstOrDefaultAsync<S16Model>(sql, new { Id = id });

                if (record == null)
                    return Json(new { success = false, message = "Record not found" });

                return Json(new { success = true, data = record });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        [HttpPost("api/s16/create")]
        public async Task<IActionResult> CreateS16([FromBody] S16Model model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return Json(new { success = false, errors = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage)) });

                using var connection = new SqlConnection(_connectionString);
                var sql = @"INSERT INTO [Quartz].[dbo].[S16] 
                            ([Ref], [CompanyName], [CompanyNo], [IncorpDate], [YearEnd], 
                             [StartDate], [CompleteDate], [DoneBy], [CompletedDate], 
                             [PenaltiesRM], [RevisedPenalties], [AppealDate], [PaymentDate], 
                             [S_OffDocsendtoClient], [SSMsubmitDate], [SSMstrikeoffDate], 
                             [Remark], [Action], [DatePassToTaxDept], [FormCSubmitDate], [JobCompleted])
                            VALUES 
                            (@Ref, @CompanyName, @CompanyNo, @IncorpDate, @YearEnd, 
                             @StartDate, @CompleteDate, @DoneBy, @CompletedDate, 
                             @PenaltiesRM, @RevisedPenalties, @AppealDate, @PaymentDate, 
                             @S_OffDocsendtoClient, @SSMsubmitDate, @SSMstrikeoffDate, 
                             @Remark, @Action, @DatePassToTaxDept, @FormCSubmitDate, @JobCompleted);
                            SELECT CAST(SCOPE_IDENTITY() as int);";

                var id = await connection.ExecuteScalarAsync<int>(sql, model);
                return Json(new { success = true, id = id, data = model });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        [HttpPut("api/s16/update")]
        public async Task<IActionResult> UpdateS16([FromBody] S16Model model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return Json(new { success = false, errors = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage)) });

                using var connection = new SqlConnection(_connectionString);
                var sql = @"UPDATE [Quartz].[dbo].[S16] SET 
                            [Ref] = @Ref, [CompanyName] = @CompanyName, [CompanyNo] = @CompanyNo, 
                            [IncorpDate] = @IncorpDate, [YearEnd] = @YearEnd, 
                            [StartDate] = @StartDate, [CompleteDate] = @CompleteDate, 
                            [DoneBy] = @DoneBy, [CompletedDate] = @CompletedDate, 
                            [PenaltiesRM] = @PenaltiesRM, [RevisedPenalties] = @RevisedPenalties, 
                            [AppealDate] = @AppealDate, [PaymentDate] = @PaymentDate, 
                            [S_OffDocsendtoClient] = @S_OffDocsendtoClient, 
                            [SSMsubmitDate] = @SSMsubmitDate, [SSMstrikeoffDate] = @SSMstrikeoffDate, 
                            [Remark] = @Remark, [Action] = @Action, 
                            [DatePassToTaxDept] = @DatePassToTaxDept, 
                            [FormCSubmitDate] = @FormCSubmitDate, [JobCompleted] = @JobCompleted
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

        [HttpDelete("api/s16/delete/{id}")]
        public async Task<IActionResult> DeleteS16(int id)
        {
            try
            {
                using var connection = new SqlConnection(_connectionString);
                var sql = "DELETE FROM [Quartz].[dbo].[S16] WHERE Id = @Id";
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

        // 获取公司列表用于下拉选择
        [HttpGet("api/s16/get-companies")]
        public async Task<IActionResult> GetS16Companies()
        {
            try
            {
                using var connection = new SqlConnection(_connectionString);
                var sql = @"SELECT Id, CompanyName, YearEnd, SSMsubmitDate, SSMstrikeoffDate, 
                                   DatePassToTaxDept, FormCSubmitDate 
                            FROM [Quartz].[dbo].[S16] 
                            ORDER BY CompanyName";
                var records = await connection.QueryAsync<S16CompanyModel>(sql);

                return Json(new { success = true, data = records });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }
        #endregion




    }

}