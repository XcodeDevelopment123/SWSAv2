global using Newtonsoft.Json;
global using Newtonsoft.Json.Converters;
using System.Text;
using AspNetCoreRateLimit;
using Microsoft.EntityFrameworkCore;
using Serilog;
using SWSA.MvcPortal.Commons.Extensions;
using SWSA.MvcPortal.Commons.Middlewares;
using SWSA.MvcPortal.Data;
using SWSA.MvcPortal.Persistence;
using SWSA.MvcPortal.Persistence.Seeders;

try
{
    // 设置控制台编码支持中文
    Console.OutputEncoding = Encoding.UTF8;
    Console.InputEncoding = Encoding.UTF8;

    var builder = WebApplication.CreateBuilder(args);
    // 配置 Configuration
    builder.Configuration
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
        .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true)
        .AddEnvironmentVariables();

    builder.Services.AddDbContext<QuartzContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("SwsaConntection")));

    // 初始化 Serilog
    builder.Host.AddHostService();

    // 注册服务
    builder.Services.AddAppSetup(builder.Configuration, builder.Environment);
    builder.Services.ConfigureSwsaDb(builder.Configuration, builder.Environment);
    builder.Services.AddSeedData();
    builder.Services.ConfigureHttpClientService(builder.Configuration);
    builder.Services.ConfigureAppService(builder.Configuration);
    builder.Services.AddControllersWithViews();
    builder.Services.AddQuartzJobs(builder.Configuration);
    builder.Services.AddControllers(); // 添加 API Controller 支持
    builder.Services.AddSession();

    var app = builder.Build();

    // 配置 HTTP 请求管道
    if (!app.Environment.IsDevelopment())
    {
        app.UseExceptionHandler("/Home/Error");
        app.UseHsts();
    }

    app.UseMiddleware<ExceptionMiddleware>();
    app.UseIpRateLimiting();

    // 应用启动事件
    app.Lifetime.ApplicationStarted.Register(() =>
    {
        Console.WriteLine("🚀 应用程序已成功启动");
        Console.WriteLine($"环境: {app.Environment.EnvironmentName}");
    });

    // 数据库初始化和数据播种
    using (var scope = app.Services.CreateScope())
    {
        try
        {
            var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();

            if (await dbContext.Database.CanConnectAsync())
            {
                var seederManager = scope.ServiceProvider.GetRequiredService<SeederManager>();
                await seederManager.SeedAll();
                Console.WriteLine("数据初始化完成");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"数据库初始化错误: {ex.Message}");
            Log.Error(ex, "数据库初始化过程中发生错误");
        }
    }

    // 配置中间件
    app.UseHttpsRedirection();
    app.UseStaticFiles();
    app.UseRouting();
    app.UseSession();
    app.UseRequestLogging();
    app.UseAuthorization();

    // 🔥 添加这一行 - 这是 API Controller 能够工作的关键！
    app.MapControllers();

    app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Auth}/{action=Login}");

    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "应用程序启动失败");
    Console.WriteLine($"启动错误: {ex.Message}");
}