
using SWSA.MvcPortal.Commons.Extensions;
using SWSA.MvcPortal.Commons.Middlewares;
using SWSA.MvcPortal.Commons.Quartz.Services.Interfaces;
using SWSA.MvcPortal.Persistence.Seeders;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Logging.SetMinimumLevel(LogLevel.Information);

builder.Services.AddAppSetup(builder.Configuration, builder.Environment);
builder.Services.ConfigureSwsaDb(builder.Configuration);
builder.Services.AddSeedData();
builder.Services.ConfigureHttpClientService(builder.Configuration);
builder.Services.ConfigureAppService(builder.Configuration);
// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Configuration
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true)
    .AddEnvironmentVariables();

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

using (var scope = app.Services.CreateScope())
{
    var seederManager = scope.ServiceProvider.GetRequiredService<SeederManager>();
    await seederManager.SeedAll();

    var jobScheduler = scope.ServiceProvider.GetRequiredService<IJobSchedulerService>();
    // await jobScheduler.ClearAllJobs();

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

app.Run();
