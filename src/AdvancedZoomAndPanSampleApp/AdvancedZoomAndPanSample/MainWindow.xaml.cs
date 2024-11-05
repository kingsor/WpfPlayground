using Microsoft.Extensions.Logging;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using ZoomAndPanSample.ViewModels;

namespace ZoomAndPanSample
{
    /// <summary>
    ///     This is a Window that uses ZoomAndPanControl to zoom and pan around some content.
    ///     This demonstrates how to use application specific mouse handling logic with ZoomAndPanControl.
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly ILogger<MainWindow> _logger;

        public MainWindow(ILogger<MainWindow> logger)
        {
            Rectangles = new ObservableCollection<Tuple<Rect, Color>>
            {
                new Tuple<Rect, Color>(new Rect(50,50,80,150), Colors.Blue ),
                new Tuple<Rect, Color>(new Rect(550,350,80,150), Colors.Green ),
                new Tuple<Rect, Color>(new Rect(850,850,80,150), Colors.Purple ),
                new Tuple<Rect, Color>(new Rect(1850,1550,80,150), Colors.Red ),
            };

            _logger = logger;

            InitializeComponent();

            LoadRectangles();

            _logger.LogInformation("Components initialized");

        }

        private void LoadRectangles()
        {
            var mainCanvas = ZoomAndPanControlView.MainCanvas;

            foreach (var rect in Rectangles)
            {
                var control = new Rectangle
                {
                    Width = rect.Item1.Width,
                    Height = rect.Item1.Height,
                    Fill = new SolidColorBrush(rect.Item2),
                    Stroke = Brushes.Yellow,
                    StrokeThickness = 2,
                };

                mainCanvas.Children.Add(control);
                Canvas.SetTop(control, rect.Item1.Top);
                Canvas.SetLeft(control, rect.Item1.Left);
            }

            
        }

        /// <summary>
        ///     Event raised when the Window has loaded.
        /// </summary>
        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            //var helpTextWindow = new HelpTextWindow
            //{
            //    Left = Left + Width + 5,
            //    Top = Top,
            //    Owner = this
            //};
            //helpTextWindow.Show();
        }


        public ObservableCollection<Tuple<Rect, Color>> Rectangles
        {
            get;
            private set;
        }
    }
}