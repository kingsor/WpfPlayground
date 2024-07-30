using System.Windows;
using WpfPlayground.Plugin1.Converters;
using WpfPlayground.Plugin1.ViewModels;
using WpfPlayground.Sdk;

namespace WpfPlayground.Plugin1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, IMainWindow
    {
        public MainWindow(MainWindowViewModel viewModel, IServiceProvider serviceProvider)
        {
            NicksCoolConverter.ServiceProvider = serviceProvider;

            InitializeComponent();
            DataContext = viewModel;
        }
    }
}