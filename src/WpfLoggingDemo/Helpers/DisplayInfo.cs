namespace WpfLoggingDemo.Helpers;

/// <summary>
/// Represents a display device on a single system.
/// </summary>
public sealed class DisplayInfo
{
    /// <summary>
    /// Initializes a new instance of the DisplayInfo class.
    /// </summary>
    /// <param name="primary">A value indicating whether the display is the primary screen.</param>
    /// <param name="x">The display's top corner X value.</param>
    /// <param name="y">The display's top corner Y value.</param>
    /// <param name="w">The width of the display.</param>
    /// <param name="h">The height of the display.</param>
    public DisplayInfo(bool primary, int x, int y, int w, int h, string deviceName)
    {
        IsPrimary = primary;
        TopX = x;
        TopY = y;
        Width = w;
        Height = h;
        DeviceName = deviceName;
    }

    /// <summary>
    /// Gets a value indicating whether the display device is the primary monitor.
    /// </summary>
    public bool IsPrimary { get; private set; }

    /// <summary>
    /// Gets the display's top corner X value.
    /// </summary>
    public int TopX { get; private set; }

    /// <summary>
    /// Gets the display's top corner Y value.
    /// </summary>
    public int TopY { get; private set; }

    /// <summary>
    /// Gets the width of the display.
    /// </summary>
    public int Width { get; private set; }

    /// <summary>
    /// Gets the height of the display.
    /// </summary>
    public int Height { get; private set; }

    /// <summary>
    /// Gets the device name
    /// </summary>
    public string DeviceName { get; private set; }
}
