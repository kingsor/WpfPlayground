using WpfLoggingDemo.Helpers.NativeMethods;

namespace WpfLoggingDemo.Helpers;

public static class DesktopMonitorsHelper
{
    private static readonly List<DisplayInfo> Screens = new();

    public static List<DisplayInfo> GetScreens()
    {
        Screens.Clear();

        var handler = new DisplayDevicesMethods.EnumMonitorsDelegate(MonitorEnumProc);
        DisplayDevicesMethods.EnumDisplayMonitors(IntPtr.Zero, IntPtr.Zero, handler, IntPtr.Zero); // should be sequential

        return Screens;
    }

    private static bool MonitorEnumProc(IntPtr hMonitor, IntPtr hdcMonitor, DisplayDevicesMethods.NativeRect rect, IntPtr dwData)
    {
        DisplayDevicesMethods.MonitorInfoEx mi = new DisplayDevicesMethods.MonitorInfoEx();

        if (DisplayDevicesMethods.GetMonitorInfo(hMonitor, mi))
        {
            Screens.Add(new DisplayInfo(
                (mi.dwFlags & 1) == 1, // 1 = primary monitor
                mi.rcMonitor.Left,
                mi.rcMonitor.Top,
                Math.Abs(mi.rcMonitor.Right - mi.rcMonitor.Left),
                Math.Abs(mi.rcMonitor.Bottom - mi.rcMonitor.Top),
                new string(mi.szDevice).TrimEnd('\0')
                ));
        }

        return true;
    }
}
