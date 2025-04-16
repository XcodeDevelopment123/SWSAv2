# 📘 Quartz Job 架构说明（中文）

## 🧩 架构角色简述

### 🏭 Factory（工厂）

- **职责：** 负责管理 `JobRequest` 参数、生成 Job 实例、Trigger 调度设定。
- **细节说明：**
  - 透过 `CreateJob()` 方法传入参数（JobDataMap）
  - 在 `CreateTrigger()` 中设定是否立即执行 (`StartNow()`)、是否重复（可扩展为 Cron）
  - 每个 Job 对应一个 Factory，具有清晰的责任边界

```csharp
public override IJobDetail CreateJob(IJobRequest? request)
{
    var cast = request as GenerateReportJobRequest ?? throw new ArgumentException("Invalid request type.");

    var map = new JobDataMap();
    map.AddGenerateReportRequest(cast);

    return JobBuilder.Create<GenerateAssignmentReportJob>()
        .WithIdentity(JobKeys.GenerateAssignmentReportJobKey)
        .UsingJobData(map)
        .Build();
}

public override ITrigger CreateTrigger(IJobRequest? request)
{
    return TriggerBuilder.Create()
        .WithIdentity($"trigger_{Guid.NewGuid()}", QuartzGroupKeys.ReportGroup)
        .StartNow()
        .ForJob(JobKeys.GenerateAssignmentReportJobKey)
        .Build();
}
```

### 🧠 Job（任务）

- **职责：** 作为任务执行的核心，接受注入服务并执行逻辑
- **说明：**
  - `Job.Execute()` 是执行入口，通常调用 Service 层方法
  - 通过 DI 注入业务 Service 以及日志工具

### 🔧 Service（服务）

- **职责：** 封装 Job 的业务逻辑
- **说明：**
  - 提供如数据库查询、通知发送、报表生成等业务实现
  - Job 只负责触发，Service 负责执行

---

## 🧱 Job 注册与组织结构

### 一对一绑定关系

- 一个 Job → 一个 Factory
- 每新增一个 Job，需要同时新增：
  - Job class
  - Job factory class
  - JobType enum entry
  - JobKey static entry（用于 Identity）

### 分组（Group）分类

- 使用 GroupName 做逻辑分类
- 例子：
  - `notificationGroup`：系统通知类 Job
  - `reportGroup`：生成报表类 Job

---

## ⏱ 调度策略

### 🕹 用户可自定义触发时间

- 使用 `ScheduleJob(request, JobType.XXX)`
- 支持用户提交时间参数（如执行时间、频率）

### ⚙️ 系统背景作业

- 使用 `ScheduleBackgroundJob()`
- 在系统启动时注册固定任务（如：提醒、监控）

---

## 🚨 注意事项

1. `IJobFactory` 必须启用作用域 (`IServiceScopeFactory`) 来支持 Scoped Job 的注入
2. 所有 Job 都应为 `Scoped`，避免使用 Singleton Job
3. 使用 JobDataMap 传参时，值必须为基本类型（如 int, string, datetime）
4. Trigger 可以扩展为 CronTrigger 支持周期性作业
5. 注册新 Job 时需更新：
   - `JobKeys.cs`
   - `JobType.cs`
   - `IJobRequest` 实现类
   - 对应 Factory 和 Job 本体

---

## ✅ 推荐文件命名

| 类型        | 文件名示例                                   |
| --------- | --------------------------------------- |
| Job       | `GenerateAssignmentReportJob.cs`        |
| Factory   | `GenerateAssignmentReportJobFactory.cs` |
| Request   | `GenerateReportJobRequest.cs`           |
| Service   | `AssignmentDueSoonJobService.cs`        |
| Constants | `JobKeys.cs`, `JobType.cs`              |

---

# 📘 Quartz Job Architecture Overview (English)

## 🧩 Architecture Roles

### 🏭 Factory

- **Responsibilities:**
  - Manage `JobRequest` parameters
  - Build `JobDetail` and assign `JobDataMap`
  - Create and configure `Trigger` (start time, repeat or not)

```csharp
public override IJobDetail CreateJob(IJobRequest? request)
{
    var cast = request as GenerateReportJobRequest ?? throw new ArgumentException("Invalid request type.");

    var map = new JobDataMap();
    map.AddGenerateReportRequest(cast);

    return JobBuilder.Create<GenerateAssignmentReportJob>()
        .WithIdentity(JobKeys.GenerateAssignmentReportJobKey)
        .UsingJobData(map)
        .Build();
}

public override ITrigger CreateTrigger(IJobRequest? request)
{
    return TriggerBuilder.Create()
        .WithIdentity($"trigger_{Guid.NewGuid()}", QuartzGroupKeys.ReportGroup)
        .StartNow()
        .ForJob(JobKeys.GenerateAssignmentReportJobKey)
        .Build();
}
```

### 🧠 Job

- **Responsibilities:** Core execution unit triggered by Quartz
- **Details:**
  - Implement `IJob.Execute()`
  - Use DI to inject services and loggers
  - Delegate business logic to Job Service

### 🔧 Job Service

- **Responsibilities:**
  - Implement actual business logic (e.g., DB operations, notifications)
  - Keep Job class slim and decoupled from business rules

---

## 🧱 Job Registration & Structure

### One-to-One Binding

- One job ⇆ One factory
- New job requires:
  - Job class
  - Factory class
  - New `JobType` enum entry
  - New `JobKey` static entry

### Group Categorization

- Job groups help organize job purposes:
  - `notificationGroup`: System-level background tasks
  - `reportGroup`: Report generation, data exports

---

## ⏱ Scheduling Strategy

### 🕹 User-triggered jobs

- Use `ScheduleJob(request, JobType.X)`
- Users can control job timing (e.g., set time, one-off)

### ⚙️ Background jobs

- Use `ScheduleBackgroundJob()`
- Executed automatically during app boot or periodic intervals

---

## 🚨 Important Notes

1. Use `IServiceScopeFactory` in your `QuartzJobFactory` to resolve scoped services
2. Do not resolve jobs directly from root provider
3. All jobs must be registered as `Scoped`
4. `JobDataMap` must only contain primitive types (int, string, etc.)
5. When creating a new job:
   - Add key in `JobKeys.cs`
   - Add enum in `JobType.cs`
   - Create job class + factory + request DTO

---

## ✅ Suggested File Naming

| Type      | Example Filename                        |
| --------- | --------------------------------------- |
| Job       | `GenerateAssignmentReportJob.cs`        |
| Factory   | `GenerateAssignmentReportJobFactory.cs` |
| Request   | `GenerateReportJobRequest.cs`           |
| Service   | `AssignmentDueSoonJobService.cs`        |
| Constants | `JobKeys.cs`, `JobType.cs`              |

---

This documentation summarizes how to use Quartz in a modular, extensible, and scalable architecture. For updates, follow the pattern and always isolate Factory/Job/Service cleanly.

