namespace PcConfigurator.services.WindowsManager;

internal class MainWindowsManager
{
    internal static void changeWindow(Window newWindow)
    {
        newWindow.Show();
    }
    internal static void changeWindow(Window closingWindow, Window newWindow)
    {
        newWindow.Show();
        closingWindow.Close();
    }
}
