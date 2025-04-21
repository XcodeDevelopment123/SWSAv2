using Quartz;
using SWSA.MvcPortal.Commons.Quartz.Factories;
using SWSA.MvcPortal.Commons.Quartz.Requests;
using SWSA.MvcPortal.Entities;
namespace SWSA.MvcPortal.Commons.Quartz.Support;

/// <summary>
///  When you have create new job, u must add the key to the GetJobKeyFromEnum() method,
///  You will no need to edit any other method, excep you know what to change
/// </summary>
public interface IJobExecutionResolver
{
    IJobDetail CreateJob(ScheduledJob jobEntity);
    ITrigger CreateTrigger(ScheduledJob jobEntity);

    IJobDetail CreateJob(IJobRequest? request, QuratzJobType type);
    ITrigger CreateTrigger(IJobRequest? request, QuratzJobType type);


    /// <summary>
    /// 一次性构建包含 JobDetail 与 Trigger 的调度上下文
    /// </summary>
    (JobBuildContext Context, IJobDetail Job, ITrigger Trigger) BuildAll(IJobRequest? request, QuratzJobType type);

    /// <summary>
    /// 从 request + job type 构建 JobBuildContext，统一 JobKey 命名规则
    /// </summary>
    JobBuildContext BuildContext(IJobRequest? request, QuratzJobType type);
}

public class JobExecutionResolver : IJobExecutionResolver
{
    private readonly IJobMetadataRegistry _registry;

    public JobExecutionResolver(IJobMetadataRegistry registry)
    {
        _registry = registry;
    }

    public (JobBuildContext Context, IJobDetail Job, ITrigger Trigger) BuildAll(IJobRequest? request, QuratzJobType type)
    {
        var context = BuildContext(request, type);
        var jobKey = GetJobKeyFromEnum(type);

        var factory = ResolveFactory(jobKey);

        var job = factory.CreateJob(context);
        var trigger = factory.CreateTrigger(context);

        return (context, job, trigger);
    }

    public IJobDetail CreateJob(ScheduledJob jobEntity)
    {
        var factory = ResolveFactory(jobEntity.JobKey);

        var request = _registry.RequiresPayload(jobEntity.JobKey) && !string.IsNullOrWhiteSpace(jobEntity.RequestPayloadJson)
            ? _registry.DeserializeRequest(jobEntity.JobKey, jobEntity.RequestPayloadJson!)
            : null;

        var context = JobRequestMapper.ToContext(jobEntity, request);
        return factory.CreateJob(context);
    }

    public ITrigger CreateTrigger(ScheduledJob jobEntity)
    {
        var factory = ResolveFactory(jobEntity.JobKey);

        var request = _registry.RequiresPayload(jobEntity.JobKey) && !string.IsNullOrWhiteSpace(jobEntity.RequestPayloadJson)
            ? _registry.DeserializeRequest(jobEntity.JobKey, jobEntity.RequestPayloadJson!)
            : null;

        var context = JobRequestMapper.ToContext(jobEntity, request);
        return factory.CreateTrigger(context);
    }

    public IJobDetail CreateJob(IJobRequest? request, QuratzJobType type)
    {
        var context = BuildContext(request, type);
        var factory = ResolveFactory(context.JobKey.Name);
        return factory.CreateJob(context);
    }

    public ITrigger CreateTrigger(IJobRequest? request, QuratzJobType type)
    {
        var context = BuildContext(request, type);
        var factory = ResolveFactory(context.JobKey.Name);
        return factory.CreateTrigger(context);
    }

    public JobBuildContext BuildContext(IJobRequest? request, QuratzJobType type)
    {
        var baseKeyName = GetJobKeyFromEnum(type);
        var baseKey = new JobKey(baseKeyName);

        var isCustom = request is BaseJobRequest br && br.IsCustom;
        var finalKey = isCustom
            ? new JobKey($"{baseKey.Name}_{Guid.NewGuid()}", baseKey.Group)
            : baseKey;

        return new JobBuildContext(request, finalKey)
        {
            TriggerGroup = baseKey.Group
        };
    }

    private IJobBaseFactory ResolveFactory(string jobKey)
    {
        return _registry.TryGetFactory(jobKey, out var factory)
            ? factory
            : throw new InvalidOperationException($"Factory not found for {jobKey}");
    }

    private string GetJobKeyFromEnum(QuratzJobType type) => type switch
    {
        QuratzJobType.GenerateAssignmentReport => QuartzJobKeys.GenerateAssignmentReportJobKey.Name,
        QuratzJobType.AssignmentDueSoon => QuartzJobKeys.AssignmentDueSoonJobKey.Name,
        _ => throw new NotImplementedException($"JobKey mapping not found for {type}")
    };
}