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
using Microsoft.Extensions.Options;
using SWSA.MvcPortal.Models.DocumentRecords.Profiles;
using Quartz;
using System.Collections.Specialized;
using SWSA.MvcPortal.Commons.Quartz.Factories;
using SWSA.MvcPortal.Commons.Quartz.Jobs;
using SWSA.MvcPortal.Commons.Quartz.Services;
using SWSA.MvcPortal.Commons.Quartz.Services.Interfaces;
using Quartz.Spi;
using Quartz.Impl;
using SWSA.MvcPortal.Commons.Quartz.Config;
using SWSA.MvcPortal.Commons.Quartz;
using SWSA.MvcPortal.Commons.Services.Messaging.Implementation;
using SWSA.MvcPortal.Commons.Services.Messaging.Intefaces;
using SWSA.MvcPortal.Commons.Quartz.Support;
using Serilog;
using AspNetCoreRateLimit;
using SWSA.MvcPortal.Commons.Services.Permission;
using SWSA.MvcPortal.Commons.Services.Session;
using SWSA.MvcPortal.Commons.MapsterConfigs;
using SWSA.MvcPortal.Commons.Helpers;
using SWSA.MvcPortal.Services.SystemCore;
using SWSA.MvcPortal.Services.Interfaces.SystemCore;
using SWSA.MvcPortal.Services.Interfaces.Scheduler;
using SWSA.MvcPortal.Services.Interfaces.SystemInfra;
using SWSA.MvcPortal.Services.Interfaces.UserAccess;
using SWSA.MvcPortal.Services.CompanyProfile;
using SWSA.MvcPortal.Services.WorkAssignments;
using SWSA.MvcPortal.Services.SystemInfra;
using SWSA.MvcPortal.Services.UserAccess;
using SWSA.MvcPortal.Services.Scheduler;
using Microsoft.Data.SqlClient;
using System.Data;
using SWSA.MvcPortal.Services.Interfaces.WorkAssignments;
using SWSA.MvcPortal.Commons.Services.BackgroundQueue;
using SWSA.MvcPortal.Services.Interfaces.Clients;
using SWSA.MvcPortal.Services.Clients;
using SWSA.MvcPortal.Entities.Clients;

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
        }).AddNewtonsoftJson(options =>
           {
               options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
               options.SerializerSettings.Converters.Add(new DisplayEnumConverter());
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

        services.AddSignalR().AddNewtonsoftJsonProtocol();

        //Rate limiting
        services.Configure<IpRateLimitOptions>(configuration.GetSection("IpRateLimiting"));
        services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();
        services.AddInMemoryRateLimiting();

        //Should at after session added
        services.AddScoped<IUserContext, UserContext>();

        MapsterConfig.RegisterMappings();
    }

    public static void AddHostService(this IHostBuilder host)
    {
        host.UseSerilog((ctx, services, config) =>
                config.ReadFrom.Configuration(ctx.Configuration)
                      .ReadFrom.Services(services)
                      .Enrich.FromLogContext());
    }
    public static void ConfigureSwsaDb(this IServiceCollection services, IConfigurationManager configuration, IWebHostEnvironment environment)
    {
        string connString = configuration.GetConnectionString(AppSettings.DbConnString)!;

        services.AddDbContextFactory<AppDbContext>(options =>
        {
            options.UseSqlServer(connString, o => o.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery));
        });

        services.AddDbContext<AppDbContext>(options =>
        {
            options.UseSqlServer(connString,
                o => o.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery));

            if (environment.IsDevelopment())
            {
                options.EnableSensitiveDataLogging();
            }
        }, ServiceLifetime.Scoped);

        services.AddScoped<IDbConnection>(sp =>
        {
            return new SqlConnection(connString);
        });

        //#Repository DI (auto generated)
        services.AddScoped<ICommunicationContactRepository, CommunicationContactRepository>();
        services.AddScoped<IOfficialContactRepository, OfficialContactRepository>();
        services.AddScoped<IClientRepository, ClientRepository>();

        services.AddScoped<ICompanyMsicCodeRepository, CompanyMsicCodeRepository>();

        services.AddScoped<IWorkAssignmentRepository, WorkAssignmentRepository>();
        services.AddScoped<IWorkProgressRepository, WorkProgressRepository>();
        services.AddScoped<IDocumentRecordRepository, DocumentRecordRepository>();
        services.AddScoped<IMsicCodeRepository, MsicCodeRepository>();
        services.AddScoped<IScheduledJobRepository, ScheduledJobRepository>();
        services.AddScoped<ISystemAuditLogRepository, SystemAuditLogRepository>();
        services.AddScoped<ISystemNotificationLogRepository, SystemNotificationLogRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IWorkAssignmentUserMappingRepository, WorkAssignmentUserMappingRepository>();
        //#Repository DI end
    }

    public static void ConfigureAppService(this IServiceCollection services, IConfiguration config)
    {
        var applicationAssembly = typeof(DependencyInjector).Assembly;
        services.Configure<FileSettings>(config.GetSection("FileSettings"));

        //Auto mapper use config 
        services.AddAutoMapper((serviceProvider, cfg) =>
        {
            var fileSettings = serviceProvider.GetRequiredService<IOptions<FileSettings>>();
            cfg.AddProfile(new DocumentRecordProfile(fileSettings));
        }, applicationAssembly);

        //#Service DI (auto generated)
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IClientService, ClientService>();
        services.AddScoped<IClientOptionService, ClientOptionService>();
        services.AddScoped<ICommunicationContactService, CommunicationContactService>();
        services.AddScoped<IOfficialContactService, OfficialContactService>();
        services.AddScoped<ICompanyMsicCodeService, CompanyMsicCodeService>();
        services.AddScoped<ICompanyWorkAssignmentService, CompanyWorkAssignmentService>();
        services.AddScoped<ICompanyWorkProgressService, CompanyWorkProgressService>();
        services.AddScoped<IDocumentRecordService, DocumentRecordService>();
        services.AddScoped<IMsicCodeService, MsicCodeService>();
        services.AddScoped<IScheduledJobService, ScheduledJobService>();
        services.AddScoped<ISystemAuditLogService, SystemAuditLogService>();
        services.AddScoped<ISystemNotificationLogService, SystemNotificationLogService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IWorkAssignmentUserMappingService, WorkAssignmentUserMappingService>();
        //#Service DI end

        //Factory
        services.AddScoped<IClientCreationFactory, ClientCreationFactory>();
        services.AddScoped<IWorkAssignmentFactory, WorkAssignmentFactory>();

        services.AddScoped<IUserSessionWriter, UserSessionWriter>();
        services.AddScoped<IUserFetcher, UserFetcher>();

        //Messaging service
        services.AddSingleton<IBackgroundTaskQueue, BackgroundTaskQueue>();
        services.AddSingleton<IMessageDispatcher, DefaultDispatcher>();
        services.AddSingleton<ITemplateRegistry, InMemoryTemplateRegistry>();
        services.AddSingleton<IMessageProducer, InMemoryMessageQueue>();
        services.AddSingleton(sp => (IMessageConsumer)sp.GetRequiredService<IMessageProducer>());
        services.AddSingleton<IMessagingService, MessagingService>();
        services.AddSingleton<IPermissionRefreshTracker, MemoryPermissionRefreshTracker>();

        // 注册所有 Sender（可多个）
        services.AddSingleton<IMessageSender, SmsSender>();
        services.AddSingleton<IMessageSender>(sp => sp.GetRequiredService<WappySender>());

        // Background 消费服务
        services.AddHostedService<MessageQueueWorker>();
        services.AddHostedService<BackgroundTaskWorker>();
    }

    public static void AddSeedData(this IServiceCollection services)
    {
        services.AddTransient<ISeeder, UserSeeder>();
        services.AddTransient<ISeeder, MsicCodeSeeder>();
        services.AddTransient<ISeeder, ScheduledJobSeeder>();
        services.AddTransient<SeederManager>();
    }

    public static void ConfigureHttpClientService(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<WappySettings>(configuration.GetSection(nameof(WappySettings)));


        //services.AddHttpClient("clientName", client =>
        //  {
        //      client.BaseAddress = new Uri("baseUrl");
        //  });
        services.AddHttpClient<WappySender>((serviceProvider, client) =>
        {
            var options = serviceProvider.GetRequiredService<IOptions<WappySettings>>();
            var token = options.Value.ApiToken;
            var url = options.Value.Url;
            client.BaseAddress = new Uri(url);
            client.Timeout = TimeSpan.FromSeconds(30);
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token} ");

        });
    }

    public static IServiceCollection AddQuartzJobs(this IServiceCollection services, IConfiguration configuration)
    {

        var quartzConfig = configuration.GetSection("Quartz").Get<QuartzConfig>();
        if (quartzConfig != null)
        {
            var properties = new NameValueCollection
            {
                ["quartz.scheduler.instanceName"] = quartzConfig.Scheduler.InstanceName,
                ["quartz.scheduler.instanceId"] = quartzConfig.Scheduler.InstanceId,
                ["quartz.scheduler.idleWaitTime"] = quartzConfig.Scheduler.IdleWaitTime.ToString(),
                ["quartz.threadPool.type"] = quartzConfig.ThreadPool.Type,
                ["quartz.threadPool.maxConcurrency"] = quartzConfig.ThreadPool.MaxConcurrency.ToString(),
                ["quartz.jobStore.type"] = quartzConfig.JobStore.Type,
                ["quartz.jobStore.driverDelegateType"] = quartzConfig.JobStore.DriverDelegateType,
                ["quartz.jobStore.dataSource"] = quartzConfig.JobStore.DataSource,
                ["quartz.jobStore.tablePrefix"] = quartzConfig.JobStore.TablePrefix,
                ["quartz.jobStore.misfireThreshold"] = quartzConfig.JobStore.MisfireThreshold.ToString(),
                ["quartz.jobStore.clustered"] = quartzConfig.JobStore.Clustered.ToString(),
                ["quartz.jobStore.acquireTriggersWithinLock"] = quartzConfig.JobStore.AcquireTriggersWithinLock.ToString(),
                ["quartz.dataSource.default.connectionString"] = quartzConfig.DataSource.Default.ConnectionString,
                ["quartz.dataSource.default.provider"] = quartzConfig.DataSource.Default.Provider,
                ["quartz.serializer.type"] = quartzConfig.Serializer.Type
            };
            services.AddSingleton<ISchedulerFactory>(_ => new StdSchedulerFactory(properties));
            // Quartz needs a DI-enabled JobFactory
            services.AddSingleton<IJobFactory, QuartzJobFactory>();
            services.AddSingleton<IJobListener, QuartzJobListener>();


            // Register IScheduler with injected JobFactory and start
            services.AddScoped<IScheduler>(provider =>
            {
                var factory = provider.GetRequiredService<ISchedulerFactory>();
                var scheduler = factory.GetScheduler().Result;

                scheduler.JobFactory = provider.GetRequiredService<IJobFactory>();

                var listener = provider.GetRequiredService<IJobListener>();
                scheduler.ListenerManager.AddJobListener(listener);
                scheduler.Start().GetAwaiter().GetResult();

                return scheduler;
            });
        }

        services.AddSingleton<IJobMetadataRegistry, JobMetadataRegistry>();
        services.AddSingleton<IJobExecutionResolver, JobExecutionResolver>();

        // Job-specific factories (your IJobBaseFactory design)
        services.AddTransient<AssignmentDueSoonJobFactory>();
        services.AddTransient<GenerateAssignmentReportJobFactory>();
        services.AddTransient<AssignmentRemindJobFactory>();

        // Job class instances (support DI)
        services.AddScoped<AssignmentDueSoonJob>();
        services.AddScoped<GenerateAssignmentReportJob>();
        services.AddScoped<AssignmentRemindJob>();

        // Services used in jobs
        //  services.AddScoped<IAssignmentDueSoonJobService, AssignmentDueSoonJobService>();

        services.AddScoped<IJobSchedulerService, JobSchedulerService>();

        return services;
    }
}

