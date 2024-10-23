using Microsoft.Extensions.DependencyInjection;
using System.IO;
using System.Reflection;
using System.Windows;

namespace WpfPlayground.SampleApplication
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly ServiceProvider _serviceProvider;

        public App()
        {
            
            ServiceCollection services = new();
            
            services.Scan(x => x
                .FromCallingAssembly()
                .AddClasses()
                //.AsSelfWithInterfaces()
                .AsSelf()
                .WithSingletonLifetime()
                );

            _serviceProvider = services.BuildServiceProvider();

            
        }

        protected override async void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var mainWindow = _serviceProvider.GetRequiredService<MainWindow>();

            mainWindow.Show();
        }
    }

}
