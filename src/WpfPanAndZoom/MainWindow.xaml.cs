using Microsoft.Extensions.Logging;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using WpfPanAndZoom.CustomControls;

namespace WpfPanAndZoom
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly ILogger<MainWindow> _logger;

        public MainWindow(ILogger<MainWindow> logger)
        {
            _logger = logger;

            InitializeComponent();

            _logger.LogInformation("Components initialized");

            LoadControls();
        }

        private void LoadControls()
        {
            Widget w1 = new Widget
            {
                Width = 150,
                Height = 100
            };
            canvas.Children.Add(w1);
            w1.Header.Text = "Widget 1";
            Canvas.SetTop(w1, 100);
            Canvas.SetLeft(w1, 100);

            Widget w2 = new Widget
            {
                Width = 150,
                Height = 100
            };
            canvas.Children.Add(w2);
            w2.Header.Text = "Widget 2";
            w2.HeaderRectangle.Fill = Brushes.Blue;
            Canvas.SetTop(w2, 400);
            Canvas.SetLeft(w2, 400);

            Widget w3 = new Widget
            {
                Width = 150,
                Height = 100
            };
            canvas.Children.Add(w3);
            w3.Header.Text = "Widget 3";
            w3.HeaderRectangle.Fill = Brushes.Red;
            Canvas.SetTop(w3, 400);
            Canvas.SetLeft(w3, 800);

            _logger.LogInformation("Controls inserted");
        }
    }
}