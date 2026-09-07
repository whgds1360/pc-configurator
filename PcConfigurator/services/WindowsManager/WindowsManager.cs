using System.Configuration;
using PcConfigurator.windows.Main;
using PcConfigurator.windows.PreMain;
using Avalonia.Controls;

namespace PcConfigurator.services.WindowsManager;

internal class MainWindowsManager
{
    public static void changeWindow(Window newWindow)
    {
        newWindow.Show();
    }

    public static void changeWindow(Window closingWindow, Window newWindow)
    {
        closingWindow.Close();
        newWindow.Show();
    }
}
