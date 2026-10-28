using System.Collections.Generic;
using Avalonia.Controls;
using Avalonia.Interactivity;
using PcConfigurator.shared.DataBase;

namespace PcConfigurator.windows.Admin;


public class DbInfoTemplate
{
    public string Key { get; set; } = string.Empty;
    public string Value { get; set; } = string.Empty;
}

public partial class AdminWindow : Window
{
    object? SelectTable { get; set; } = null;
    object[] Tables { get; set; } = [];

    public AdminWindow()
    {
        InitializeComponent();
        GetDbInfo();
    }

    private void initTablesCombo()
    {
        
    }

    private void ChangedTableHandler(object sender, RoutedEventArgs e)
    {
        
    }

    private void GetDbInfo()
    {
        var data = DataBaseManager.GetInfo();

        var datalist = new List<DbInfoTemplate>
        {
          new DbInfoTemplate(){ Key = "iP сервера:", Value = data.TryGetValue("ip", out var ip) ? ip : "Неизвестно"},
          new DbInfoTemplate(){ Key = "Порт:", Value = data.TryGetValue("port", out var port) ? port : "Неизвестно"},
          new DbInfoTemplate(){ Key = "Имя БД:", Value = data.TryGetValue("db", out var db) ? db : "Неизвестно"},
          new DbInfoTemplate(){ Key = "Подключено под:", Value = data.TryGetValue("user", out var user) ? user : "Неизвестно"}  
        };

        DbInfoList.ItemsSource = datalist;
    }
}