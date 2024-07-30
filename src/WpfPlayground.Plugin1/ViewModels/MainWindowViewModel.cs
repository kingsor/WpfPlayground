using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfPlayground.Plugin1.ViewModels
{
    public class MainWindowViewModel
    {

        public string CustomTitle { get; } = "Hello, World";

        public string CustomLabel { get; } = "Dev Leader Rocks!";

        public string FancyText { get; set; } = "Dev Leader Rocks!";

        public double CoolLevel { get; } = 42.1337;
    }
}
