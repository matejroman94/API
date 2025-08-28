using AutoMapper;
using Database;
using Domain.MappingProfiles;
using EFCore.DbContextFactory.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting.WindowsServices;
using Microsoft.Extensions.Logging.EventLog;
using Repository.Interfaces;
using Repository.RepositoryWithFactory;
using Serilog;
using VZPStatAPI.Services;
using VZPStatAPI.Helpers;

try
{
    var options = new WebApplicationOptions
    {
        Args = args,
        ContentRootPath = WindowsServiceHelpers.IsWindowsService()
                                 ? AppContext.BaseDirectory : default
    };

    var builder = WebApplication.CreateBuilder(options);

    if (OperatingSystem.IsWindows())
    {
        // This setup allows the application to write logs to the Windows Event Log under the specified log and source names
        builder.Services.Configure<EventLogSettings>(config =>
        {
            config.LogName = "VZP API";
            config.SourceName = "VZP API Source";
        });
    }

    // Add services to the container.

    Log.Logger = new LoggerConfiguration().ReadFrom.Configuration(builder.Configuration, sectionName: "Serilog").CreateLogger();

    builder.Services.AddHttpContextAccessor();
    builder.Services.AddSingleton<IURIService>(o =>
    {
        var accessor = o.GetRequiredService<IHttpContextAccessor>();
        var request = accessor.HttpContext?.Request;
        var uri = string.Concat(request?.Scheme, "://", request?.Host.ToUriComponent());
        return new URIService(uri);
    });
    builder.Services.AddResponseCaching();
    builder.Services.AddControllers(options =>
    {       
        options.CacheProfiles.Add("Default",
            new CacheProfile()
            {
                Duration = 1
            });
        options.ReturnHttpNotAcceptable = true;
        options.Filters.Add(new ProducesAttribute("application/json"));
    }).AddNewtonsoftJson(x => 
    {
        x.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
    });
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen(options =>
    {
        options.UseAllOfToExtendReferenceSchemas();
    });

    builder.Services.AddApiVersioning(option =>
    {
        option.AssumeDefaultVersionWhenUnspecified = true;
        option.DefaultApiVersion = new ApiVersion(1, 10);
        option.ReportApiVersions = true;
    });

    string ConnectionString = builder.Configuration["ConnectionStrings:VZPDatabase"];
      builder.Services.AddDbContextFactory<VZPStatDbContext>(builder => builder
            .UseSqlServer(ConnectionString, b => b.MigrationsAssembly("Database"))
            );

    builder.Services.AddScoped<IUnitOfWork, UnitOfWorkWithFactory>();

    var config = new MapperConfiguration(cfg =>
    {
        cfg.AddProfile(new ActivityProfile());
        cfg.AddProfile(new BranchProfile());
        cfg.AddProfile(new ClerkProfile());
        cfg.AddProfile(new ClientProfile());
        cfg.AddProfile(new CounterProfile());
        cfg.AddProfile(new EventProfile());
        cfg.AddProfile(new LoggerProfile());
        cfg.AddProfile(new PrinterProfile());
        cfg.AddProfile(new DiagnosticBranchProfile());
        cfg.AddProfile(new ClerkEventProfile());
        cfg.AddProfile(new RegionProfile());
        cfg.AddProfile(new UserProfile());
        cfg.AddProfile(new RoleProfile());
        cfg.AddProfile(new AppProfile());
    });
    var mapper = config.CreateMapper();
    builder.Services.AddSingleton(mapper);
    builder.Services.AddSingleton<ConfigFileLog>();

    // It tells the host builder that the application should be installed and run as a Windows Service
    builder.Host.UseWindowsService();

    mapper = config.CreateMapper();
    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI(
            options =>
            {
                options.DisplayOperationId();
            });
    }
    else
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();

    app.UseAuthorization();

    app.MapControllers();

    app.Run();

    
}
catch (Exception ex)
{
    string msg = ex.Message;
    msg += ex.InnerException?.Message;
    Logger.Logger.NewOperationLog("Startup failed " + msg,Logger.Logger.Level.Error);
}