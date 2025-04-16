using Quartz;

namespace SWSA.MvcPortal.Commons.Quartz.Jobs;

public class GenerateAssignmentReportJob : IJob
{
    public async Task Execute(IJobExecutionContext context)
    {
        var data = context.MergedJobDataMap;
        int companyId = data.GetInt("CompanyId");
        int month = data.GetInt("Month");
        int year = data.GetInt("Year");

        Console.WriteLine($"📊 Generating report for CompanyId: {companyId}, {month}/{year}");
        await Task.Delay(1000); // 模拟处理

        Console.WriteLine("✅ Report generation completed.");
    }
}
