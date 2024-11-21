using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace DragDropSample
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
        }

        private Point BasePoint = new Point(0.0, 0.0);
        private double DeltaX = 0.0;
        private double DeltaY = 0.0;
        private bool moving = false;
        private Point PositionInLabel;

        public double XPosition
        {
            get { return BasePoint.X + DeltaX; }
        }

        public double YPosition
        {
            get { return BasePoint.Y + DeltaY; }
        }

        private void Feast_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Label l = e.Source as Label;
            if (l != null)
            {
                l.CaptureMouse();
                moving = true;
                PositionInLabel = e.GetPosition(l);
            }
        }

        private void Feast_MouseMove(object sender, MouseEventArgs e)
        {
            if (moving)
            {
                Point p = e.GetPosition(MainCanvas);
                DeltaX = p.X - BasePoint.X - PositionInLabel.X;
                DeltaY = p.Y - BasePoint.Y - PositionInLabel.Y;
                RaisePropertyChanged("XPosition");
                RaisePropertyChanged("YPosition");
            }
        }

        private void Feast_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Label l = e.Source as Label;
            if (l != null)
            {
                l.ReleaseMouseCapture();
                BasePoint.X += DeltaX;
                BasePoint.Y += DeltaY;
                DeltaX = 0.0;
                DeltaY = 0.0;
                moving = false;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void RaisePropertyChanged(string prop)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

        private void StackPanel_DragOver(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent("Object"))
            {
                // These Effects values are used in the drag source's
                // GiveFeedback event handler to determine which cursor to display.
                if (e.KeyStates == DragDropKeyStates.ControlKey)
                {
                    e.Effects = DragDropEffects.Copy;
                }
                else
                {
                    e.Effects = DragDropEffects.Move;
                }
            }
        }

        private void StackPanel_Drop(object sender, DragEventArgs e)
        {
            // If an element in the panel has already handled the drop,
            // the panel should not also handle it.
            if (e.Handled == false)
            {
                Panel _panel = (Panel)sender;
                UIElement _element = (UIElement)e.Data.GetData("Object");

                if (_panel != null && _element != null)
                {
                    // Get the panel that the element currently belongs to,
                    // then remove it from that panel and add it the Children of
                    // the panel that its been dropped on.
                    Panel _parent = (Panel)VisualTreeHelper.GetParent(_element);

                    if (_parent != null)
                    {
                        if (e.KeyStates == DragDropKeyStates.ControlKey &&
                            e.AllowedEffects.HasFlag(DragDropEffects.Copy))
                        {
                            Circle _circle = new Circle((Circle)_element);
                            _panel.Children.Add(_circle);
                            // set the value to return to the DoDragDrop call
                            e.Effects = DragDropEffects.Copy;
                        }
                        else if (e.AllowedEffects.HasFlag(DragDropEffects.Move))
                        {
                            _parent.Children.Remove(_element);
                            _panel.Children.Add(_element);
                            // set the value to return to the DoDragDrop call
                            e.Effects = DragDropEffects.Move;
                        }
                    }
                }
            }
        }
    }
}