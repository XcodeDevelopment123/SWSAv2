using Microsoft.AspNetCore.Mvc;
using SWSA.MvcPortal.Commons.Quartz.Requests;
using SWSA.MvcPortal.Commons.Quartz.Services.Interfaces;
using SWSA.MvcPortal.Commons.Quartz.Support;
using SWSA.MvcPortal.Dtos.Requests.SchedulerJobs;
using SWSA.MvcPortal.Services.Interfaces;

namespace SWSA.MvcPortal.Controllers;

[Route("scheduler-jobs")]

public class SchedulerJobController(
 IJobSchedulerService scheduler,
 IJobMetadataRegistry registry,
 IScheduledJobService service
) : BaseController
{
    [Route("")]
    public async Task<IActionResult> List()
    {
        var data = await service.GetAllScheduledJobs();
        return View(data);
    }

    [Route("create")]
    public async Task<IActionResult> Create()
    {
        return View();
    }

    [Route("{jobKey}/overview")]
    public async Task<IActionResult> JobOverview([FromRoute] string jobKey)
    {
        var data = await service.GetScheduledJobByJobKey(jobKey);
        return View(data);
    }

    [HttpPost("{jobKey}/execute")]
    public async Task<IActionResult> ExecuteNow([FromRoute] string jobKey)
    {
        var result = await service.ExecuteByJobKey(jobKey);
        return Ok(result);
    }

    [HttpPost("schedule")]
    public async Task<IActionResult> ScheduleJob(UpdateScheduleJobRequest req)
    {
        var result = await service.UpdateScheduleJob(req);
        return Ok(result);
    }

    [HttpPost("schedule-dynamic")]
    public async Task<IActionResult> ScheduleGenericJob(DynamicJobScheduleRequest input)
    {
        if (!registry.TryGetQuartzJobType(input.JobKey, out var type))
            return BadRequest("❌ Unknown job key.");

        IJobRequest? request = null;

        if (registry.RequiresPayload(input.JobKey))
        {
            if (input.Payload == null)
                return BadRequest("❌ Payload is required for this job.");

            if (!registry.ValidatePayload(input.JobKey, input.Payload.Value, out var error))
                return BadRequest($"❌ Payload invalid: {error}");

            var rawJson = input.Payload?.GetRawText();
            request = registry.DeserializeRequest(input.JobKey, rawJson!);
        }

        // 设置时间参数（注入调度行为）
        if (request is BaseJobRequest timed)
        {
            timed.StartTime = input.StartTime;
            timed.CronExpression = input.CronExpression;
        }

        await scheduler.ScheduleJob(request, type);
        return Ok("✅ Job scheduled successfully.");
    }
}