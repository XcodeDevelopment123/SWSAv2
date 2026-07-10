# 实际执行的 Migration 命令序列

## 步骤 1: 创建迁移
```
命令：dotnet ef migrations add AddMissingColumnsToClients --project SWSA.MvcPortal --context AppDbContext --no-build

执行位置：C:\Users\Wayne\Desktop\SWSAv2

结果：生成文件 SWSA.MvcPortal/Migrations/20260618025150_AddMissingColumnsToClients.cs
```

### 生成的迁移文件内容（部分）：
```csharp
protected override void Up(MigrationBuilder migrationBuilder)
{
	// 向 SecDeptTaskTemplates 表添加列
	migrationBuilder.AddColumn<DateTime>(
		name: "ADDueDate",
		table: "SecDeptTaskTemplates",
		type: "datetime2",
		nullable: true);

	migrationBuilder.AddColumn<string>(
		name: "YearInput",
		table: "SecDeptTaskTemplates",
		type: "nvarchar(max)",
		nullable: true);

	// 向 BaseCompanies 表添加列
	migrationBuilder.AddColumn<string>(
		name: "AppointmentEngagementData",
		table: "BaseCompanies",
		type: "nvarchar(max)",
		nullable: true);

	migrationBuilder.AddColumn<string>(
		name: "BusinessNature",
		table: "BaseCompanies",
		type: "nvarchar(max)",
		nullable: true);

	migrationBuilder.AddColumn<string>(
		name: "ClientRating",
		table: "BaseCompanies",
		type: "nvarchar(max)",
		nullable: true);

	// ... 更多列 ...

	migrationBuilder.AddColumn<string>(
		name: "ServiceSelected",
		table: "BaseCompanies",
		type: "nvarchar(max)",
		nullable: true);
}
```

## 步骤 2: 应用迁移到数据库
```
命令：dotnet ef database update --project SWSA.MvcPortal --context AppDbContext --no-build

执行位置：C:\Users\Wayne\Desktop\SWSAv2

结果：Done. (表示迁移成功应用)
```

### 数据库中发生的变化：
1. **执行迁移中的 SQL 语句**：
   - `ALTER TABLE [BaseCompanies] ADD [AppointmentEngagementData] nvarchar(max) NULL`
   - `ALTER TABLE [BaseCompanies] ADD [BusinessNature] nvarchar(max) NULL`
   - `ALTER TABLE [BaseCompanies] ADD [ServiceSelected] nvarchar(max) NULL`
   - ... 等等

2. **记录迁移历史**：
   - 在 `__EFMigrationsHistory` 表中插入新行
   - MigrationId: `20260618025150_AddMissingColumnsToClients`

## 步骤 3: 手动修复 GroupId 列
```sql
命令：sqlcmd -S ".\SQLEXPRESS" -E -d Quartz -Q "
IF NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.COLUMNS 
WHERE TABLE_NAME = 'Clients' AND COLUMN_NAME = 'GroupId')
BEGIN
	ALTER TABLE [Clients] ADD [GroupId] int NULL;
END
"

原因：之前 AddGroupIdToClients 迁移被标记为已应用（通过SQL直接插入），
	  但实际的SQL没有执行，所以手动补执行
```

## 验证结果
```sql
命令：sqlcmd -S ".\SQLEXPRESS" -E -d Quartz -Q "
SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS 
WHERE TABLE_NAME = 'BaseCompanies' 
ORDER BY ORDINAL_POSITION;"

结果：
	Id
	FileNo
	RegistrationNumber
	...
	AppointmentEngagementData ✓ (已添加)
	BusinessNature ✓ (已添加)
	ClientRating ✓ (已添加)
	CompanyStatus ✓ (已添加)
	...
	ServiceSelected ✓ (已添加)
	ForeignOwned ✓ (已添加)
	PrincipalActivity ✓ (已添加)
```

## 总结

**三个关键命令：**
1. `dotnet ef migrations add AddMissingColumnsToClients` - 生成迁移文件
2. `dotnet ef database update` - 应用迁移，修改数据库
3. `sqlcmd` - 手动执行遗漏的SQL（用于修复历史问题）

**时间线：**
- 发现问题：数据库缺少列导致SQL查询失败
- 创建迁移：EF Core 检测模型和数据库的差异，生成迁移
- 应用迁移：实际创建这些列
- 结果：数据库Schema和代码模型同步，应用正常运行
