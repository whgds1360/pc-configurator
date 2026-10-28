using System.Collections.Generic;
using Avalonia.Controls;
using Avalonia.Interactivity;
using PcConfigurator.shared.DataBase;
using PcConfigurator.helpers.TableStructures;
using System.Linq;

namespace PcConfigurator.windows.Admin;


public class DbInfoTemplate
{
    public string Key { get; set; } = string.Empty;
    public string Value { get; set; } = string.Empty;
}

public class PropetryListTemplate
{
    public string Name { get; set; } = string.Empty;
    public string Tag { get; set; } = string.Empty;
}

public partial class AdminWindow : Window
{
    private string SelectTable { get; set; } = string.Empty;
    private static List<MetaData> TablesMeta { get; set; } = new();
    private List<string> TablesName { get; set; } = new();

    public AdminWindow()
    {
        initTablesCombo();
        InitializeComponent();
        DataContext = this; 
        GetDbInfo();
    }

    private void initTablesCombo()
    {
        var context = new ApplicationContext();

        TablesMeta = context.Model.GetEntityTypes()
            .Select(data => new MetaData{
                TableName = data.DisplayName(), 
                Properties =data.GetProperties().Select(p => p.Name).ToList()
                })
            .ToList();

        foreach (var table in TablesMeta)
        {
            if (table.TableName is null) continue;

            TablesName.Add(table.TableName);
        }        
    }

    private void ChangedTableHandler(object sender, RoutedEventArgs e)
    {
        if (SelectTable is null) return;

        var dataItems = new List<PropetryListTemplate>();

        foreach (var table in TablesMeta)
        {
            if (table.TableName == SelectTable)
            {
                if (table.Properties is null) continue;
                
                foreach (var prop in table.Properties)
                {
                    dataItems.Add(new PropetryListTemplate {Name = prop, Tag = prop});
                }
            }
        }

        PropertyList.ItemsSource =  dataItems;
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