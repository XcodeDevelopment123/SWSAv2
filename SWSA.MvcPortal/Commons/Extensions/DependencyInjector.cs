using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using SWSA.MvcPortal.Commons.Constants;
using SWSA.MvcPortal.Commons.Filters;
using SWSA.MvcPortal.Commons.Middlewares;
using SWSA.MvcPortal.Persistence;
using SWSA.MvcPortal.Persistence.Seeders;
using SWSA.MvcPortal.Repositories.Interfaces;
using SWSA.MvcPortal.Repositories.Repo;
using SWSA.MvcPortal.Services;
using SWSA.MvcPortal.Services.Interfaces;

namespace SWSA.MvcPortal.Commons.Extensions;

public static class DependencyInjector
{
    public static void AddAppSetup(this IServiceCollection services, IConfigurationManager configuration, IWebHostEnvironment environment)
    {
        services.AddHttpContextAccessor();
        services.AddTransient<ExceptionMiddleware>();

        var allowedOrigins = configuration.GetSection(AppSettings.AllowedOrigins).Get<string[]>();
        services.AddCors(options =>
        {
            options.AddPolicy(AppSettings.DynamicCorsPolicy, builder =>
            {
                builder.WithOrigins(allowedOrigins!) // enable domain
                       .AllowCredentials()  // Enable Cookie
                       .AllowAnyHeader()
                       .AllowAnyMethod();
            });
        });

        services.AddControllers(options =>
        {
            options.Filters.Add<LoginSessionFilter>();
        })

           .AddNewtonsoftJson(options =>
           {
               options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
           });

        services.AddSession(options =>
          {
              options.IdleTimeout = TimeSpan.FromMinutes(60);
              options.Cookie.HttpOnly = true; //Avoid XSS
              options.Cookie.IsEssential = true;
              options.Cookie.SecurePolicy = CookieSecurePolicy.Always; //Only Https working
          });

        services.AddMemoryCache();
        services.AddSingleton(new MemoryCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10),
            SlidingExpiration = TimeSpan.FromMinutes(5)
        });

        configuration
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
        .AddJsonFile($"appsettings.{environment.EnvironmentName}.json", optional: true)
        .AddEnvironmentVariables();
    }

    public static void ConfigureSwsaDb(this IServiceCollection services, IConfigurationManager configuration)
    {
        string connString = configuration.GetConnectionString(AppSettings.DbConnString)!;
        services.AddDbContext<AppDbContext>(options =>
        {
            options.UseSqlServer(connString,
                o => o.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery))
            .EnableSensitiveDataLogging(); //Show param in console

        }, ServiceLifetime.Scoped);

        services.AddScoped<ICompanyCommunicationContactRepository, CompanyCommunicationContactRepository>();
        services.AddScoped<ICompanyDepartmentRepository, CompanyDepartmentRepository>();
        services.AddScoped<ICompanyMsicCodeRepository, CompanyMsicCodeRepository>();
        services.AddScoped<ICompanyOfficialContactRepository, CompanyOfficialContactRepository>();
        services.AddScoped<ICompanyOwnerRepository, CompanyOwnerRepository>();
        services.AddScoped<ICompanyRepository, CompanyRepository>();
        services.AddScoped<ICompanyTypeRepository, CompanyTypeRepository>();
        services.AddScoped<IDepartmentRepository, DepartmentRepository>();
        services.AddScoped<IMsicCodeRepository, MsicCodeRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
    }

    public static void ConfigureAppService(this IServiceCollection services)
    {
        var applicationAssembly = typeof(DependencyInjector).Assembly;

        services.AddAutoMapper((serviceProvider, cfg) =>
        {

        }, applicationAssembly);

        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<ICompanyService, CompanyService>();
        services.AddScoped<ICompanyCommunicationContactService, CompanyCommunicationContactService>();
        services.AddScoped<ICompanyDepartmentService, CompanyDepartmentService>();
        services.AddScoped<ICompanyMsicCodeService, CompanyMsicCodeService>();
        services.AddScoped<ICompanyOfficialContactService, CompanyOfficialContactService>();
        services.AddScoped<ICompanyOwnerService, CompanyOwnerService>();
        services.AddScoped<ICompanyTypeService, CompanyTypeService>();
        services.AddScoped<IDepartmentService, DepartmentService>();
        services.AddScoped<IMsicCodeService, MsicCodeService>();
    }

    public static void AddSeedData(this IServiceCollection services)
    {
        services.AddTransient<ISeeder, UserSeeder>();
        services.AddTransient<ISeeder, CompanyTypeSeeder>();
        services.AddTransient<ISeeder, DepartmentSeeder>();
        services.AddTransient<ISeeder, MsicCodeSeeder>();
        services.AddTransient<SeederManager>();
    }

    public static void ConfigureHttpClientService(this IServiceCollection services, IConfiguration configuration)
    {

        //services.AddHttpClient("clientName", client =>
        //  {
        //      client.BaseAddress = new Uri("baseUrl");
        //  });
        //services.AddHttpClient<SmsService>(client =>
        //{
        //    client.BaseAddress = new Uri("https://api.example.com/");
        //    client.Timeout = TimeSpan.FromSeconds(30);
        //    client.DefaultRequestHeaders.Add("User-Agent", "SmsServiceHttpClient");
        //    client.DefaultRequestHeaders.Add("Accept", "application/json");
        //});

    }
}
