using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using SWSA.MvcPortal.Entities.Models;
using SWSA.MvcPortal.Persistence;

namespace SWSA.MvcPortal.Controllers.Group
{
    public class GroupController : Controller
    {
        private readonly string _connectionString;
        private readonly IConfiguration _configuration;
        private readonly AppDbContext _db;

        public GroupController(IConfiguration configuration, AppDbContext db)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("SwsaConntection");
            _db = db;
        }

        public IActionResult GroupList()
        {
            return View();
        }

        public IActionResult ReferralList()
        {
            return View();
        }

        #region API GroupList

        public class GroupDto
        {
            public int Id { get; set; }
            public string GroupName { get; set; } = string.Empty;
            public bool IsActive { get; set; }
            public DateTime CreatedAt { get; set; }
        }

        public class GroupCreateUpdateDto
        {
            public string GroupName { get; set; } = string.Empty;
            public bool IsActive { get; set; } = true;
        }

        // 专门给 Update 用，多一个 Id
        public class GroupUpdateDto : GroupCreateUpdateDto
        {
            public int Id { get; set; }
        }

        // === 获取全部（不含 ClientCount，用不到可以留着）===
        [HttpGet("api/groups/get-all")]
        public async Task<IActionResult> GetAllGroups()
        {
            try
            {
                using var conn = new SqlConnection(_connectionString);
                var sql = @"SELECT Id, GroupName, IsActive, CreatedAt
                    FROM dbo.Groups
                    ORDER BY IsActive DESC, GroupName ASC;";
                var rows = await conn.QueryAsync<GroupDto>(sql);
                return Json(new { success = true, data = rows });
            }
            catch (Exception ex) { return Json(new { success = false, message = ex.Message }); }
        }

        // === 获取全部（含 ClientCount，用在 DataTable）===
        [HttpGet("api/groups/with-counts")]
        public async Task<IActionResult> GetGroupsWithCounts()
        {
            try
            {
                using var conn = new SqlConnection(_connectionString);
                var sql = @"
SELECT g.Id, g.GroupName, g.IsActive, g.CreatedAt, COUNT(c.Id) AS ClientCount
FROM dbo.Groups g
LEFT JOIN dbo.Clients c ON c.GroupId = g.Id
GROUP BY g.Id, g.GroupName, g.IsActive, g.CreatedAt
ORDER BY g.IsActive DESC, g.GroupName ASC;";
                var rows = await conn.QueryAsync(sql);
                return Json(new { success = true, data = rows });
            }
            catch (Exception ex) { return Json(new { success = false, message = ex.Message }); }
        }

        // === 下拉选项（仅启用）===
        [HttpGet("api/groups/options")]
        public async Task<IActionResult> GetGroupOptions()
        {
            try
            {
                using var conn = new SqlConnection(_connectionString);
                var sql = @"SELECT Id AS value, GroupName AS text
                    FROM dbo.Groups
                    WHERE IsActive = 1
                    ORDER BY GroupName ASC;";
                var rows = await conn.QueryAsync(sql);
                return Json(new { success = true, data = rows });
            }
            catch (Exception ex) { return Json(new { success = false, message = ex.Message }); }
        }

