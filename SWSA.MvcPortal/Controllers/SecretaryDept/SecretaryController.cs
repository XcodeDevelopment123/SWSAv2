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



        #region API  S16
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

        #region API S13A
        [HttpGet("api/s13a/get-all")]
        public async Task<IActionResult> GetAllS13ARecords()
        {
            try
            {
                using var connection = new SqlConnection(_connectionString);
                var sql = "SELECT * FROM [Quartz].[dbo].[S13A] ORDER BY Id DESC";
                var records = await connection.QueryAsync<S13A>(sql);

                return Json(new { success = true, data = records });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        [HttpGet("api/s13a/get/{id}")]
        public async Task<IActionResult> GetS13AById(int id)
        {
            try
            {
                using var connection = new SqlConnection(_connectionString);
                var sql = "SELECT * FROM [Quartz].[dbo].[S13A] WHERE Id = @Id";
                var record = await connection.QueryFirstOrDefaultAsync<S13A>(sql, new { Id = id });

                if (record == null)
                    return Json(new { success = false, message = "Record not found" });

                return Json(new { success = true, data = record });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        [HttpPost("api/s13a/create")]
        public async Task<IActionResult> CreateS13A([FromBody] S13A model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return Json(new { success = false, errors = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage)) });

                using var connection = new SqlConnection(_connectionString);
                var sql = @"INSERT INTO [Quartz].[dbo].[S13A]
            ([Grouping], [Referral], [SecFileNo], [CompanyName], [CompanyType], 
             [YearEnd], [CompanyStatus], [ActiveCoActivitySize], [YEtoDo], 
             [ACCmthTodo], [AuditMthTodo], [YrMthDueDate], [Circulation], 
             [ARdueDate], [AFSSubmitDate], [ARSubmitDate], [JobCompleted])
            VALUES
            (@Grouping, @Referral, @SecFileNo, @CompanyName, @CompanyType, 
             @YearEnd, @CompanyStatus, @ActiveCoActivitySize, @YEtoDo, 
             @ACCmthTodo, @AuditMthTodo, @YrMthDueDate, @Circulation, 
             @ARdueDate, @AFSSubmitDate, @ARSubmitDate, @JobCompleted);

            SELECT CAST(SCOPE_IDENTITY() AS int);";


                var id = await connection.ExecuteScalarAsync<int>(sql, model);
                return Json(new { success = true, id = id, data = model });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        [HttpPut("api/s13a/update")]
        public async Task<IActionResult> UpdateS13A([FromBody] S13A model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return Json(new { success = false, errors = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage)) });

                using var connection = new SqlConnection(_connectionString);
                var sql = @"UPDATE [Quartz].[dbo].[S13A] SET
            [Grouping] = @Grouping,
            [Referral] = @Referral,
            [SecFileNo] = @SecFileNo,
            [CompanyName] = @CompanyName,
            [CompanyType] = @CompanyType,
            [YearEnd] = @YearEnd,
            [CompanyStatus] = @CompanyStatus,
            [ActiveCoActivitySize] = @ActiveCoActivitySize,
            [YEtoDo] = @YEtoDo,
            [ACCmthTodo] = @ACCmthTodo,
            [AuditMthTodo] = @AuditMthTodo,
            [YrMthDueDate] = @YrMthDueDate,
            [Circulation] = @Circulation,
            [ARdueDate] = @ARdueDate,
            [AFSSubmitDate] = @AFSSubmitDate,
            [ARSubmitDate] = @ARSubmitDate,
            [JobCompleted] = @JobCompleted
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

        [HttpDelete("api/s13a/delete/{id}")]
        public async Task<IActionResult> DeleteS13A(int id)
        {
            try
            {
                using var connection = new SqlConnection(_connectionString);
                var sql = "DELETE FROM [Quartz].[dbo].[S13A] WHERE Id = @Id";
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

        //// 获取公司列表用于下拉选择
        //[HttpGet("api/s13a/get-companies")]
        //public async Task<IActionResult> GetS13ACompanies()
        //{
        //    try
        //    {
        //        using var connection = new SqlConnection(_connectionString);
        //        var sql = @"SELECT Id, CompanyName, YearEnd, SSMsubmitDate, SSMstrikeoffDate, 
        //                           DatePassToTaxDept, FormCSubmitDate 
        //                    FROM [Quartz].[dbo].[S13A] 
        //                    ORDER BY CompanyName";
        //        var records = await connection.QueryAsync<S13A>(sql);

        //        return Json(new { success = true, data = records });
        //    }
        //    catch (Exception ex)
        //    {
        //        return Json(new { success = false, message = ex.Message });
        //    }
        //}
        #endregion

        #region API S14A
        [HttpGet("api/s14a/get-all")]
        public async Task<IActionResult> GetAllS14ARecords()
        {
            try
            {
                using var connection = new SqlConnection(_connectionString);
                var sql = "SELECT * FROM [Quartz].[dbo].[S14A] ORDER BY Id DESC";
                var records = await connection.QueryAsync<S14A>(sql);

                return Json(new { success = true, data = records });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        [HttpGet("api/s14a/get/{id}")]
        public async Task<IActionResult> GetS14AById(int id)
        {
            try
            {
                using var connection = new SqlConnection(_connectionString);
                var sql = "SELECT * FROM [Quartz].[dbo].[S14A] WHERE Id = @Id";
                var record = await connection.QueryFirstOrDefaultAsync<S14A>(sql, new { Id = id });

                if (record == null)
                    return Json(new { success = false, message = "Record not found" });

                return Json(new { success = true, data = record });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        [HttpPost("api/s14a/create")]
        public async Task<IActionResult> CreateS14A([FromBody] S14A model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return Json(new { success = false, errors = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage)) });

                using var connection = new SqlConnection(_connectionString);
                var sql = @"INSERT INTO [Quartz].[dbo].[S14A]
([FileNo], [CompanyName], [CompanyNo], [IncorpDate], [CompanyStatus],
 [AnniversaryDate], [ARdueDate], [ReminderDate], [DateOfAR],
 [ARsubmitDate], [DateSendtoClient], [DateReturned], [JobCompleted],
 [Remarks])
VALUES
(@FileNo, @CompanyName, @CompanyNo, @IncorpDate, @CompanyStatus,
 @AnniversaryDate, @ARdueDate, @ReminderDate, @DateOfAR,
 @ARsubmitDate, @DateSendtoClient, @DateReturned, @JobCompleted,
 @Remarks);

SELECT CAST(SCOPE_IDENTITY() AS int);";



                var id = await connection.ExecuteScalarAsync<int>(sql, model);
                return Json(new { success = true, id = id, data = model });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        [HttpPut("api/s14a/update")]
        public async Task<IActionResult> UpdateS14A([FromBody] S14A model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return Json(new { success = false, errors = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage)) });

                using var connection = new SqlConnection(_connectionString);
                var sql = @"UPDATE [Quartz].[dbo].[S14A] SET
 [FileNo] = @FileNo,
 [CompanyName] = @CompanyName,
 [CompanyNo] = @CompanyNo,
 [IncorpDate] = @IncorpDate,
 [CompanyStatus] = @CompanyStatus,
 [AnniversaryDate] = @AnniversaryDate,
 [ARdueDate] = @ARdueDate,
 [ReminderDate] = @ReminderDate,
 [DateOfAR] = @DateOfAR,
 [ARsubmitDate] = @ARsubmitDate,
 [DateSendtoClient] = @DateSendtoClient,
 [DateReturned] = @DateReturned,
 [JobCompleted] = @JobCompleted,
 [Remarks] = @Remarks
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

        [HttpDelete("api/s14a/delete/{id}")]
        public async Task<IActionResult> DeleteS14A(int id)
        {
            try
            {
                using var connection = new SqlConnection(_connectionString);
                var sql = "DELETE FROM [Quartz].[dbo].[S14A] WHERE Id = @Id";
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

        //// 获取公司列表用于下拉选择
        //[HttpGet("api/s13a/get-companies")]
        //public async Task<IActionResult> GetS13ACompanies()
        //{
        //    try
        //    {
        //        using var connection = new SqlConnection(_connectionString);
        //        var sql = @"SELECT Id, CompanyName, YearEnd, SSMsubmitDate, SSMstrikeoffDate, 
        //                           DatePassToTaxDept, FormCSubmitDate 
        //                    FROM [Quartz].[dbo].[S13A] 
        //                    ORDER BY CompanyName";
        //        var records = await connection.QueryAsync<S13A>(sql);

        //        return Json(new { success = true, data = records });
        //    }
        //    catch (Exception ex)
        //    {
        //        return Json(new { success = false, message = ex.Message });
        //    }
        //}
        #endregion

        #region API S14B
        [HttpGet("api/s14b/get-all")]
        public async Task<IActionResult> GetAllS14BRecords()
        {
            try
            {
                using var connection = new SqlConnection(_connectionString);
                var sql = "SELECT * FROM [Quartz].[dbo].[S14B] ORDER BY Id DESC";
                var records = await connection.QueryAsync<S14B>(sql);

                return Json(new { success = true, data = records });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        [HttpGet("api/s14b/get/{id}")]
        public async Task<IActionResult> GetS14BById(int id)
        {
            try
            {
                using var connection = new SqlConnection(_connectionString);
                var sql = "SELECT * FROM [Quartz].[dbo].[S14B] WHERE Id = @Id";
                var record = await connection.QueryFirstOrDefaultAsync<S14B>(sql, new { Id = id });

                if (record == null)
                    return Json(new { success = false, message = "Record not found" });

                return Json(new { success = true, data = record });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        [HttpPost("api/s14b/create")]
        public async Task<IActionResult> CreateS14B([FromBody] S14B model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return Json(new { success = false, errors = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage)) });

                using var connection = new SqlConnection(_connectionString);
                var sql = @"INSERT INTO [Quartz].[dbo].[S14B]
		([FileNo], [CompanyName], [CompanyNo], [IncorpDate], [YearEnd],
 		[CompanyStatus], [YrMthdueDate], [CirculationAFSduedate],
 		[MBRSreceivedDate], [OntimeLate], [ReasonForLate], [JobCompleted])
		VALUES
		(@FileNo, @CompanyName, @CompanyNo, @IncorpDate, @YearEnd,
 		@CompanyStatus, @YrMthdueDate, @CirculationAFSduedate,
 		@MBRSreceivedDate, @OntimeLate, @ReasonForLate, @JobCompleted);

		SELECT CAST(SCOPE_IDENTITY() AS int);";



                var id = await connection.ExecuteScalarAsync<int>(sql, model);
                return Json(new { success = true, id = id, data = model });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        [HttpPut("api/s14b/update")]
        public async Task<IActionResult> UpdateS14B([FromBody] S14B model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return Json(new { success = false, errors = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage)) });

                using var connection = new SqlConnection(_connectionString);
                var sql = @"UPDATE [Quartz].[dbo].[S14B] SET
 		[FileNo] = @FileNo,
 		[CompanyName] = @CompanyName,
 		[CompanyNo] = @CompanyNo,
 		[IncorpDate] = @IncorpDate,
 		[YearEnd] = @YearEnd,
		[CompanyStatus] = @CompanyStatus,
 		[YrMthdueDate] = @YrMthdueDate,
 		[CirculationAFSduedate] = @CirculationAFSduedate,
 		[MBRSreceivedDate] = @MBRSreceivedDate,
 		[OntimeLate] = @OntimeLate,
 		[ReasonForLate] = @ReasonForLate,
 		[JobCompleted] = @JobCompleted
		WHERE Id = @Id;";



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

        [HttpDelete("api/s14b/delete/{id}")]
        public async Task<IActionResult> DeleteS14B(int id)
        {
            try
            {
                using var connection = new SqlConnection(_connectionString);
                var sql = "DELETE FROM [Quartz].[dbo].[S14B] WHERE Id = @Id";
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

        //// 获取公司列表用于下拉选择
        //[HttpGet("api/s13a/get-companies")]
        //public async Task<IActionResult> GetS13ACompanies()
        //{
        //    try
        //    {
        //        using var connection = new SqlConnection(_connectionString);
        //        var sql = @"SELECT Id, CompanyName, YearEnd, SSMsubmitDate, SSMstrikeoffDate, 
        //                           DatePassToTaxDept, FormCSubmitDate 
        //                    FROM [Quartz].[dbo].[S13A] 
        //                    ORDER BY CompanyName";
        //        var records = await connection.QueryAsync<S13A>(sql);

        //        return Json(new { success = true, data = records });
        //    }
        //    catch (Exception ex)
        //    {
        //        return Json(new { success = false, message = ex.Message });
        //    }
        //}
        #endregion

    }

}