namespace PcConfigurator.Ui.Helpers;

internal sealed class WindowsManager
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