        // === 单条读取：api/groups/get/1 ===
        [HttpGet("api/groups/get/{id}")]
        public async Task<IActionResult> GetGroupById(int id)
        {
            try
            {
                using var conn = new SqlConnection(_connectionString);
                // 修复：使用正确的表名，和其他查询保持一致
                var sql = @"SELECT Id, GroupName, IsActive, CreatedAt
                    FROM dbo.Groups
                    WHERE Id = @Id;";
                var row = await conn.QueryFirstOrDefaultAsync<GroupDto>(sql, new { Id = id });

                if (row == null)
                    return Json(new { success = false, message = "Group not found." });

                return Json(new { success = true, data = row });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        // === 新增：/api/groups/create ===
        [HttpPost("api/groups/create")]
        public async Task<IActionResult> CreateGroup([FromBody] GroupCreateUpdateDto model)
        {
            try
            {
                if (model == null || string.IsNullOrWhiteSpace(model.GroupName))
                    return Json(new { success = false, message = "Group name is required." });

                using var conn = new SqlConnection(_connectionString);

                var exists = await conn.ExecuteScalarAsync<int>(
                    "SELECT COUNT(1) FROM dbo.Groups WHERE GroupName = @GroupName;",
                    new { model.GroupName });
                if (exists > 0)
                    return Json(new { success = false, message = "Group name already exists." });

                var sql = @"
INSERT INTO dbo.Groups (GroupName, IsActive, CreatedAt)
VALUES (@GroupName, @IsActive, SYSDATETIME());
SELECT CAST(SCOPE_IDENTITY() AS INT);";

                var newId = await conn.ExecuteScalarAsync<int>(sql, new { model.GroupName, model.IsActive });

                return Json(new { success = true, id = newId });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        // === 更新：/api/groups/update ===
        [HttpPost("api/groups/update")]
        public async Task<IActionResult> UpdateGroup([FromBody] GroupUpdateDto model)
        {
            try
            {
                if (model == null || model.Id <= 0)
                    return Json(new { success = false, message = "Invalid id." });

                if (string.IsNullOrWhiteSpace(model.GroupName))
                    return Json(new { success = false, message = "Group name is required." });

                using var conn = new SqlConnection(_connectionString);

                // 名称唯一性（排除自己）
                var exists = await conn.ExecuteScalarAsync<int>(
                    "SELECT COUNT(1) FROM dbo.Groups WHERE GroupName = @GroupName AND Id <> @Id;",
                    new { model.GroupName, Id = model.Id });
                if (exists > 0)
                    return Json(new { success = false, message = "Group name already exists." });

                var sql = @"UPDATE dbo.Groups
                    SET GroupName = @GroupName,
                        IsActive  = @IsActive
                    WHERE Id = @Id;";

                var affected = await conn.ExecuteAsync(sql, new { Id = model.Id, model.GroupName, model.IsActive });

                return Json(new { success = affected > 0 });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }


        // === 删除：/api/groups/delete?id=1 （如果被 Clients 用到就禁止）===
        [HttpPost("api/groups/delete")]
        public async Task<IActionResult> DeleteGroup(int id)
        {
            try
            {
                if (id <= 0) return Json(new { success = false, message = "Invalid id." });

                using var conn = new SqlConnection(_connectionString);

                var inUse = await conn.ExecuteScalarAsync<int>(
                    "SELECT COUNT(1) FROM dbo.Clients WHERE GroupId = @Id;",
                    new { Id = id });
                if (inUse > 0)
                    return Json(new { success = false, message = "This group is in use by clients and cannot be deleted." });

                var affected = await conn.ExecuteAsync("DELETE FROM dbo.Groups WHERE Id = @Id;", new { Id = id });
                return Json(new { success = affected > 0 });
            }
            catch (Exception ex) { return Json(new { success = false, message = ex.Message }); }
        }

        public class GroupClientDto
        {
            public int Id { get; set; }

            // 用别名让前端拿 companyName、registrationNumber、yearEndMonth
            public string CompanyName { get; set; } = string.Empty;
            public string? RegistrationNumber { get; set; }
            public string? YearEndMonth { get; set; }
        }


        // GET: /api/groups/{groupId}/clients
        [HttpGet("api/groups/{groupId}/clients")]
        public async Task<IActionResult> GetClientsByGroup(int groupId)
        {
            try
            {
                if (groupId <= 0)
                    return Json(new { success = false, message = "Invalid group id." });

                using var conn = new SqlConnection(_connectionString);

                var sql = @"
SELECT 
    c.Id,
    c.[Name]                 AS CompanyName,        -- ← 对应 Name
    c.[TaxIdentificationNumber] AS RegistrationNumber, -- ← 对应 TaxIdentificationNumber
    c.[YearEndMonth]         AS YearEndMonth        -- ← 对应 YearEndMonth
FROM [Quartz2].[dbo].[Clients] c
WHERE c.[GroupId] = @GroupId
ORDER BY c.[Name] ASC;";

                var rows = await conn.QueryAsync<GroupClientDto>(sql, new { GroupId = groupId });

                return Json(new { success = true, data = rows });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }
        #endregion

        #region API ReferralList

        public class ReferralCreateUpdateDto
        {
            public string ReferralName { get; set; } = string.Empty;
            public bool IsActive { get; set; } = true;
        }

        public class ReferralUpdateDto : ReferralCreateUpdateDto
        {
            public int Id { get; set; }
        }

        // === TEMP DEBUG ===
        [HttpGet("api/referrals/debug")]
        public async Task<IActionResult> DebugReferrals()
        {
            try
            {
                var efConnStr = _db.Database.GetConnectionString();
                var rawCount = await _db.Database.SqlQueryRaw<int>("SELECT COUNT(*) AS [Value] FROM dbo.Referrals").FirstOrDefaultAsync();
                var efRows = await _db.Referrals.CountAsync();
                return Json(new
                {
                    efConnectionString = efConnStr,
                    dapperConnectionString = _connectionString,
                    rawSqlCount = rawCount,
                    efLinqCount = efRows
                });
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message, stackTrace = ex.StackTrace });
            }
        }

        // === 获取全部 Referrals（用在 DataTable）===
        [HttpGet("api/referrals/get-all")]
        public async Task<IActionResult> GetAllReferrals()
        {
            try
            {
                var rows = await _db.Referrals
                    .OrderByDescending(r => r.IsActive)
                    .ThenBy(r => r.ReferralName)
                    .Select(r => new { r.Id, r.ReferralName, r.IsActive, r.CreatedAt })
                    .ToListAsync();
                return Json(new { success = true, data = rows });
            }
            catch (Exception ex) { return Json(new { success = false, message = ex.Message }); }
        }

        // === 下拉选项（仅启用）===
        [HttpGet("api/referrals/options")]
        public async Task<IActionResult> GetReferralOptions()
        {
            try
            {
                var rows = await _db.Referrals
                    .Where(r => r.IsActive)
                    .OrderBy(r => r.ReferralName)
                    .Select(r => new { value = r.Id, text = r.ReferralName })
                    .ToListAsync();
                return Json(new { success = true, data = rows });
            }
            catch (Exception ex) { return Json(new { success = false, message = ex.Message }); }
        }

        // === 单条读取：api/referrals/get/1 ===
        [HttpGet("api/referrals/get/{id}")]
        public async Task<IActionResult> GetReferralById(int id)
        {
            try
            {
                var row = await _db.Referrals.FindAsync(id);
                if (row == null)
                    return Json(new { success = false, message = "Referral not found." });
                return Json(new { success = true, data = new { row.Id, row.ReferralName, row.IsActive, row.CreatedAt } });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        // === 新增：/api/referrals/create ===
        [HttpPost("api/referrals/create")]
        public async Task<IActionResult> CreateReferral([FromBody] ReferralCreateUpdateDto model)
        {
            try
            {
                if (model == null || string.IsNullOrWhiteSpace(model.ReferralName))
                    return Json(new { success = false, message = "Referral name is required." });

                var exists = await _db.Referrals.AnyAsync(r => r.ReferralName == model.ReferralName);
                if (exists)
                    return Json(new { success = false, message = "Referral name already exists." });

                var entity = new Referral
                {
                    ReferralName = model.ReferralName,
                    IsActive = model.IsActive,
                    CreatedAt = DateTime.UtcNow
                };

                _db.Referrals.Add(entity);
                await _db.SaveChangesAsync();

                return Json(new { success = true, id = entity.Id });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        // === 更新：/api/referrals/update ===
        [HttpPost("api/referrals/update")]
        public async Task<IActionResult> UpdateReferral([FromBody] ReferralUpdateDto model)
        {
            try
            {
                if (model == null || model.Id <= 0)
                    return Json(new { success = false, message = "Invalid id." });

                if (string.IsNullOrWhiteSpace(model.ReferralName))
                    return Json(new { success = false, message = "Referral name is required." });

                var entity = await _db.Referrals.FindAsync(model.Id);
                if (entity == null)
                    return Json(new { success = false, message = "Referral not found." });

                // 名称唯一性（排除自己）
                var exists = await _db.Referrals.AnyAsync(r => r.ReferralName == model.ReferralName && r.Id != model.Id);
                if (exists)
                    return Json(new { success = false, message = "Referral name already exists." });

                entity.ReferralName = model.ReferralName;
                entity.IsActive = model.IsActive;

                await _db.SaveChangesAsync();

                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        // === 删除：/api/referrals/delete?id=1 ===
        [HttpPost("api/referrals/delete")]
        public async Task<IActionResult> DeleteReferral(int id)
        {
            try
            {
                if (id <= 0) return Json(new { success = false, message = "Invalid id." });

                var entity = await _db.Referrals.FindAsync(id);
                if (entity == null)
                    return Json(new { success = false, message = "Referral not found." });

                _db.Referrals.Remove(entity);
                await _db.SaveChangesAsync();

                return Json(new { success = true });
            }
            catch (Exception ex) { return Json(new { success = false, message = ex.Message }); }
        }

        #endregion
    }
}