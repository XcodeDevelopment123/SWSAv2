using Quartz;
using SWSA.MvcPortal.Commons.Quartz.Requests;
using SWSA.MvcPortal.Entities;

namespace SWSA.MvcPortal.Commons.Quartz.Support;
public static class JobRequestMapper
{
    /// <summary>
    /// 从数据库实体和反序列化后的请求构造 JobBuildContext
    /// 确保 JobKey 按系统任务 / 用户任务逻辑一致
    /// </summary>
    public static JobBuildContext ToContext(ScheduledJob jobEntity, IJobRequest? request)
    {
        // JobKey 命名策略
        var baseKey = new JobKey(jobEntity.JobKey, jobEntity.JobGroup);

        var isUserJob = request is BaseJobRequest br && br.IsCustom;
        var finalKey = isUserJob
            ? new JobKey($"{baseKey.Name}_{Guid.NewGuid()}", baseKey.Group)
            : baseKey;

        // 注入 StartTime 和 CronExpression（用于 trigger 构建）
        if (request is BaseJobRequest baseReq)
        {
            baseReq.StartTime = jobEntity.TriggerTime;
            baseReq.CronExpression = jobEntity.CronExpression;
        }

        return new JobBuildContext(request, finalKey)
        {
            TriggerGroup = baseKey.Group // 可选：继承 job 的 group
        };
    }


    // ✅ 可用于 Resolver:
    // var context = JobRequestMapper.ToContext(jobEntity, request);
    // factory.CreateJob(context);
    // factory.CreateTrigger(context);
}

public static class JobDataBinder
{
    public static JobDataMap ToJobDataMap(IJobRequest request)
    {
        var dict = new Dictionary<string, object>();

        foreach (var prop in request.GetType().GetProperties())
        {
            var value = prop.GetValue(request);
            if (value != null)
            {
                dict[prop.Name] = value;
            }
        }

        return new JobDataMap((IDictionary<string, object>)dict);
    }
}