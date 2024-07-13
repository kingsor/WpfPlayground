using Microsoft.Extensions.DependencyInjection;
using System.Windows;
using WpfPlayground.SampleApplication.Extensions;

namespace WpfPlayground.SampleApplication
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            ServiceCollection services = new();
            services.ConfigureServices();

            ServiceProvider serviceProvider = services.BuildServiceProvider();

            var mainWindow = serviceProvider.GetRequiredService<MainWindow>();
            MainWindow.Show();
        }
    }

}
