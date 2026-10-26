using Avalonia.Controls;
using PcConfigurator.shared.DataBase;

namespace PcConfigurator.windows.Admin;


public class DbInfoTemplate
{
    public string Key { get; set; } = string.Empty;
    public string Value { get; set; } = string.Empty;
}

public partial class AdminWindow : Window
{
    public AdminWindow()
    {
        InitializeComponent();
    }

    private void GetDbInfo()
    {
        DataBaseManager.
    }
}