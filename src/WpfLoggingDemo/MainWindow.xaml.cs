using Microsoft.Extensions.Logging;
using System.Windows;
using WpfLoggingDemo.ViewModels;

namespace WpfLoggingDemo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly MainWindowViewModel _viewModel;
        private readonly ILogger<MainWindow> _logger;

        public MainWindow(MainWindowViewModel viewModel, ILogger<MainWindow> logger)
        {
            _viewModel = viewModel;
            _logger = logger;

            DataContext = viewModel;

            InitializeComponent();

            _logger.LogInformation("Components initialized");

            _viewModel.AddMessageContent("MainWindow has started.");
        }
    }
}