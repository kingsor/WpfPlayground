namespace WpfPlayground.SampleApplication.ViewModels
{
    public class MainWindowViewModel
    {

        public string CustomTitle { get; } = "Hello, World";

        public string FancyText { get; set; } = "Dev Leader Rocks!";

        public bool ShowFancyText { get; set; } = true;

        public double CoolLevel { get; } = 42.1337;
    }
}
