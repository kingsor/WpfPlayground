using System.Windows;
using System.Windows.Data;
using WpfPlayground.SampleApplication.Converters;
using WpfPlayground.SampleApplication.ViewModels;
using WpfPlayground.SampleApplication.Views;

namespace WpfPlayground.SampleApplication
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow(MainWindowViewModel viewModel, NicksCoolConverter nicksCoolConverter)
        {
            InitializeComponent();
            DataContext = viewModel;

            Binding binding = new(nameof(MainWindowViewModel.CoolLevel))
            {
                Converter = nicksCoolConverter,
            };

            CoolLabel.SetBinding(ContentProperty, binding);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var wnd = new GridLinesWindow();
            wnd.ShowDialog();
        }
    }
}