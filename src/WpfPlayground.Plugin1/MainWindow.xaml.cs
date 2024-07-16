using System.Windows;
using WpfPlayground.SampleApplication.ViewModels;
using WpfPlayground.Sdk;

namespace WpfPlayground.SampleApplication
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, IMainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainWindowViewModel();
        }
    }
}