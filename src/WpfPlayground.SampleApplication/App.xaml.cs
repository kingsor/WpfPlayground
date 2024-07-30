using Microsoft.Extensions.DependencyInjection;
using System.IO;
using System.Reflection;
using System.Windows;
using WpfPlayground.Sdk;

namespace WpfPlayground.SampleApplication
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            // FIXME: do more robust checking than this
            // naive approach to blindly try and load
            // any and all assemblies with no checks!
            var assemblies = Directory
                .GetFiles(AppDomain.CurrentDomain.BaseDirectory, "*.dll")
                .Select(Assembly.LoadFrom)
                .ToArray();
            
            ServiceCollection services = new();
            services.Scan(x => x
                .FromAssemblies(assemblies)
                .AddClasses()
                .AsSelfWithInterfaces()
                .WithSingletonLifetime()
                );

            ServiceProvider serviceProvider = services.BuildServiceProvider();

            var mainWindow = serviceProvider.GetRequiredService<IMainWindow>();
            mainWindow.Show();
        }
    }

}
