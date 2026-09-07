using System.Configuration;
using PcConfigurator.windows.Main;
using PcConfigurator.windows.PreMain;
using Avalonia.Controls;

namespace PcConfigurator.shared.WindowsManager;

internal class MainWindowsManager
{
    private void changeWindow(Window newWindow)
    {
        newWindow.Show();
    }

    private void changeWindow(Window closingWindow, Window newWindow)
    {
        closingWindow.Close();
        newWindow.Show();
    }
}
