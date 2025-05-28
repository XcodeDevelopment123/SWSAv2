using Newtonsoft.Json;
using SWSA.MvcPortal.Commons.Enums;
using SWSA.MvcPortal.Commons.Quartz.Factories;
using SWSA.MvcPortal.Commons.Quartz.Requests;
using System.Text.Json;

namespace SWSA.MvcPortal.Commons.Quartz.Support;

public interface IJobMetadataRegistry
{
    bool TryGetFactory(string jobKey, out IJobBaseFactory factory);
    bool TryGetQuartzJobType(string jobKey, out ScheduledJobType type);
    bool RequiresPayload(string jobKey);
    bool ValidatePayload(string jobKey, JsonElement payloadJson, out string? error);
    IJobRequest? DeserializeRequest(string jobKey, string json);
}

/// <summary>
///  When you have create new job, u must register the key to the Register() method
///  Add factories _factories
///  If with request, also need to add to _deserializers
///  If request with required payload, add to _jobsRequirePayload
/// </summary>
public class JobMetadataRegistry : IJobMetadataRegistry
{
    private readonly IServiceProvider _serviceProvider;

    public JobMetadataRegistry(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;

        Register();
    }

    private readonly Dictionary<string, Func<IJobBaseFactory>> _factories = new();
    private readonly Dictionary<string, Func<string, IJobRequest?>> _deserializers = new();
    private readonly HashSet<string> _jobsRequirePayload = new();
    private readonly Dictionary<string, ScheduledJobType> _jobKeyToType = new();

    private void Register()
    {
        // === Types
        _jobKeyToType[QuartzJobKeys.AssignmentDueSoonJobKey.Name] = ScheduledJobType.AssignmentDueSoon;
        _jobKeyToType[QuartzJobKeys.GenerateAssignmentReportJobKey.Name] = ScheduledJobType.GenerateAssignmentReport;
        _jobKeyToType[QuartzJobKeys.AssignmentRemindJobKey.Name] = ScheduledJobType.AssignmentRemind;

        // === Factories
        _factories[QuartzJobKeys.AssignmentDueSoonJobKey.Name] = () =>
            _serviceProvider.GetRequiredService<AssignmentDueSoonJobFactory>();
        _factories[QuartzJobKeys.AssignmentRemindJobKey.Name] = () =>
            _serviceProvider.GetRequiredService<AssignmentRemindJobFactory>();
        _factories[QuartzJobKeys.GenerateAssignmentReportJobKey.Name] = () =>
            _serviceProvider.GetRequiredService<GenerateAssignmentReportJobFactory>();

        // === Only those have specific request payload
        _deserializers[QuartzJobKeys.GenerateAssignmentReportJobKey.Name] = json =>
            JsonConvert.DeserializeObject<GenerateReportJobRequest>(json);

        _jobsRequirePayload.Add(QuartzJobKeys.GenerateAssignmentReportJobKey.Name);
    }

    public bool TryGetFactory(string jobKey, out IJobBaseFactory factory)
    {
        if (_factories.TryGetValue(jobKey, out var resolver))
        {
            factory = resolver();
            return true;
        }

        factory = null!;
        return false;
    }

    public IJobRequest? DeserializeRequest(string jobKey, string json)
    {
        return _deserializers.TryGetValue(jobKey, out var parser)
            ? parser(json)
            : null;
    }

    public bool TryGetQuartzJobType(string jobKey, out ScheduledJobType type)
    {
        return _jobKeyToType.TryGetValue(jobKey, out type);
    }

    public bool RequiresPayload(string jobKey) =>
      _jobsRequirePayload.Contains(jobKey);

    public bool ValidatePayload(string jobKey, JsonElement payloadJson, out string? error)
    {
        error = null;

        if (!RequiresPayload(jobKey))
            return true;

        if (!_deserializers.TryGetValue(jobKey, out var parser))
        {
            error = "Deserializer not registered for this job.";
            return false;
        }

        try
        {
            var json = payloadJson.GetRawText();
            var request = parser(json);
            if (request == null)
            {
                error = "Invalid or incomplete payload format.";
                return false;
            }

            var missingProps = request.GetType()
                .GetProperties()
                .Where(p => p.GetValue(request) == null)
                .Select(p => p.Name)
                .ToList();

            if (missingProps.Count != 0)
            {
                error = $"Missing required fields: {string.Join(", ", missingProps)}";
                return false;
            }

            return true;
        }
        catch (Exception ex)
        {
            error = $"Payload parsing error: {ex.Message}";
            return false;
        }
    }
}