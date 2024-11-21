using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace WpfPanAndZoom.CustomControls
{
    public class OtherWidget : Button
    {
        private bool _dragging;
        private UIElement _selectedElement;
        private Vector _draggingDelta;

        public OtherWidget()
        {
            //MouseDown += OnMouseDown;
            //MouseMove += OnMouseMove;
            //MouseUp += OnMouseUp;
        }

        private void OnMouseUp(object sender, MouseButtonEventArgs e)
        {
            _dragging = false;
            _selectedElement = null;
        }

        private void OnMouseMove(object sender, MouseEventArgs e)
        {
            if (_dragging && e.RightButton == MouseButtonState.Pressed)
            {
                double x = Mouse.GetPosition(this).X;
                double y = Mouse.GetPosition(this).Y;

                if (_selectedElement != null)
                {
                    Canvas.SetLeft(_selectedElement, x + _draggingDelta.X);
                    Canvas.SetTop(_selectedElement, y + _draggingDelta.Y);
                }
            }
        }

        private void OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Right)
            {
                _selectedElement = (UIElement)e.Source;
                Point mousePosition = Mouse.GetPosition(this);
                double x = Canvas.GetLeft(_selectedElement);
                double y = Canvas.GetTop(_selectedElement);
                Point elementPosition = new Point(x, y);
                _draggingDelta = elementPosition - mousePosition;

                _dragging = true;
            }
        }
    }
}
