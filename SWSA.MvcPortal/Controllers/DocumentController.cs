using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using SWSA.MvcPortal.Commons.Constants;
using SWSA.MvcPortal.Commons.Enums;
using SWSA.MvcPortal.Data;
using SWSA.MvcPortal.Data.Models;
using SWSA.MvcPortal.Models.Clients;
using SWSA.MvcPortal.Models.DocumentRecords;
using SWSA.MvcPortal.Services.Interfaces;
using SWSA.MvcPortal.Services.Interfaces.Clients;
using SWSA.MvcPortal.Services.Interfaces.UserAccess;
using A32a = SWSA.MvcPortal.Models.Clients.A32a;

namespace SWSA.MvcPortal.Controllers;

[Route("documents")]
public class DocumentController(
    IDocumentRecordService service,
    IClientService _clientService,
    IUserService userService,
    IConfiguration configuration
    ) : BaseController
{
    private readonly string _connectionString = configuration.GetConnectionString("SwsaConntection");

    #region Page/View Methods
    [Route("audit-dept")]
    public async Task<IActionResult> AuditDepartment()
    {
        var documents = await service.GetDocumentRecordsByDepartment(DepartmentType.Audit);
        //Only Sdn Bhd has audit document;
        var clients = await _clientService.GetClientSelectionVM([ClientType.SdnBhd]);
        var vm = new DocumentRecordAuditDeptPageVM(clients, documents);
        return View(vm);
    }

    [Route("account-dept")]
    public async Task<IActionResult> AccountDepartment()
    {
        var documents = await service.GetDocumentRecordsByDepartment(DepartmentType.Account);
        return View(documents);
    }

    [Route("doct-audA31A")]
    public async Task<IActionResult> A31ADocControl()
    {
        return View();
    }

    [Route("doct-audA31B")]
    public async Task<IActionResult> A31BDocControl()
    {
        return View();
    }

    [Route("doct-accA32A")]
    public async Task<IActionResult> A32AcorrespondanceRecord()
    {
        try
        {
            Console.WriteLine("=== Loading A32A Page with Dapper ===");

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                Console.WriteLine("✅ Database connection successful");

                var sql = "SELECT * FROM [Quartz].[dbo].[A32A] ORDER BY Id DESC";
                var records = await connection.QueryAsync<A32a>(sql);

                Console.WriteLine($"✅ Successfully loaded {records.Count()} records for page");
                return View(records);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"❌ Error in A32AcorrespondanceRecord: {ex.Message}");
            Console.WriteLine($"Stack Trace: {ex.StackTrace}");

            // 返回空列表而不是抛出异常
            return View(new List<A32a>());
        }
    }

    [Route("doct-accA32B")]
    public async Task<IActionResult> A32BForm()
    {
        return View();
    }

    [Route("doct-taxA33A")]
    public async Task<IActionResult> A33ATaxDeptcorrespondanceRecord()
    {
        return View();
    }

    [Route("doct-taxA33B")]
    public async Task<IActionResult> A33BSdnBhdTaxAudit()
    {
        return View();
    }
    #endregion

    #region Client API Methods
    [HttpGet("get-clients")]
    public async Task<IActionResult> GetClients()
    {
        try
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                // 先检查连接是否成功
                await connection.OpenAsync();

                // 先查询所有客户端，不限制 IsActive
                var testSql = "SELECT COUNT(*) FROM [Quartz].[dbo].[Clients]";
                var count = await connection.ExecuteScalarAsync<int>(testSql);
                Console.WriteLine($"Total clients in database: {count}");

                // 查询所有客户端，包括不活跃的
                var sql = "SELECT Id, Name, YearEndMonth FROM [Quartz].[dbo].[Clients] ORDER BY Name";
                var clients = await connection.QueryAsync<ClientModel>(sql);

                Console.WriteLine($"Clients returned: {clients.Count()}");
                foreach (var client in clients)
                {
                    Console.WriteLine($"Client: {client.Id} - {client.Name} - IsActive: {client.IsActive}");
                }

                return Json(new { success = true, data = clients });
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error in GetClients: {ex.Message}");
            Console.WriteLine($"Stack Trace: {ex.StackTrace}");
            return Json(new { success = false, message = ex.Message, data = new List<ClientModel>() });
        }
    }

    [HttpGet("get-client-details/{id}")]
    public async Task<IActionResult> GetClientDetails(int id)
    {
        try
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var sql = "SELECT Id, Name, YearEndMonth FROM [Quartz].[dbo].[Clients] WHERE Id = @Id";
                var client = await connection.QueryFirstOrDefaultAsync<ClientModel>(sql, new { Id = id });

                if (client == null)
                    return Json(new { success = false, message = "Client not found" });

                return Json(new { success = true, data = client });
            }
        }
        catch (Exception ex)
        {
            return Json(new { success = false, message = ex.Message });
        }
    }
    #endregion

    #region Private Helper Methods
    private async Task CreateA31BFromA31A(A31AModel a31aModel)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            var sql = @"INSERT INTO [Quartz].[dbo].[A31B] 
                        ([Clients], [YearEnded], [CoStatus], [DateDocFr], 
                         [DateReceived], [NoOfBoxBag], [ByWhom], [UploadLetter], 
                         [Remark], [Date], [NoOfbox], [ByWhom2], [UploadLetter2], [Remark2])
                        VALUES 
                        (@Clients, @YearEnded, @CoStatus, @DateDocFr, 
                         @DateReceived, @NoOfBoxBag, @ByWhom, @UploadLetter, 
                         @Remark, @Date, @NoOfbox, @ByWhom2, @UploadLetter2, @Remark2)";

            var a31bModel = new A31BModel
            {
                Clients = a31aModel.Client,
                YearEnded = a31aModel.YearEnded,
                CoStatus = "Active", // 默认状态
                DateDocFr = a31aModel.DateSendToAD,
                // 其他字段可以根据需要从 A31A 复制或留空
                DateReceived = null,
                NoOfBoxBag = null,
                ByWhom = null,
                UploadLetter = null,
                Remark = "Auto-created from A31A",
                Date = null,
                NoOfbox = null,
                ByWhom2 = null,
                UploadLetter2 = null,
                Remark2 = null
            };

            await connection.ExecuteAsync(sql, a31bModel);
        }
    }

    private async Task<string> GetClientNameById(int? clientId)
    {
        if (clientId == null) return string.Empty;

        try
        {
            var clientTableName = await GetClientTableName();
            if (clientTableName == null) return string.Empty;

            using (var connection = new SqlConnection(_connectionString))
            {
                var sql = $"SELECT Name FROM [Quartz].[dbo].[{clientTableName}] WHERE Id = @Id";
                return await connection.QueryFirstOrDefaultAsync<string>(sql, new { Id = clientId });
            }
        }
        catch
        {
            return string.Empty;
        }
    }

    private async Task<string> GetClientTableName()
    {
        try
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var tableNames = new[] { "Client", "Clients", "client", "clients" };

                foreach (var tableName in tableNames)
                {
                    try
                    {
                        var testSql = $"SELECT TOP 1 Id FROM [Quartz].[dbo].[{tableName}]";
                        await connection.ExecuteScalarAsync<int>(testSql);
                        return tableName;
                    }
                    catch
                    {
                        continue;
                    }
                }
                return null;
            }
        }
        catch
        {
            return null;
        }
    }
    #endregion



    //Table Controller Methods

    #region A31A API Methods
    [HttpGet("get-a31a-records")]
    public async Task<IActionResult> GetA31ARecords()
    {
        try
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var sql = "SELECT * FROM [Quartz].[dbo].[A31A] ORDER BY Id DESC";
                var records = await connection.QueryAsync<A31AModel>(sql);
                return Json(new { success = true, data = records });
            }
        }
        catch (Exception ex)
        {
            return Json(new { success = false, message = ex.Message });
        }
    }

    [HttpGet("get-a31a-record/{id}")]
    public async Task<IActionResult> GetA31ARecord(int id)
    {
        try
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var sql = "SELECT * FROM [Quartz].[dbo].[A31A] WHERE Id = @Id";
                var record = await connection.QueryFirstOrDefaultAsync<A31AModel>(sql, new { Id = id });

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

    [HttpPost("create-a31a")]
    public async Task<IActionResult> CreateA31A([FromBody] A31AModel model)
    {
        try
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var sql = @"INSERT INTO [Quartz].[dbo].[A31A] 
                        ([Client], [YearEnded], [DateReceived], [NoOfBagBox], 
                         [ByWhom], [UploadLetter], [Remark], [DateSendToAD], 
                         [Date], [NoOfBoxBag], [ByWhoam2], [UploadLetter2], [Remark2])
                        VALUES 
                        (@Client, @YearEnded, @DateReceived, @NoOfBagBox, 
                         @ByWhom, @UploadLetter, @Remark, @DateSendToAD, 
                         @Date, @NoOfBoxBag, @ByWhoam2, @UploadLetter2, @Remark2);
                        SELECT SCOPE_IDENTITY();";

                var id = await connection.ExecuteScalarAsync<int>(sql, model);

                // 如果填写了 DateSendToAD，则创建 A31B 记录
                if (!string.IsNullOrEmpty(model.DateSendToAD))
                {
                    await CreateA31BFromA31A(model);
                }

                return Json(new { success = true, id = id });
            }
        }
        catch (Exception ex)
        {
            return Json(new { success = false, message = ex.Message });
        }
    }

    [HttpPut("update-a31a")]
    public async Task<IActionResult> UpdateA31A([FromBody] A31AModel model)
    {
        try
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var sql = @"UPDATE [Quartz].[dbo].[A31A] SET 
                        [Client] = @Client, [YearEnded] = @YearEnded, [DateReceived] = @DateReceived, 
                        [NoOfBagBox] = @NoOfBagBox, [ByWhom] = @ByWhom, [UploadLetter] = @UploadLetter, 
                        [Remark] = @Remark, [DateSendToAD] = @DateSendToAD, [Date] = @Date, 
                        [NoOfBoxBag] = @NoOfBoxBag, [ByWhoam2] = @ByWhoam2, [UploadLetter2] = @UploadLetter2, 
                        [Remark2] = @Remark2 
                        WHERE Id = @Id";

                await connection.ExecuteAsync(sql, model);
                return Json(new { success = true });
            }
        }
        catch (Exception ex)
        {
            return Json(new { success = false, message = ex.Message });
        }
    }
    #endregion

    #region A31B API Methods
    [HttpGet("get-a31b-records")]
    public async Task<IActionResult> GetA31BRecords()
    {
        try
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var sql = "SELECT * FROM [Quartz].[dbo].[A31B] ORDER BY Id DESC";
                var records = await connection.QueryAsync<A31BModel>(sql);
                return Json(new { success = true, data = records });
            }
        }
        catch (Exception ex)
        {
            return Json(new { success = false, message = ex.Message });
        }
    }

    [HttpGet("get-a31b-record/{id}")]
    public async Task<IActionResult> GetA31BRecord(int id)
    {
        try
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var sql = "SELECT * FROM [Quartz].[dbo].[A31B] WHERE Id = @Id";
                var record = await connection.QueryFirstOrDefaultAsync<A31BModel>(sql, new { Id = id });

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

    [HttpPost("create-a31b")]
    public async Task<IActionResult> CreateA31B([FromBody] A31BModel model)
    {
        try
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var sql = @"INSERT INTO [Quartz].[dbo].[A31B] 
                        ([Clients], [YearEnded], [CoStatus], [DateDocFr], 
                         [DateReceived], [NoOfBoxBag], [ByWhom], [UploadLetter], 
                         [Remark], [Date], [NoOfbox], [ByWhom2], [UploadLetter2], [Remark2])
                        VALUES 
                        (@Clients, @YearEnded, @CoStatus, @DateDocFr, 
                         @DateReceived, @NoOfBoxBag, @ByWhom, @UploadLetter, 
                         @Remark, @Date, @NoOfbox, @ByWhom2, @UploadLetter2, @Remark2);
                        SELECT SCOPE_IDENTITY();";

                var id = await connection.ExecuteScalarAsync<int>(sql, model);
                return Json(new { success = true, id = id });
            }
        }
        catch (Exception ex)
        {
            return Json(new { success = false, message = ex.Message });
        }
    }

    [HttpPut("update-a31b")]
    public async Task<IActionResult> UpdateA31B([FromBody] A31BModel model)
    {
        try
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var sql = @"UPDATE [Quartz].[dbo].[A31B] SET 
                        [Clients] = @Clients, [YearEnded] = @YearEnded, [CoStatus] = @CoStatus, 
                        [DateDocFr] = @DateDocFr, [DateReceived] = @DateReceived, [NoOfBoxBag] = @NoOfBoxBag, 
                        [ByWhom] = @ByWhom, [UploadLetter] = @UploadLetter, [Remark] = @Remark, 
                        [Date] = @Date, [NoOfbox] = @NoOfbox, [ByWhom2] = @ByWhom2, 
                        [UploadLetter2] = @UploadLetter2, [Remark2] = @Remark2 
                        WHERE Id = @Id";

                await connection.ExecuteAsync(sql, model);
                return Json(new { success = true });
            }
        }
        catch (Exception ex)
        {
            return Json(new { success = false, message = ex.Message });
        }
    }

    [HttpDelete("delete-a31b/{id}")]
    public async Task<IActionResult> DeleteA31B(int id)
    {
        try
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var sql = "DELETE FROM [Quartz].[dbo].[A31B] WHERE Id = @Id";
                await connection.ExecuteAsync(sql, new { Id = id });
                return Json(new { success = true });
            }
        }
        catch (Exception ex)
        {
            return Json(new { success = false, message = ex.Message });
        }
    }
    #endregion

    #region A32A API Methods
    [HttpGet("get-a32a-records")]
    public async Task<IActionResult> GetA32ARecords()
    {
        try
        {
            Console.WriteLine("=== Starting GetA32ARecords with Dapper ===");

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                Console.WriteLine("✅ Database connection successful");

                var sql = "SELECT * FROM [Quartz].[dbo].[A32A] ORDER BY Id DESC";
                var records = await connection.QueryAsync<A32a>(sql);

                Console.WriteLine($"✅ Successfully retrieved {records.Count()} records");
                return Json(new { success = true, data = records });
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"❌ Error in GetA32ARecords: {ex.Message}");
            Console.WriteLine($"Stack Trace: {ex.StackTrace}");

            return Json(new
            {
                success = false,
                message = ex.Message,
                detailed = ex.StackTrace
            });
        }
    }

    [HttpGet("get-a32a-record/{id}")]
    public async Task<IActionResult> GetA32ARecord(int id)
    {
        try
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var sql = "SELECT * FROM [Quartz].[dbo].[A32A] WHERE Id = @Id";
                var record = await connection.QueryFirstOrDefaultAsync<A32a>(sql, new { Id = id });

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

    [HttpPost("create-a32a")]
    public async Task<IActionResult> CreateA32A([FromBody] A32a model)
    {
        try
        {
            if (!ModelState.IsValid)
                return Json(new { success = false, errors = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage)) });

            using (var connection = new SqlConnection(_connectionString))
            {
                var sql = @"INSERT INTO [Quartz].[dbo].[A32A] 
                        ([CaseNo], [DateReceived], [TypeIncoming], [Client], 
                         [YearAssessment], [Details], [Date], [BriefDescritions], 
                         [PIC], [Remark], [DoneOn])
                        VALUES 
                        (@CaseNo, @DateReceived, @TypeIncoming, @Client, 
                         @YearAssessment, @Details, @Date, @BriefDescritions, 
                         @Pic, @Remark, @DoneOn);
                        SELECT CAST(SCOPE_IDENTITY() as int);";

                var id = await connection.ExecuteScalarAsync<int>(sql, new
                {
                    model.CaseNo,
                    model.DateReceived,
                    model.TypeIncoming,
                    model.Client,
                    model.YearAssessment,
                    model.Details,
                    model.Date,
                    model.BriefDescritions,
                    model.Pic,
                    model.Remark,
                    model.DoneOn
                });

                return Json(new { success = true, id = id, data = model });
            }
        }
        catch (Exception ex)
        {
            return Json(new { success = false, message = ex.Message });
        }
    }

    [HttpPut("update-a32a")]
    public async Task<IActionResult> UpdateA32A([FromBody] A32a model)
    {
        try
        {
            if (!ModelState.IsValid)
                return Json(new { success = false, errors = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage)) });

            using (var connection = new SqlConnection(_connectionString))
            {
                var sql = @"UPDATE [Quartz].[dbo].[A32A] SET 
                        [CaseNo] = @CaseNo, [DateReceived] = @DateReceived, 
                        [TypeIncoming] = @TypeIncoming, [Client] = @Client, 
                        [YearAssessment] = @YearAssessment, [Details] = @Details, 
                        [Date] = @Date, [BriefDescritions] = @BriefDescritions, 
                        [PIC] = @Pic, [Remark] = @Remark, [DoneOn] = @DoneOn
                        WHERE Id = @Id";

                var affectedRows = await connection.ExecuteAsync(sql, new
                {
                    model.Id,
                    model.CaseNo,
                    model.DateReceived,
                    model.TypeIncoming,
                    model.Client,
                    model.YearAssessment,
                    model.Details,
                    model.Date,
                    model.BriefDescritions,
                    model.Pic,
                    model.Remark,
                    model.DoneOn
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

    [HttpDelete("delete-a32a/{id}")]
    public async Task<IActionResult> DeleteA32A(int id)
    {
        try
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var sql = "DELETE FROM [Quartz].[dbo].[A32A] WHERE Id = @Id";
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

    #region A32B API Methods
    [HttpGet("get-a32b-records")]
    public async Task<IActionResult> GetA32BRecords()
    {
        try
        {
            Console.WriteLine("=== Starting GetA32BRecords with Dapper ===");

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                Console.WriteLine("✅ Database connection successful");

                var sql = @"SELECT 
                        Id,
                        CaseNo,
                        DateReceived,
                        Client,
                        OfficerInCharge,
                        TelExtension,
                        YearAssessment,
                        DateIRBemailLetter,
                        DetailsCorrepondence,
                        PIC,
                        Date,
                        Note
                    FROM [Quartz].[dbo].[A32B] 
                    ORDER BY Id DESC";

                var records = await connection.QueryAsync<A32BModel>(sql);

                Console.WriteLine($"✅ Successfully retrieved {records.Count()} records");
                return Json(new { success = true, data = records });
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"❌ Error in GetA32BRecords: {ex.Message}");
            Console.WriteLine($"Stack Trace: {ex.StackTrace}");

            return Json(new
            {
                success = false,
                message = ex.Message,
                detailed = ex.StackTrace
            });
        }
    }

    [HttpGet("get-a32b-record/{id}")]
    public async Task<IActionResult> GetA32BRecord(int id)
    {
        try
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var sql = @"SELECT 
                        Id,
                        CaseNo,
                        DateReceived,
                        Client,
                        OfficerInCharge,
                        TelExtension,
                        YearAssessment,
                        DateIRBemailLetter,
                        DetailsCorrepondence,
                        PIC,
                        Date,
                        Note
                    FROM [Quartz].[dbo].[A32B] 
                    WHERE Id = @Id";

                var record = await connection.QueryFirstOrDefaultAsync<A32BModel>(sql, new { Id = id });

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

    [HttpPost("create-a32b")]
    public async Task<IActionResult> CreateA32B([FromBody] A32BModel model)
    {
        try
        {
            if (!ModelState.IsValid)
                return Json(new { success = false, errors = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage)) });

            using (var connection = new SqlConnection(_connectionString))
            {
                var sql = @"INSERT INTO [Quartz].[dbo].[A32B] 
                    ([CaseNo], [DateReceived], [Client], [OfficerInCharge], 
                     [TelExtension], [YearAssessment], [DateIRBemailLetter], [DetailsCorrepondence], 
                     [PIC], [Date], [Note])
                    VALUES 
                    (@CaseNo, @DateReceived, @Client, @OfficerInCharge, 
                     @TelExtension, @YearAssessment, @DateIRBemailLetter, @DetailsCorrepondence, 
                     @PIC, @Date, @Note);
                    SELECT CAST(SCOPE_IDENTITY() as int);";

                var id = await connection.ExecuteScalarAsync<int>(sql, model);

                return Json(new { success = true, id = id, data = model });
            }
        }
        catch (Exception ex)
        {
            return Json(new { success = false, message = ex.Message });
        }
    }

    [HttpPut("update-a32b")]
    public async Task<IActionResult> UpdateA32B([FromBody] A32BModel model)
    {
        try
        {
            if (!ModelState.IsValid)
                return Json(new { success = false, errors = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage)) });

            using (var connection = new SqlConnection(_connectionString))
            {
                var sql = @"UPDATE [Quartz].[dbo].[A32B] SET 
                    [CaseNo] = @CaseNo, 
                    [DateReceived] = @DateReceived, 
                    [Client] = @Client, 
                    [OfficerInCharge] = @OfficerInCharge, 
                    [TelExtension] = @TelExtension, 
                    [YearAssessment] = @YearAssessment, 
                    [DateIRBemailLetter] = @DateIRBemailLetter, 
                    [DetailsCorrepondence] = @DetailsCorrepondence, 
                    [PIC] = @PIC, 
                    [Date] = @Date, 
                    [Note] = @Note
                    WHERE Id = @Id";

                var affectedRows = await connection.ExecuteAsync(sql, model);

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

    [HttpDelete("delete-a32b/{id}")]
    public async Task<IActionResult> DeleteA32B(int id)
    {
        try
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var sql = "DELETE FROM [Quartz].[dbo].[A32B] WHERE Id = @Id";
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

    #region A33A API Methods
    [HttpGet("get-a33a-records")]
    public async Task<IActionResult> GetA33ARecords()
    {
        try
        {
            Console.WriteLine("=== Starting GetA33ARecords with Dapper ===");

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                Console.WriteLine("✅ Database connection successful");

                var sql = @"SELECT 
                    Id,
                    CaseNo,
                    DateReceived,
                    TypeIncoming,
                    Client,
                    YearAssessment,
                    Details,
                    Date,
                    BriefDescritions,
                    PIC,
                    Remark,
                    DoneOn
                FROM [Quartz].[dbo].[A33A] 
                ORDER BY Id DESC";

                var records = await connection.QueryAsync<A33AModel>(sql);

                Console.WriteLine($"✅ Successfully retrieved {records.Count()} records");
                return Json(new { success = true, data = records });
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"❌ Error in GetA33ARecords: {ex.Message}");
            Console.WriteLine($"Stack Trace: {ex.StackTrace}");

            return Json(new
            {
                success = false,
                message = ex.Message,
                detailed = ex.StackTrace
            });
        }
    }

    [HttpGet("get-a33a-record/{id}")]
    public async Task<IActionResult> GetA33ARecord(int id)
    {
        try
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var sql = @"SELECT 
                    Id,
                    CaseNo,
                    DateReceived,
                    TypeIncoming,
                    Client,
                    YearAssessment,
                    Details,
                    Date,
                    BriefDescritions,
                    PIC,
                    Remark,
                    DoneOn
                FROM [Quartz].[dbo].[A33A] 
                WHERE Id = @Id";

                var record = await connection.QueryFirstOrDefaultAsync<A33AModel>(sql, new { Id = id });

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

    [HttpPost("create-a33a")]
    public async Task<IActionResult> CreateA33A([FromBody] A33AModel model)
    {
        try
        {
            if (!ModelState.IsValid)
                return Json(new { success = false, errors = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage)) });

            using (var connection = new SqlConnection(_connectionString))
            {
                var sql = @"INSERT INTO [Quartz].[dbo].[A33A] 
                    ([CaseNo], [DateReceived], [TypeIncoming], [Client], 
                     [YearAssessment], [Details], [Date], [BriefDescritions], 
                     [PIC], [Remark], [DoneOn])
                    VALUES 
                    (@CaseNo, @DateReceived, @TypeIncoming, @Client, 
                     @YearAssessment, @Details, @Date, @BriefDescritions, 
                     @PIC, @Remark, @DoneOn);
                    SELECT CAST(SCOPE_IDENTITY() as int);";

                var id = await connection.ExecuteScalarAsync<int>(sql, model);

                return Json(new { success = true, id = id, data = model });
            }
        }
        catch (Exception ex)
        {
            return Json(new { success = false, message = ex.Message });
        }
    }

    [HttpPut("update-a33a")]
    public async Task<IActionResult> UpdateA33A([FromBody] A33AModel model)
    {
        try
        {
            if (!ModelState.IsValid)
                return Json(new { success = false, errors = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage)) });

            using (var connection = new SqlConnection(_connectionString))
            {
                var sql = @"UPDATE [Quartz].[dbo].[A33A] SET 
                    [CaseNo] = @CaseNo, 
                    [DateReceived] = @DateReceived, 
                    [TypeIncoming] = @TypeIncoming, 
                    [Client] = @Client, 
                    [YearAssessment] = @YearAssessment, 
                    [Details] = @Details, 
                    [Date] = @Date, 
                    [BriefDescritions] = @BriefDescritions, 
                    [PIC] = @PIC, 
                    [Remark] = @Remark, 
                    [DoneOn] = @DoneOn
                    WHERE Id = @Id";

                var affectedRows = await connection.ExecuteAsync(sql, model);

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

    [HttpDelete("delete-a33a/{id}")]
    public async Task<IActionResult> DeleteA33A(int id)
    {
        try
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var sql = "DELETE FROM [Quartz].[dbo].[A33A] WHERE Id = @Id";
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

    #region A33B API Methods
    [HttpGet("get-a33b-records")]
    public async Task<IActionResult> GetA33BRecords()
    {
        try
        {
            Console.WriteLine("=== Starting GetA33BRecords with Dapper ===");

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                Console.WriteLine("✅ Database connection successful");

                var sql = @"SELECT 
                    Id,
                    CaseNo,
                    DateReceived,
                    Client,
                    OfficerInchrage,
                    TelExtension,
                    YearAssessment,
                    DateIRBemailLetter,
                    DetailsCorrepondence,
                    PIC,
                    Date,
                    Note
                FROM [Quartz].[dbo].[A33B] 
                ORDER BY Id DESC";

                var records = await connection.QueryAsync<A33BModel>(sql);

                Console.WriteLine($"✅ Successfully retrieved {records.Count()} records");
                return Json(new { success = true, data = records });
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"❌ Error in GetA33BRecords: {ex.Message}");
            Console.WriteLine($"Stack Trace: {ex.StackTrace}");

            return Json(new
            {
                success = false,
                message = ex.Message,
                detailed = ex.StackTrace
            });
        }
    }

    [HttpGet("get-a33b-record/{id}")]
    public async Task<IActionResult> GetA33BRecord(int id)
    {
        try
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var sql = @"SELECT 
                    Id,
                    CaseNo,
                    DateReceived,
                    Client,
                    OfficerInchrage,
                    TelExtension,
                    YearAssessment,
                    DateIRBemailLetter,
                    DetailsCorrepondence,
                    PIC,
                    Date,
                    Note
                FROM [Quartz].[dbo].[A33B] 
                WHERE Id = @Id";

                var record = await connection.QueryFirstOrDefaultAsync<A33BModel>(sql, new { Id = id });

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

    [HttpPost("create-a33b")]
    public async Task<IActionResult> CreateA33B([FromBody] A33BModel model)
    {
        try
        {
            if (!ModelState.IsValid)
                return Json(new { success = false, errors = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage)) });

            using (var connection = new SqlConnection(_connectionString))
            {
                var sql = @"INSERT INTO [Quartz].[dbo].[A33B] 
                    ([CaseNo], [DateReceived], [Client], [OfficerInchrage], 
                     [TelExtension], [YearAssessment], [DateIRBemailLetter], 
                     [DetailsCorrepondence], [PIC], [Date], [Note])
                    VALUES 
                    (@CaseNo, @DateReceived, @Client, @OfficerInchrage, 
                     @TelExtension, @YearAssessment, @DateIRBemailLetter, 
                     @DetailsCorrepondence, @PIC, @Date, @Note);
                    SELECT CAST(SCOPE_IDENTITY() as int);";

                var id = await connection.ExecuteScalarAsync<int>(sql, model);

                return Json(new { success = true, id = id, data = model });
            }
        }
        catch (Exception ex)
        {
            return Json(new { success = false, message = ex.Message });
        }
    }

    [HttpPut("update-a33b")]
    public async Task<IActionResult> UpdateA33B([FromBody] A33BModel model)
    {
        try
        {
            if (!ModelState.IsValid)
                return Json(new { success = false, errors = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage)) });

            using (var connection = new SqlConnection(_connectionString))
            {
                var sql = @"UPDATE [Quartz].[dbo].[A33B] SET 
                    [CaseNo] = @CaseNo, 
                    [DateReceived] = @DateReceived, 
                    [Client] = @Client, 
                    [OfficerInchrage] = @OfficerInchrage, 
                    [TelExtension] = @TelExtension, 
                    [YearAssessment] = @YearAssessment, 
                    [DateIRBemailLetter] = @DateIRBemailLetter, 
                    [DetailsCorrepondence] = @DetailsCorrepondence, 
                    [PIC] = @PIC, 
                    [Date] = @Date, 
                    [Note] = @Note
                    WHERE Id = @Id";

                var affectedRows = await connection.ExecuteAsync(sql, model);

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

    [HttpDelete("delete-a33b/{id}")]
    public async Task<IActionResult> DeleteA33B(int id)
    {
        try
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var sql = "DELETE FROM [Quartz].[dbo].[A33B] WHERE Id = @Id";
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