using Quartz;
using SWSA.MvcPortal.Commons.Quartz.Requests;

namespace SWSA.MvcPortal.Commons.Quartz.Support;

/// <summary>
/// 用于在构建 Job 和 Trigger 时集中传递必要上下文（如 JobKey、Request、分组等）
/// 保证 Job 和 Trigger 使用一致的 Key，并可根据需要扩展元信息（如描述、用户信息）
/// </summary>
public class JobBuildContext
{
    public IJobRequest? Request { get; init; }
    public JobKey JobKey { get; init; }
    public string TriggerGroup { get; init; } = "Default";

    public bool IsUserJob => Request is BaseJobRequest br && br.IsCustom;

    public JobBuildContext(IJobRequest? request, JobKey jobKey)
    {
        Request = request;
        JobKey = jobKey;
    }

    /// 可扩展字段：比如 future: Description, ScheduleName, OwnerId 等
}

// 更新 BaseJobFactory 中引用此 Context 的部分
// 可在子类中通过 BuildContext(request) 获取统一的 JobKey
// 保证 JobDetail 和 Trigger 绑定一致
// 
// var ctx = BuildContext(request);
// var job = JobBuilder.Create(...).WithIdentity(ctx.JobKey).Build();
// var trigger = TriggerBuilder.Create().ForJob(ctx.JobKey).Build();
