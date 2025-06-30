global using Newtonsoft.Json;
global using Newtonsoft.Json.Converters;

using SWSA.MvcPortal.Commons.Extensions;
using SWSA.MvcPortal.Commons.Middlewares;
using SWSA.MvcPortal.Commons.Quartz.Services.Interfaces;
using SWSA.MvcPortal.Persistence.Seeders;
using Serilog;
using SWSA.MvcPortal.Commons.SignalR;
using AspNetCoreRateLimit;


try
{
    var builder = WebApplication.CreateBuilder(args);
    // 必须最先配置 Configuration，决定使用哪个 appsettings 文件
    builder.Configuration
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
        .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true)
        .AddEnvironmentVariables();

    // 在配置好 Configuration 和 Environment 后，立刻初始化 Serilog
    builder.Host.AddHostService();

    builder.Services.AddAppSetup(builder.Configuration, builder.Environment);
    builder.Services.ConfigureSwsaDb(builder.Configuration, builder.Environment);
    builder.Services.AddSeedData();
    builder.Services.ConfigureHttpClientService(builder.Configuration);
    builder.Services.ConfigureAppService(builder.Configuration);
    // Add services to the container.
    builder.Services.AddControllersWithViews();

    builder.Services.AddQuartzJobs(builder.Configuration);
    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (!app.Environment.IsDevelopment())
    {
        app.UseExceptionHandler("/Home/Error");
        // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
        app.UseHsts();
    }
    app.UseMiddleware<ExceptionMiddleware>();
    app.UseIpRateLimiting();
    using (var scope = app.Services.CreateScope())
    {
        var seederManager = scope.ServiceProvider.GetRequiredService<SeederManager>();
        await seederManager.SeedAll();

        var jobScheduler = scope.ServiceProvider.GetRequiredService<IJobSchedulerService>();
        //    await jobScheduler.ClearAllJobs();

        await jobScheduler.ScheduleBackgroundJob();
        Console.WriteLine("Background job scheduled at system startup.");
    }

    app.UseHttpsRedirection();
    app.UseStaticFiles();

    app.UseRouting();
    app.UseSession();
    app.UseRequestLogging();

    app.UseAuthorization();

    app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Auth}/{action=Login}");

    //Signal R
    app.MapHub<SignalRHub>("/hubs/notification");

    app.Run();
}
catch (Exception ex)
{
    Log.Error(ex, "Error");
    Log.Fatal(ex, "Application failed to start");
}
