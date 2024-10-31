using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows;

namespace ZoomAndPanSample.ViewModels
{
    public class MainViewModel
    {
        public ObservableCollection<Tuple<Rect, Color>> Rectangles { get; set; }

        public MainViewModel()
        {
            Rectangles = new ObservableCollection<Tuple<Rect, Color>>
            {
                new Tuple<Rect, Color>(new Rect(50,25,50,25), Colors.Blue ),
                new Tuple<Rect, Color>(new Rect(150,100,100,50), Colors.Aqua ),
            };
        }
    }
}
