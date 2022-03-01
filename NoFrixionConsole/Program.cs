

/// Original Programs.cs in net core 6
//// See https://aka.ms/new-console-template for more information
//Console.WriteLine("Hello, World!");



using Architecture.AppSettingsOptions;
using Architecture.BusinessLayer;
using Architecture.Services.CoinDeskService;
using Architecture.Services.JsonSerializer;
using Implementation.BusinessLayer;
using Implementation.Services.CoinDeskService;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;
using NoFrixionConsole;
using System;
using static Implementation.Services.JsonSerializerService.JSonSerializeService;


var _logger = NLog.LogManager.GetCurrentClassLogger();
_logger.Debug("init main");
try
{

    //Read Enviroment
    var environmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
    var builder = new ConfigurationBuilder()
               .AddJsonFile($"appsettings.json", true, true)
               .AddJsonFile($"appsettings.{environmentName}.json", true, true)
               .AddEnvironmentVariables();
    var configuration = builder.Build();



    // Setup DI
    var services = new ServiceCollection();
    services.AddLogging(builder =>
    {
        builder.SetMinimumLevel(LogLevel.Information);
        builder.AddNLog("nlog.config");
    });

    var appSettings = configuration.GetSection("AppSettings").Get<AppSettingsOptions>();
    //   if (appSettings == null) _logger.Error("No se pudo traer App Settings");
    services.AddSingleton<AppSettingsOptions>(appSettings);
    services.AddSingleton<IJSonSerializerService, JSonSerializerService>();
    services.AddHttpContextAccessor(); //For inject HttpContextFactory in code behind
    var servicesNames = new string[] { "Default", "CoinDesk" };
    NoFrixionConsole.Helpers.HttpServices.Add(services, appSettings, servicesNames);
    //https://josef.codes/you-are-probably-still-using-httpclient-wrong-and-it-is-destabilizing-your-software/

    services.AddSingleton<ICoinDeskService, CoinDeskService>();
    services.AddSingleton<IBusinessLayer, BusinessLayer>();
    services.AddSingleton<ConsoleApp>();

    var serviceProvider = services.BuildServiceProvider();


    var business = serviceProvider.GetService<ConsoleApp>();

    await business.RunAsync();

}
catch (Exception exception)
{
    // NLog: catch setup errors
    _logger.Error(exception, "Stopped program because of exception");
    throw;
}
finally
{
    // Ensure to flush and stop internal timers/threads before application-exit (Avoid segmentation fault on Linux)
    NLog.LogManager.Shutdown();
}

