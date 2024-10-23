using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using System.Drawing;
using Point = System.Windows.Point;

namespace WpfPlayground.SampleApplication.Views
{
    /// <summary>
    /// Interaction logic for GridLinesWindow.xaml
    /// </summary>
    public partial class GridLinesWindow : Window
    {
        private Brush _color1 = new SolidColorBrush(Colors.Red);
        private Brush _color2 = new SolidColorBrush(Colors.DarkRed);

        public GridLinesWindow()
        {
            InitializeComponent();
        }

        private void ShowGridlines_OnChecked(object sender, RoutedEventArgs e)
        {
            DrawGraph((int)SliderValue.Value, (int)SliderValue.Value, ShapeCanvas);
            SliderValue.IsEnabled = true;
        }

        private void ShowGridlines_OnUnchecked(object sender, RoutedEventArgs e)
        {
            RemoveGraph(ShapeCanvas);
            SliderValue.IsEnabled = false;
        }

        private void SliderValue_OnValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (ShowGridlines.IsChecked ?? false)
            {
                DrawGraph((int)SliderValue.Value, (int)SliderValue.Value, ShapeCanvas);
            }
        }

        private void DrawGraph(int yoffSet, int xoffSet, Canvas mainCanvas)
        {
            RemoveGraph(mainCanvas);
            Image lines = new Image();
            lines.SetValue(Panel.ZIndexProperty, -100);
            //Draw the grid
            DrawingVisual gridLinesVisual = new DrawingVisual();
            DrawingContext dct = gridLinesVisual.RenderOpen();
            Pen lightPen = new Pen(_color1, 0.5), darkPen = new Pen(_color2, 1);
            lightPen.Freeze();
            darkPen.Freeze();

            int yOffset = yoffSet,
                xOffset = xoffSet,
                rows = (int)(SystemParameters.PrimaryScreenHeight),
                columns = (int)(SystemParameters.PrimaryScreenWidth),
                alternate = yOffset == 5 ? yOffset : 1,
                j = 0;

            //Draw the horizontal lines
            Point x = new Point(0, 0.5);
            Point y = new Point(SystemParameters.PrimaryScreenWidth, 0.5);

            for (int i = 0; i <= rows; i++, j++)
            {
                dct.DrawLine(j % alternate == 0 ? lightPen : darkPen, x, y);
                x.Offset(0, yOffset);
                y.Offset(0, yOffset);
            }
            j = 0;
            //Draw the vertical lines
            x = new Point(0.5, 0);
            y = new Point(0.5, SystemParameters.PrimaryScreenHeight);

            for (int i = 0; i <= columns; i++, j++)
            {
                dct.DrawLine(j % alternate == 0 ? lightPen : darkPen, x, y);
                x.Offset(xOffset, 0);
                y.Offset(xOffset, 0);
            }

            dct.Close();

            RenderTargetBitmap bmp = new RenderTargetBitmap((int)SystemParameters.PrimaryScreenWidth,
                (int)SystemParameters.PrimaryScreenHeight, 96, 96, PixelFormats.Pbgra32);
            bmp.Render(gridLinesVisual);
            bmp.Freeze();
            lines.Source = bmp;

            mainCanvas.Children.Add(lines);
        }

        private void RemoveGraph(Canvas mainCanvas)
        {
            foreach (UIElement obj in mainCanvas.Children)
            {
                if (obj is Image)
                {
                    mainCanvas.Children.Remove(obj);
                    break;
                }
            }
        }
    }
}
