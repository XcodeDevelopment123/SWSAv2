
using SWSA.MvcPortal.Commons.Extensions;
using SWSA.MvcPortal.Commons.Middlewares;
using SWSA.MvcPortal.Persistence.Seeders;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAppSetup(builder.Configuration,builder.Environment);
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
