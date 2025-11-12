using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using SWSA.MvcPortal.Models.Clients;
using SWSA.MvcPortal.Models.TaxDeptModel;

namespace SWSA.MvcPortal.Controllers.TaxDept
{
    public class FormCController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;

        public FormCController(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("SwsaConntection");
        }



        public IActionResult FormC()
        {
            return View();
        }

        public IActionResult TaxDeptWorkSchedule()
        {
            return View();
        }

        public IActionResult TaxWorkLogbook()
        {
            return View();
        }

        public IActionResult StrikeOffTaxWork()
        {
            return View();
        }

        public IActionResult TX5TEMPLATE()
        {
            return View();
        }




        #region FormC Methods
        [HttpGet("api/formc/get-all")]
        public async Task<IActionResult> GetAllFormCRecords()
        {
            try
            {
                using var connection = new SqlConnection(_connectionString);
                var sql = "SELECT * FROM [Quartz].[dbo].[FormC] ORDER BY Id DESC";
                var records = await connection.QueryAsync<FormCModel>(sql);

                return Json(new { success = true, data = records });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        [HttpGet("api/formc/get/{id}")]
        public async Task<IActionResult> GetFormCById(int id)
        {
            try
            {
                using var connection = new SqlConnection(_connectionString);
                var sql = "SELECT * FROM [Quartz].[dbo].[FormC] WHERE Id = @Id";
                var record = await connection.QueryFirstOrDefaultAsync<FormCModel>(sql, new { Id = id });

                if (record == null)
                    return Json(new { success = false, message = "Record not found" });

                return Json(new { success = true, data = record });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        [HttpPost("api/formc/create")]
        public async Task<IActionResult> CreateFormC([FromBody] FormCModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return Json(new { success = false, errors = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage)) });

                using var connection = new SqlConnection(_connectionString);
                var sql = @"INSERT INTO [Quartz].[dbo].[FormC] 
                            ([TaxDueDate], [EstQuarterTodo], [DateMgmtAccAvailable], [StartDate], 
                             [EndDate], [NoOfDays], [PnLAnalysis], [CAnTaxCompu], [DraftFormC], 
                             [TaxPayableRM], [TaxCompCA], [FormC], [Sent], [Received], 
                             [TaxPaymentDate], [FormCsubmitedDate], [InvDate], [Fees], 
                             [Printing], [Despatch])
                            VALUES 
                            (@TaxDueDate, @EstQuarterTodo, @DateMgmtAccAvailable, @StartDate, 
                             @EndDate, @NoOfDays, @PnLAnalysis, @CAnTaxCompu, @DraftFormC, 
                             @TaxPayableRM, @TaxCompCA, @FormC, @Sent, @Received, 
                             @TaxPaymentDate, @FormCsubmitedDate, @InvDate, @Fees, 
                             @Printing, @Despatch, @JobCompleted);
                            SELECT CAST(SCOPE_IDENTITY() as int);";

                var id = await connection.ExecuteScalarAsync<int>(sql, model);
                return Json(new { success = true, id = id, data = model });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        [HttpPut("api/formc/update")]
        public async Task<IActionResult> UpdateFormC([FromBody] FormCModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return Json(new { success = false, errors = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage)) });

                using var connection = new SqlConnection(_connectionString);
                var sql = @"UPDATE [Quartz].[dbo].[FormC] SET 
                            [TaxDueDate] = @TaxDueDate, [EstQuarterTodo] = @EstQuarterTodo, 
                            [DateMgmtAccAvailable] = @DateMgmtAccAvailable, [StartDate] = @StartDate, 
                            [EndDate] = @EndDate, [NoOfDays] = @NoOfDays, [PnLAnalysis] = @PnLAnalysis, 
                            [CAnTaxCompu] = @CAnTaxCompu, [DraftFormC] = @DraftFormC, 
                            [TaxPayableRM] = @TaxPayableRM, [TaxCompCA] = @TaxCompCA, 
                            [FormC] = @FormC, [Sent] = @Sent, [Received] = @Received, 
                            [TaxPaymentDate] = @TaxPaymentDate, [FormCsubmitedDate] = @FormCsubmitedDate, 
                            [InvDate] = @InvDate, [Fees] = @Fees, [Printing] = @Printing, 
                            [Despatch] = @Despatch,
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

        [HttpDelete("api/formc/delete/{id}")]
        public async Task<IActionResult> DeleteFormC(int id)
        {
            try
            {
                using var connection = new SqlConnection(_connectionString);
                var sql = "DELETE FROM [Quartz].[dbo].[FormC] WHERE Id = @Id";
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

        #region TX2 (TaxDeptWorkSchedule) Methods
        [HttpGet("api/tx2/get-all")]
        public async Task<IActionResult> GetAllTX2Records()
        {
            try
            {
                using var connection = new SqlConnection(_connectionString);
                var sql = "SELECT * FROM [Quartz].[dbo].[TX2] ORDER BY Id DESC";
                var records = await connection.QueryAsync<TX2Model>(sql);

                return Json(new { success = true, data = records });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        [HttpGet("api/tx2/get/{id}")]
        public async Task<IActionResult> GetTX2ById(int id)
        {
            try
            {
                using var connection = new SqlConnection(_connectionString);
                var sql = "SELECT * FROM [Quartz].[dbo].[TX2] WHERE Id = @Id";
                var record = await connection.QueryFirstOrDefaultAsync<TX2Model>(sql, new { Id = id });

                if (record == null)
                    return Json(new { success = false, message = "Record not found" });

                return Json(new { success = true, data = record });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        [HttpPost("api/tx2/create")]
        public async Task<IActionResult> CreateTX2([FromBody] TX2Model model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return Json(new { success = false, errors = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage)) });

                using var connection = new SqlConnection(_connectionString);
                var sql = @"INSERT INTO [Quartz].[dbo].[TX2] 
                            ([CompanyName], [Activity], [AEXOT], [RAKC], [BTM], 
                             [YearEnd], [TaxDueDate], [EstMthTodo], [TransferToWIPTX3], 
                             [Revenue], [NetProfit], [StartDate], [TotalPercent], 
                             [DocPassFrAuditDept], [DateMgmtAccAvailable])
                            VALUES 
                            (@CompanyName, @Activity, @AEXOT, @RAKC, @BTM, 
                             @YearEnd, @TaxDueDate, @EstMthTodo, @TransferToWIPTX3, 
                             @Revenue, @NetProfit, @StartDate, @TotalPercent, 
                             @DocPassFrAuditDept, @DateMgmtAccAvailable);
                            SELECT CAST(SCOPE_IDENTITY() as int);";

                var id = await connection.ExecuteScalarAsync<int>(sql, model);
                return Json(new { success = true, id = id, data = model });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        [HttpPut("api/tx2/update")]
        public async Task<IActionResult> UpdateTX2([FromBody] TX2Model model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return Json(new { success = false, errors = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage)) });

                using var connection = new SqlConnection(_connectionString);
                var sql = @"UPDATE [Quartz].[dbo].[TX2] SET 
                            [CompanyName] = @CompanyName, [Activity] = @Activity, 
                            [AEXOT] = @AEXOT, [RAKC] = @RAKC, [BTM] = @BTM, 
                            [YearEnd] = @YearEnd, [TaxDueDate] = @TaxDueDate, 
                            [EstMthTodo] = @EstMthTodo, [TransferToWIPTX3] = @TransferToWIPTX3, 
                            [Revenue] = @Revenue, [NetProfit] = @NetProfit, 
                            [StartDate] = @StartDate, [TotalPercent] = @TotalPercent, 
                            [DocPassFrAuditDept] = @DocPassFrAuditDept, 
                            [DateMgmtAccAvailable] = @DateMgmtAccAvailable
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

        [HttpDelete("api/tx2/delete/{id}")]
        public async Task<IActionResult> DeleteTX2(int id)
        {
            try
            {
                using var connection = new SqlConnection(_connectionString);
                var sql = "DELETE FROM [Quartz].[dbo].[TX2] WHERE Id = @Id";
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

        // 获取 AT31 记录用于链接
        [HttpGet("api/at31/get-companies")]
        public async Task<IActionResult> GetAT31Companies()
        {
            try
            {
                using var connection = new SqlConnection(_connectionString);
                var sql = "SELECT Id, CompanyName, DocInwardsDate FROM [Quartz].[dbo].[AT31] ORDER BY CompanyName";
                var records = await connection.QueryAsync<dynamic>(sql);

                return Json(new { success = true, data = records });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }
        #endregion

        #region TX3 (TaxWorkLogbook) Methods
        [HttpGet("api/tx3/get-all")]
        public async Task<IActionResult> GetAllTX3Records()
        {
            try
            {
                using var connection = new SqlConnection(_connectionString);
                var sql = "SELECT * FROM [Quartz].[dbo].[TX3] ORDER BY Id DESC";
                var records = await connection.QueryAsync<TX3Model>(sql);

                return Json(new { success = true, data = records });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        [HttpGet("api/tx3/get/{id}")]
        public async Task<IActionResult> GetTX3ById(int id)
        {
            try
            {
                using var connection = new SqlConnection(_connectionString);
                var sql = "SELECT * FROM [Quartz].[dbo].[TX3] WHERE Id = @Id";
                var record = await connection.QueryFirstOrDefaultAsync<TX3Model>(sql, new { Id = id });

                if (record == null)
                    return Json(new { success = false, message = "Record not found" });

                return Json(new { success = true, data = record });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        [HttpPost("api/tx3/create")]
        public async Task<IActionResult> CreateTX3([FromBody] TX3Model model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return Json(new { success = false, errors = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage)) });

                using var connection = new SqlConnection(_connectionString);
                var sql = @"INSERT INTO [Quartz].[dbo].[TX3] 
                    ([CompanyName], [Active], [AEXOT], [RAKC], [BTM], 
                     [YearEnd], [TaxDueDate], [StartDate], [EndDate], [NoOfDays], 
                     [Completed], [PnLAnalysis], [CaTaxCompu], [DraftForm], 
                     [TaxPayable], [TaxCompCA], [FormC], [Sent], [Received], 
                     [TaxPaymentDate], [FormCSubmited], [InvDate], [Fees], 
                     [Printing], [Despatch])
                    VALUES 
                    (@CompanyName, @Active, @AEXOT, @RAKC, @BTM, 
                     @YearEnd, @TaxDueDate, @StartDate, @EndDate, @NoOfDays, 
                     @Completed, @PnLAnalysis, @CaTaxCompu, @DraftForm, 
                     @TaxPayable, @TaxCompCA, @FormC, @Sent, @Received, 
                     @TaxPaymentDate, @FormCSubmited, @InvDate, @Fees, 
                     @Printing, @Despatch);
                    SELECT CAST(SCOPE_IDENTITY() as int);";

                var id = await connection.ExecuteScalarAsync<int>(sql, model);
                return Json(new { success = true, id = id, data = model });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        [HttpPut("api/tx3/update")]
        public async Task<IActionResult> UpdateTX3([FromBody] TX3Model model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return Json(new { success = false, errors = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage)) });

                using var connection = new SqlConnection(_connectionString);
                var sql = @"UPDATE [Quartz].[dbo].[TX3] SET 
                    [CompanyName] = @CompanyName, [Active] = @Active, 
                    [AEXOT] = @AEXOT, [RAKC] = @RAKC, [BTM] = @BTM, 
                    [YearEnd] = @YearEnd, [TaxDueDate] = @TaxDueDate, 
                    [StartDate] = @StartDate, [EndDate] = @EndDate, 
                    [NoOfDays] = @NoOfDays, [Completed] = @Completed, 
                    [PnLAnalysis] = @PnLAnalysis, [CaTaxCompu] = @CaTaxCompu, 
                    [DraftForm] = @DraftForm, [TaxPayable] = @TaxPayable, 
                    [TaxCompCA] = @TaxCompCA, [FormC] = @FormC, 
                    [Sent] = @Sent, [Received] = @Received, 
                    [TaxPaymentDate] = @TaxPaymentDate, [FormCSubmited] = @FormCSubmited, 
                    [InvDate] = @InvDate, [Fees] = @Fees, 
                    [Printing] = @Printing, [Despatch] = @Despatch
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

        [HttpDelete("api/tx3/delete/{id}")]
        public async Task<IActionResult> DeleteTX3(int id)
        {
            try
            {
                using var connection = new SqlConnection(_connectionString);
                var sql = "DELETE FROM [Quartz].[dbo].[TX3] WHERE Id = @Id";
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

        #region TX4 (StrikeOffTaxWork) Methods
        [HttpGet("api/tx4/get-all")]
        public async Task<IActionResult> GetAllTX4Records()
        {
            try
            {
                using var connection = new SqlConnection(_connectionString);
                var sql = "SELECT * FROM [Quartz].[dbo].[TX4] ORDER BY Id DESC";
                var records = await connection.QueryAsync<TX4Model>(sql);

                return Json(new { success = true, data = records });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        [HttpGet("api/tx4/get/{id}")]
        public async Task<IActionResult> GetTX4ById(int id)
        {
            try
            {
                using var connection = new SqlConnection(_connectionString);
                var sql = "SELECT * FROM [Quartz].[dbo].[TX4] WHERE Id = @Id";
                var record = await connection.QueryFirstOrDefaultAsync<TX4Model>(sql, new { Id = id });

                if (record == null)
                    return Json(new { success = false, message = "Record not found" });

                return Json(new { success = true, data = record });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        [HttpPost("api/tx4/create")]
        public async Task<IActionResult> CreateTX4([FromBody] TX4Model model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return Json(new { success = false, errors = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage)) });

                using var connection = new SqlConnection(_connectionString);

                // 1. 先插入 TX4 记录
                var sql = @"INSERT INTO [Quartz].[dbo].[TX4] 
                    ([CompanyName], [YearEnd], [SSMsubmissionDate], [DateSOff], 
                     [DateReceiveFrSecDept], [IRBpenalties], [AppealDate], [PaymentDate], 
                     [NoteRemark], [AccWkDone], [FormCsubmitDate], [FormEsubmitDate], 
                     [InvoiceDate], [AmountRM], [ClientCopySent], [JobCompletedDate])
                    VALUES 
                    (@CompanyName, @YearEnd, @SSMsubmissionDate, @DateSOff, 
                     @DateReceiveFrSecDept, @IRBpenalties, @AppealDate, @PaymentDate, 
                     @NoteRemark, @AccWkDone, @FormCsubmitDate, @FormEsubmitDate, 
                     @InvoiceDate, @AmountRM, @ClientCopySent, @JobCompletedDate);
                    SELECT CAST(SCOPE_IDENTITY() as int);";

                var id = await connection.ExecuteScalarAsync<int>(sql, model);

                // 2. 检查并创建/更新 S16 记录
                await CreateOrUpdateS16Record(connection, model);

                return Json(new { success = true, id = id, data = model });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        [HttpPut("api/tx4/update")]
        public async Task<IActionResult> UpdateTX4([FromBody] TX4Model model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return Json(new { success = false, errors = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage)) });

                using var connection = new SqlConnection(_connectionString);

                // 1. 更新 TX4 记录
                var sql = @"UPDATE [Quartz].[dbo].[TX4] SET 
                    [CompanyName] = @CompanyName, [YearEnd] = @YearEnd, 
                    [SSMsubmissionDate] = @SSMsubmissionDate, [DateSOff] = @DateSOff, 
                    [DateReceiveFrSecDept] = @DateReceiveFrSecDept, [IRBpenalties] = @IRBpenalties, 
                    [AppealDate] = @AppealDate, [PaymentDate] = @PaymentDate, 
                    [NoteRemark] = @NoteRemark, [AccWkDone] = @AccWkDone, 
                    [FormCsubmitDate] = @FormCsubmitDate, [FormEsubmitDate] = @FormEsubmitDate, 
                    [InvoiceDate] = @InvoiceDate, [AmountRM] = @AmountRM, 
                    [ClientCopySent] = @ClientCopySent, [JobCompletedDate] = @JobCompletedDate
                    WHERE Id = @Id";

                var affectedRows = await connection.ExecuteAsync(sql, model);
                if (affectedRows == 0)
                    return Json(new { success = false, message = "Record not found" });

                // 2. 检查并创建/更新 S16 记录
                await CreateOrUpdateS16Record(connection, model);

                return Json(new { success = true, data = model });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        [HttpDelete("api/tx4/delete/{id}")]
        public async Task<IActionResult> DeleteTX4(int id)
        {
            try
            {
                using var connection = new SqlConnection(_connectionString);
                var sql = "DELETE FROM [Quartz].[dbo].[TX4] WHERE Id = @Id";
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

        // 创建或更新 S16 记录的辅助方法
        private async Task CreateOrUpdateS16Record(SqlConnection connection, TX4Model tx4Model)
        {
            // 检查 S16 中是否已存在该公司记录
            var checkSql = "SELECT COUNT(*) FROM [Quartz].[dbo].[S16] WHERE CompanyName = @CompanyName";
            var exists = await connection.ExecuteScalarAsync<int>(checkSql, new { tx4Model.CompanyName });

            if (exists > 0)
            {
                // 更新现有 S16 记录
                var updateSql = @"UPDATE [Quartz].[dbo].[S16] SET 
                         [SSMsubmitDate] = @SSMsubmitDate,
                         [SSMstrikeoffDate] = @SSMstrikeoffDate,
                         [DatePassToTaxDept] = @DatePassToTaxDept,
                         [FormCSubmitDate] = @FormCSubmitDate
                         WHERE CompanyName = @CompanyName";

                await connection.ExecuteAsync(updateSql, new
                {
                    SSMsubmitDate = tx4Model.SSMsubmissionDate,
                    SSMstrikeoffDate = tx4Model.DateSOff,
                    DatePassToTaxDept = tx4Model.DateReceiveFrSecDept,
                    FormCSubmitDate = tx4Model.FormCsubmitDate,
                    CompanyName = tx4Model.CompanyName
                });
            }
            else
            {
                // 创建新的 S16 记录
                var insertSql = @"INSERT INTO [Quartz].[dbo].[S16] 
                         ([CompanyName], [YearEnd], [SSMsubmitDate], [SSMstrikeoffDate], 
                          [DatePassToTaxDept], [FormCSubmitDate], [JobCompleted])
                         VALUES 
                         (@CompanyName, @YearEnd, @SSMsubmitDate, @SSMstrikeoffDate, 
                          @DatePassToTaxDept, @FormCSubmitDate, @JobCompleted)";

                await connection.ExecuteAsync(insertSql, new
                {
                    CompanyName = tx4Model.CompanyName,
                    YearEnd = tx4Model.YearEnd,
                    SSMsubmitDate = tx4Model.SSMsubmissionDate,
                    SSMstrikeoffDate = tx4Model.DateSOff,
                    DatePassToTaxDept = tx4Model.DateReceiveFrSecDept,
                    FormCSubmitDate = tx4Model.FormCsubmitDate,
                    JobCompleted = "In Progress" // 默认值
                });
            }
        }
        #endregion    
    }
}