using Microsoft.Extensions.Logging;
using System.IO;
using System.Management;
using System.Reflection;
using System.Runtime.InteropServices;

namespace WpfLoggingDemo.Helpers;

public class SystemInfosHelper
{

    private readonly ILogger<SystemInfosHelper> _logger;

    public SystemInfosHelper(ILogger<SystemInfosHelper> logger)
    {
        _logger = logger;
    }

    public void LogInfos()
    {
        var asm = Assembly.GetExecutingAssembly();

        _logger.LogInformation($"Application version: {asm.GetName().Version}");
        _logger.LogInformation($"CommandLine: {Environment.CommandLine}");
        _logger.LogInformation($"CurrentDirectory: {Environment.CurrentDirectory}");

        var osProperties = GetOperatingSystemInfos();

        _logger.LogInformation($"OSVersion: {osProperties["Caption"]} {osProperties["OSArchitecture"]} ({osProperties["Version"]}) ");

        _logger.LogInformation($"MachineName: {Environment.MachineName}");
        _logger.LogInformation($"UserDomainName: {Environment.UserDomainName}");
        _logger.LogInformation($"UserName: {Environment.UserName}");
        _logger.LogInformation($"UserInteractive: {Environment.UserInteractive}");

        _logger.LogInformation($"Framework Version: {RuntimeInformation.FrameworkDescription}");
        _logger.LogInformation($"CLR Version: {Environment.Version}");

        LogDrivesInfos();

        LogMonitorDetails();
    }

    private void LogMonitorDetails()
    {
        _logger.LogInformation("Win32_DesktopMonitor");
        _logger.LogInformation("-----------------------------------");

        try
        {
            ManagementObjectSearcher searcher =
                new ManagementObjectSearcher("root\\CIMV2",
                "SELECT * FROM Win32_DesktopMonitor");

            foreach (ManagementObject queryObj in searcher.Get())
            {
                _logger.LogInformation("Win32_DesktopMonitor instance");
                //_logger.LogInformation("\tDescription: {0}", queryObj["Description"]);
                foreach (var prop in queryObj.Properties)
                {
                    var propValue = prop.Value?.ToString() ?? "(Unable to detect)";
                    _logger.LogInformation($"\t{prop.Name} : {propValue}");
                }
            }
        }
        catch (ManagementException e)
        {
            _logger.LogError($"An error occurred while querying for WMI data: {e.Message}");
        }

        _logger.LogInformation("-----------------------------------");


        _logger.LogInformation("Monitors -> GetScreens");
        _logger.LogInformation("-----------------------------------");

        try
        {
            var screens = DesktopMonitorsHelper.GetScreens();

            foreach (var screen in screens)
            {
                _logger.LogInformation($"\tMonitor [{screen.DeviceName}] [ isPrimary = {screen.IsPrimary}, x = {screen.TopX}, y = {screen.TopY}, width = {screen.Width}, height = {screen.Height} ]");
            }
        }
        catch (Exception ex)
        {
            _logger.LogError($"\tAn error occurred while querying NativeMethods: {ex.Message}");
        }
    }

    private void LogDrivesInfos()
    {
        foreach (var driveInfo in DriveInfo.GetDrives())
        {
            try
            {
                var bytesInGigabyte = 1073741824;
                _logger.LogInformation($"DriveName: {driveInfo.Name}");
                _logger.LogInformation($"\tVolumeLabel: {driveInfo.VolumeLabel}");
                _logger.LogInformation($"\tDriveType: {driveInfo.DriveType}");
                _logger.LogInformation($"\tDriveFormat: {driveInfo.DriveFormat}");
                var val = ((double)driveInfo.TotalSize / bytesInGigabyte).ToString("N");
                _logger.LogInformation($"\tTotal size of drive: {val} Gb ({driveInfo.TotalSize} bytes)");
                val = ((double)driveInfo.TotalFreeSpace / bytesInGigabyte).ToString("N");
                _logger.LogInformation($"\tTotal available space: {val} Gb ({driveInfo.TotalFreeSpace} bytes)");
                val = ((double)driveInfo.AvailableFreeSpace / bytesInGigabyte).ToString("N");
                _logger.LogInformation($"\tAvailable space to current user: {val} Gb ({driveInfo.AvailableFreeSpace} bytes)");
            }
            catch (Exception ex)
            {
                _logger.LogError($"\tError getting drive infos: {ex.Message}");
            }
        }
    }

    private Dictionary<string, string> GetOperatingSystemInfos()
    {
        Dictionary<string, string> osProperties = new Dictionary<string, string>();

        ManagementClass mgmntClass = new ManagementClass("Win32_OperatingSystem");
        ManagementObjectCollection mgmntObjs = mgmntClass.GetInstances();
        PropertyDataCollection properties = mgmntClass.Properties;
        
        foreach (ManagementObject obj in mgmntObjs)
        {
            //log.Debug($"ClassPath.ClassName : {obj.ClassPath.ClassName}");

            foreach (PropertyData property in properties)
            {
                try
                {
                    var itemValue = obj.Properties[property.Name].Value?.ToString() ?? "(Unable to detect)";
                    osProperties.Add(property.Name, itemValue);
                }
                catch (Exception ex)
                {
                    _logger.LogError($"Error getting OS infos: {ex.Message}");
                }
            }
        }

        return osProperties;
    }
}
