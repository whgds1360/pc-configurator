using Avalonia.Controls;
using Mysqlx.Connection;

namespace PcConfigurator.windows.Main;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    public static void Close(Window window)
    {
        window.Close();
    }
}