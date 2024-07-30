using Microsoft.Extensions.DependencyInjection;
using System.Globalization;
using System.Windows.Data;

namespace WpfPlayground.Plugin1.Converters;

public sealed class StringFormattingHelper
{
    public string FormatDouble(double value, int decimalPlaces)
    {
        return value.ToString($"F{decimalPlaces}", CultureInfo.InvariantCulture);
    }
}

public class NicksCoolConverter : IValueConverter
{
    StringFormattingHelper _stringFormattingHelper;

    public NicksCoolConverter()
    {
        ArgumentNullException.ThrowIfNull(ServiceProvider);
        _stringFormattingHelper = ServiceProvider.GetRequiredService<StringFormattingHelper>();
    }

    internal static IServiceProvider? ServiceProvider { get; set; }

    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        var numericValue = (double)value;
        var formatted = $"{numericValue:0.00}";
        return formatted;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
