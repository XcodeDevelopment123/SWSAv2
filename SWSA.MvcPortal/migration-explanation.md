## EF Core 迁移操作步骤

### 我使用的命令：

#### 1. 创建新迁移（Add Migration）
```powershell
cd "C:\Users\Wayne\Desktop\SWSAv2"
dotnet ef migrations add AddMissingColumnsToClients --project SWSA.MvcPortal --context AppDbContext --no-build
```

**这做了什么：**
- 扫描 AppDbContext 中的所有实体模型（BaseClient、BaseCompany等）
- 对比它们的属性和当前数据库Schema
- 检测到缺失的列（AppointmentEngagementData、BusinessNature、ServiceSelected等）
- 在 Migrations 文件夹中生成新文件：`20260618025150_AddMissingColumnsToClients.cs`
- 这个文件包含 Up() 方法，里面有 ALTER TABLE ADD COLUMN 的SQL语句

#### 2. 应用迁移到数据库（Database Update）
```powershell
dotnet ef database update --project SWSA.MvcPortal --context AppDbContext --no-build
```

**这做了什么：**
- 连接到数据库（Quartz）
- 查看 __EFMigrationsHistory 表，检查哪些迁移已经应用
- 执行所有未应用的迁移（包括 AddMissingColumnsToClients）
- 在 __EFMigrationsHistory 中记录 20260618025150_AddMissingColumnsToClients 为已应用
- 在数据库中实际执行了 SQL：
  ```sql
  ALTER TABLE [BaseCompanies] ADD [AppointmentEngagementData] nvarchar(max) NULL;
  ALTER TABLE [BaseCompanies] ADD [BusinessNature] nvarchar(max) NULL;
  ALTER TABLE [BaseCompanies] ADD [ServiceSelected] nvarchar(max) NULL;
  -- ... 其他列
  ```

#### 3. 手动执行缺失的 SQL（因为之前的问题）
```sql
IF NOT EXISTS (
	SELECT 1 FROM INFORMATION_SCHEMA.COLUMNS 
	WHERE TABLE_NAME = 'Clients' AND COLUMN_NAME = 'GroupId'
)
BEGIN
	ALTER TABLE [Clients] ADD [GroupId] int NULL;
END
```

**为什么需要手动执行：**
- 之前的开发者或我在调试时用SQL直接插入了迁移历史记录
- 这导致迁移被标记为"已应用"，但实际的SQL没有执行
- 所以我手动执行了那个迁移中的SQL

### 结果效果：

**迁移之前：**
- 数据库表和代码模型不匹配
- SQL查询引用的列（[b].[ServiceSelected]、[b].[AppointmentEngagementData]等）在数据库中不存在
- 应用执行查询时报错：`Invalid column name 'ServiceSelected'`
- HTTP 500 错误

**迁移之后：**
- 所有必需的列都在数据库中创建
- SQL查询可以正常执行
- 页面加载成功，没有500错误

### 关键概念：

**Add-Migration 和 Update-Database 的关系：**
- `Add-Migration` = 设计阶段：生成迁移脚本文件
- `Update-Database` = 执行阶段：实际修改数据库

两个都需要执行才能完成：
1. 先 `Add-Migration` 生成迁移代码
2. 再 `Update-Database` 在数据库中执行迁移
