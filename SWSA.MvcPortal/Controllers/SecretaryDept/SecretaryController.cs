using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Dapper;
using SWSA.MvcPortal.Entities.Templates;
using SWSA.MvcPortal.Persistence;
using SWSA.MvcPortal.Models.SecDeptModel;
using System.Globalization;
using SWSA.MvcPortal.Services.Interfaces.Clients;

namespace SWSA.MvcPortal.Controllers.SecretaryDept
{
    [Route("secretary-dept")]
    public class SecretaryController : BaseController
    {
        private readonly AppDbContext _db;
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;
        private readonly DbSet<SecDeptTaskTemplate> _tasks;
        private readonly IClientService _clientService;

        public SecretaryController(AppDbContext db, IConfiguration configuration,IClientService clientService)
        {
            _db = db;
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("SwsaConntection");
            _tasks = _db.Set<SecDeptTaskTemplate>();
            _clientService = clientService;
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

        public class S16Tx4SourceOption
        {
            public int Tx4Id { get; set; }             // TX4.Id
            public string CompanyName { get; set; }    // 公司名
            public string YearEnd { get; set; }        // dd/MM/yyyy

            public string SsmStrikeOffDate { get; set; }      // = TX4.DateSOff
            public string DatePassToTaxDept { get; set; }     // = TX4.DateReceiveFrSecDept
            public string FormCSubmissionDate { get; set; }   // = TX4.FormCsubmitDate
        }

        [HttpGet("api/s16/get-tx4-source-options")]
        public async Task<IActionResult> GetS16Tx4SourceOptions()
        {
            try
            {
                using var connection = new SqlConnection(_connectionString);

                var sql = @"
SELECT 
    Id                                    AS Tx4Id,
    CompanyName,
    CONVERT(varchar(10), YearEnd, 103)    AS YearEnd,             -- dd/MM/yyyy
    CONVERT(varchar(10), DateSOff, 103)   AS SsmStrikeOffDate,    -- dd/MM/yyyy
    CONVERT(varchar(10), DateReceiveFrSecDept, 103) AS DatePassToTaxDept,   -- dd/MM/yyyy
    CONVERT(varchar(10), FormCsubmitDate, 103) AS FormCSubmissionDate      -- dd/MM/yyyy
FROM [Quartz].[dbo].[TX4]
ORDER BY CompanyName, YearEnd DESC;
";

                var list = (await connection.QueryAsync<S16Tx4SourceOption>(sql)).ToList();

                return Json(new { success = true, data = list });
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
                model.Id = id;

                var s14bAction = await UpsertS14BFromS13AAsync(connection, model);

                return Json(new
                {
                    success = true,
                    id = id,
                    data = model,
                    s14bAction = s14bAction   // ⚡ 关键：告诉前端是 created / updated
                });
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
                await connection.OpenAsync();

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

                var s14bAction = await UpsertS14BFromS13AAsync(connection, model);
                return Json(new { success = true, data = model, s14bAction });

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

        [HttpGet("get-s13a-source-options")]
        public async Task<IActionResult> GetS13ASourceOptions()
        {
            try
            {
                using var connection = new SqlConnection(_connectionString);

                var sql = @"
SELECT 
    Id                                       AS SourceId,
    'AT21'                                   AS SourceType,
    CompanyName,
    CONVERT(varchar(50), YearEnd, 113)       AS YearEnd,        -- 先随便转成字串
    CONVERT(varchar(50), First18mthdue, 113) AS First18MthsDue,
    CONVERT(varchar(50), AFSdueDate, 113)    AS AfsDueDate
FROM [Quartz].[dbo].[AT21]

UNION ALL

SELECT
    Id                                       AS SourceId,
    'AEX41'                                  AS SourceType,
    CompanyName,
    CONVERT(varchar(50), YearEnd, 113)       AS YearEnd,
    CONVERT(varchar(50), First18mthsDue, 113)AS First18MthsDue,
    CONVERT(varchar(50), AuditedAccDueDate, 113) AS AfsDueDate
FROM [Quartz].[dbo].[AEX41];";

                var list = (await connection.QueryAsync<S13ASourceOption>(sql)).ToList();

                // ✅ 在这里统一改成 dd/MM/yyyy
                foreach (var x in list)
                {
                    x.YearEnd = NormalizeToDdMmYyyy(x.YearEnd);
                    x.First18MthsDue = NormalizeToDdMmYyyy(x.First18MthsDue);
                    x.AfsDueDate = NormalizeToDdMmYyyy(x.AfsDueDate);
                }

                return Json(new { success = true, data = list });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }


        public class S13ASourceOption
        {
            public string SourceType { get; set; }      // "AT21" / "AEX41"
            public int SourceId { get; set; }
            public string CompanyName { get; set; }

            public string YearEnd { get; set; }         // dd/MM/yyyy
            public string First18MthsDue { get; set; }  // dd/MM/yyyy
            public string AfsDueDate { get; set; }      // dd/MM/yyyy
        }

        /// <summary>
        /// 从一条 S13A 记录，同步/生成 S14B：
        /// - 以 FileNo + YearEnd 作为「一家公司一个 Year」的 key。
        /// - 有就 UPDATE，没有就 INSERT。
        /// </summary>
        private async Task<string> UpsertS14BFromS13AAsync(SqlConnection connection, S13A s13a)
        {
            var incorpFromAr = CalculateIncorpDateFromArDue(s13a.ARdueDate);

            var finalIncorp = !string.IsNullOrWhiteSpace(incorpFromAr)
                ? incorpFromAr
                : (s13a.IncorpDate ?? string.Empty);

            var param = new
            {
                FileNo = s13a.SecFileNo ?? string.Empty,
                CompanyName = s13a.CompanyName ?? string.Empty,

                // 🔹 这里用 S13A 带过来的 CompanyNo
                CompanyNo = s13a.CompanyNo ?? string.Empty,

                // 🔹 IncorpDate 最终值（Ar 算出来优先，其次用 S13A 的）
                IncorpDate = finalIncorp,

                YearEnd = s13a.YearEnd ?? string.Empty,
                CompanyStatus = s13a.CompanyStatus ?? string.Empty,

                YrMthdueDate = s13a.YrMthDueDate ?? string.Empty,
                CirculationAFSduedate = s13a.Circulation ?? string.Empty,

                MBRSreceivedDate = string.Empty, // 如果将来要从 AFSSubmitDate 带，也可以改这里
                OntimeLate = string.Empty,
                ReasonForLate = string.Empty,
                JobCompleted = s13a.JobCompleted ?? string.Empty
            };
            var sql = @"
DECLARE @Action nvarchar(10);

IF EXISTS (
    SELECT 1
    FROM [Quartz].[dbo].[S14B]
    WHERE FileNo = @FileNo
      AND YearEnd = @YearEnd
)
BEGIN
    UPDATE [Quartz].[dbo].[S14B]
    SET CompanyName   = @CompanyName,
        CompanyNo     = CASE WHEN CompanyNo IS NULL OR CompanyNo = '' THEN @CompanyNo ELSE CompanyNo END,
        IncorpDate    = CASE WHEN (IncorpDate IS NULL OR IncorpDate = '') AND @IncorpDate <> '' THEN @IncorpDate ELSE IncorpDate END,
        CompanyStatus = @CompanyStatus,
        YrMthdueDate = @YrMthdueDate,
        CirculationAFSduedate = @CirculationAFSduedate,
        JobCompleted  = @JobCompleted
    WHERE FileNo = @FileNo
      AND YearEnd = @YearEnd;

    SET @Action = 'updated';
END
ELSE
BEGIN
    INSERT INTO [Quartz].[dbo].[S14B]
        ([FileNo],
         [CompanyName],
         [CompanyNo],
         [IncorpDate],
         [YearEnd],
         [CompanyStatus],
         [YrMthdueDate],
         [CirculationAFSduedate],
         [MBRSreceivedDate],
         [OntimeLate],
         [ReasonForLate],
         [JobCompleted])
    VALUES
        (@FileNo,
         @CompanyName,
         @CompanyNo,
         @IncorpDate,
         @YearEnd,
         @CompanyStatus,
         @YrMthdueDate,
         @CirculationAFSduedate,
         @MBRSreceivedDate,
         @OntimeLate,
         @ReasonForLate,
         @JobCompleted);

    SET @Action = 'created';
END

SELECT @Action;
";

            var action = await connection.ExecuteScalarAsync<string>(sql, param);
            return action ?? "none";
        }




        private string NormalizeToDdMmYyyy(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return string.Empty;

            // 尝试各种格式，让 .NET 自己 parse
            if (DateTime.TryParse(value, out var dt))
            {
                return dt.ToString("dd/MM/yyyy");   // 统一成日/月/年
            }

            // parse 不到就原样返回，至少不会报错
            return value;
        }

        // 根据 ARdueDate(字符串) 计算 IncorpDate = ARdueDate 往前一年
        private string CalculateIncorpDateFromArDue(string arDueDate)
        {
            if (string.IsNullOrWhiteSpace(arDueDate))
                return string.Empty;

            // 你现在的格式是 dd/MM/yyyy
            var formats = new[] { "dd/MM/yyyy", "d/M/yyyy" };

            if (DateTime.TryParseExact(arDueDate,
                                       formats,
                                       CultureInfo.InvariantCulture,
                                       DateTimeStyles.None,
                                       out var dt))
            {
                var incorp = dt.AddYears(-1);
                return incorp.ToString("dd/MM/yyyy");   // 一样保持 dd/MM/yyyy 的 string
            }

            // 如果 format 奇怪 / 解析不到，就放空，避免出错
            return string.Empty;
        }
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

        [HttpGet("get-s14b-mbrs-source-options")]
        public async Task<IActionResult> GetS14B_MbrsSourceOptions()
        {
            try
            {
                using var connection = new SqlConnection(_connectionString);

                var sql = @"
SELECT 
    Id AS SourceId,
    'AT31' AS SourceType,
    CompanyName,
    CONVERT(varchar(50), DateReceiveBack, 113) AS MbrsReceivedDate
FROM [Quartz].[dbo].[AT31];
";

                var list = (await connection.QueryAsync<S14BMbrsSourceOption>(sql)).ToList();

                // 统一转 dd/MM/yyyy
                foreach (var x in list)
                {
                    x.MbrsReceivedDate = NormalizeToDdMmYyyy(x.MbrsReceivedDate);
                }

                return Json(new { success = true, data = list });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        public class S14BMbrsSourceOption
        {
            public string SourceType { get; set; }        // "AT31"
            public int SourceId { get; set; }
            public string CompanyName { get; set; }
            public string MbrsReceivedDate { get; set; }  // dd/MM/yyyy
        }
        #endregion

        #region API S15
        [HttpGet("api/s15/get-all")]
        public async Task<IActionResult> GetAllS15Records()
        {
            try
            {
                using var connection = new SqlConnection(_connectionString);
                var sql = "SELECT * FROM [Quartz].[dbo].[S15] ORDER BY Id DESC";
                var records = await connection.QueryAsync<S15>(sql);

                return Json(new { success = true, data = records });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        [HttpGet("api/s15/get/{id}")]
        public async Task<IActionResult> GetS15ById(int id)
        {
            try
            {
                using var connection = new SqlConnection(_connectionString);
                var sql = "SELECT * FROM [Quartz].[dbo].[S15] WHERE Id = @Id";
                var record = await connection.QueryFirstOrDefaultAsync<S15>(sql, new { Id = id });

                if (record == null)
                    return Json(new { success = false, message = "Record not found" });

                return Json(new { success = true, data = record });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        [HttpPost("api/s15/create")]
        public async Task<IActionResult> CreateS15([FromBody] S15 model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return Json(new { success = false, errors = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage)) });

                using var connection = new SqlConnection(_connectionString);
                var sql = @"INSERT INTO [Quartz].[dbo].[S15]
                ([SecFileNo], [CompanyName], [YearEnd], [IncorpDate], [Co],
                [CompanyStatus], [ActiveCoActivitySize], [SSMextensionDateforAcc],
                [ADdueDate], [ADsubmitDate], [DateSendtoClient], [DateReturned], [JobCompleted])
                VALUES
                (@SecFileNo, @CompanyName, @YearEnd, @IncorpDate, @Co,
                @CompanyStatus, @ActiveCoActivitySize, @SSMextensionDateforAcc,
                @ADdueDate, @ADsubmitDate, @DateSendtoClient, @DateReturned, @JobCompleted);
                SELECT CAST(SCOPE_IDENTITY() AS int);";

                var id = await connection.ExecuteScalarAsync<int>(sql, model);
                return Json(new { success = true, id = id, data = model });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        [HttpPut("api/s15/update")]
        public async Task<IActionResult> UpdateS15([FromBody] S15 model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return Json(new { success = false, errors = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage)) });

                using var connection = new SqlConnection(_connectionString);
                var sql = @"UPDATE [Quartz].[dbo].[S15] SET
                [SecFileNo] = @SecFileNo,
                [CompanyName] = @CompanyName,
                [YearEnd] = @YearEnd,
                [IncorpDate] = @IncorpDate,
                [Co] = @Co,
                [CompanyStatus] = @CompanyStatus,
                [ActiveCoActivitySize] = @ActiveCoActivitySize,
                [SSMextensionDateforAcc] = @SSMextensionDateforAcc,
                [ADdueDate] = @ADdueDate,
                [ADsubmitDate] = @ADsubmitDate,
                [DateSendtoClient] = @DateSendtoClient,
                [DateReturned] = @DateReturned,
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

        [HttpDelete("api/s15/delete/{id}")]
        public async Task<IActionResult> DeleteS15(int id)
        {
            try
            {
                using var connection = new SqlConnection(_connectionString);
                var sql = "DELETE FROM [Quartz].[dbo].[S15] WHERE Id = @Id";
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

        [HttpGet("api/get/company-options")]
        public async Task<IActionResult> GetCompanyOptions()
        {
            try
            {
                var list = await _clientService.GetCompanyOptionsAsync();
                return Json(new { success = true, data = list });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }
    }

}