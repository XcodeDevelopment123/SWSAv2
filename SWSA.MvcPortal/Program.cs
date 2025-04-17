
using SWSA.MvcPortal.Commons.Extensions;
using SWSA.MvcPortal.Commons.Middlewares;
using SWSA.MvcPortal.Commons.Quartz.Services.Interfaces;
using SWSA.MvcPortal.Persistence.Seeders;
using Serilog;
using SWSA.MvcPortal.Commons.SignalR;

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
    builder.Host.UseSerilog((ctx, services, config) =>
        config.ReadFrom.Configuration(ctx.Configuration)
              .ReadFrom.Services(services)
              .Enrich.FromLogContext());

    builder.Services.AddAppSetup(builder.Configuration, builder.Environment);
    builder.Services.ConfigureSwsaDb(builder.Configuration);
    builder.Services.AddSeedData();
    builder.Services.ConfigureHttpClientService(builder.Configuration);
    builder.Services.ConfigureAppService(builder.Configuration);
    // Add services to the container.
    builder.Services.AddControllersWithViews();

    builder.Services.AddQuartzJobs(builder.Configuration);
    var app = builder.Build();

    app.UseRequestLogging();
    // Configure the HTTP request pipeline.
    if (!app.Environment.IsDevelopment())
    {
        app.UseExceptionHandler("/Home/Error");
        // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
        app.UseHsts();
    }
    app.UseMiddleware<ExceptionMiddleware>();

    using (var scope = app.Services.CreateScope())
    {
        var seederManager = scope.ServiceProvider.GetRequiredService<SeederManager>();
        await seederManager.SeedAll();

        var jobScheduler = scope.ServiceProvider.GetRequiredService<IJobSchedulerService>();
        await jobScheduler.ClearAllJobs();

        await jobScheduler.ScheduleBackgroundJob();
        Console.WriteLine("Background job scheduled at system startup.");
    }

    app.UseHttpsRedirection();
    app.UseStaticFiles();

    app.UseSession();
    app.UseRouting();

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
    Log.Fatal(ex, "Application failed to start");
}
