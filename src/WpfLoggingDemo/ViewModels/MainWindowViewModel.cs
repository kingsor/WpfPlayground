using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace WpfLoggingDemo.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    private readonly ILogger<MainWindowViewModel> _logger;

    public ICommand CustomCommand { get; }

    private string _messagesContent = string.Empty;
    public string MessagesContent
    {
        get
        {
            return _messagesContent;
        }

        set
        {
            _messagesContent = value;
            RaisePropertyChanged();
        }
    }

    public MainWindowViewModel(ICommand customCommand, ILogger<MainWindowViewModel> logger)
    {
        CustomCommand = customCommand;
        _logger = logger;

        _logger.LogInformation("Created view model");
    }

    public void AddMessageContent(string message)
    {
        _logger.LogDebug($"Adding message to MessagesContent: {message}");

        var sb = new StringBuilder();
        sb.AppendLine($"{message}");
        sb.Append(MessagesContent);
        MessagesContent = sb.ToString();
    }
}
