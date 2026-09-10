using Avalonia.Controls;
using Avalonia.Interactivity;
using PcConfigurator.services.WindowsManager;
using PcConfigurator.windows.Configurator;

namespace PcConfigurator.windows.Main;

public partial class MainWindow : Window
{
    Button? currentConfiguration = null;

    public MainWindow()
    {
        InitializeComponent();
    }

    private void CreateHandler(object? sender, RoutedEventArgs e)
    {
        var nextWindow = new ConfiguratorWindow();

        MainWindowsManager.changeWindow(closingWindow: this, newWindow: nextWindow);
    }
}