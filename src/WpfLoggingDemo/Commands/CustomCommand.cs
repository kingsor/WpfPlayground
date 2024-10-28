using Microsoft.Extensions.Logging;

namespace WpfLoggingDemo.Commands;

public class CustomCommand : CommandBase
{
    private readonly ILogger<CustomCommand> _logger;

    public CustomCommand(ILogger<CustomCommand> logger)
    {
        _logger = logger;
    }

    public override void Execute(object? parameter)
    {
        _logger.LogInformation("Executing custom command");
    }
}
