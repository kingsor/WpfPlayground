﻿using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using System.Windows;
using System.Windows.Input;
using WpfLoggingDemo.Commands;
using WpfLoggingDemo.Helpers;
using WpfLoggingDemo.ViewModels;

namespace WpfLoggingDemo;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    private readonly IHost _host;
    private readonly ILogger<App> _logger;

    public App()
    {

        var builder = Host.CreateDefaultBuilder();

        builder.ConfigureServices(ConfigureServices)
        .UseSerilog();

        _host = builder.Build();

        _logger = _host.Services.GetRequiredService<ILogger<App>>();

        _logger.LogInformation("App created");

        LogSystemInfos();
    }

    private void ConfigureServices(HostBuilderContext hostContext, IServiceCollection services)
    {
        IConfiguration config = hostContext.Configuration;
        Log.Logger = new LoggerConfiguration()
            .ReadFrom.Configuration(config)
            .CreateLogger();

        services.AddSingleton<ICommand, CustomCommand>();
        services.AddSingleton<MainWindowViewModel>();

        services.AddSingleton<MainWindow>();

        services.AddTransient<SystemInfosHelper>();
    }

    protected override void OnStartup(StartupEventArgs e)
    {
        _logger.LogInformation("OnStartup");

        _host.Start();

        MainWindow = _host.Services.GetRequiredService<MainWindow>();
        MainWindow.Show();

        base.OnStartup(e);
    }

    protected override async void OnExit(ExitEventArgs e)
    {
        _logger.LogInformation("OnExit");

        await _host.StopAsync();
        _host.Dispose();

        base.OnExit(e);
    }

    private void LogSystemInfos()
    {
        var sysInfos = _host.Services.GetRequiredService<SystemInfosHelper>();

        sysInfos.LogInfos();
    }
}
