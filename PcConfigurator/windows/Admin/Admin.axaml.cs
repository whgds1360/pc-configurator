using System.Collections.Generic;
using Avalonia.Controls;
using Avalonia.Interactivity;
using PcConfigurator.shared.DataBase;
using PcConfigurator.helpers.TableStructures;
using System.Linq;
using System.Diagnostics;
using Avalonia.VisualTree;
using Avalonia.Media;
using System;
using Dapper;

namespace PcConfigurator.windows.Admin;

enum Status
{
    ok,
    error
}

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
    internal string SelectTable { get; set; } = string.Empty;
    internal static List<MetaData> TablesMeta { get; set; } = new();
    internal List<string> TablesName { get; set; } = new();

    public AdminWindow()
    {
        initTablesCombo();
        InitializeComponent();
        DataContext = this; 
        GetDbInfo();
    }

    private void AddButtonHandler()
    {
        var textBoxs = PropertyList
            .GetVisualDescendants()
            .OfType<TextBox>()
            .ToList();

        var status = Status.ok;

        foreach (var textBox in textBoxs)
        {
            if (string.IsNullOrEmpty(textBox.Text))
            {   
                StatusBar.Content = "Не все поля заполнены!";
                StatusBar.Foreground = new SolidColorBrush(Color.Parse("#f80000"));

                status = Status.error;
                break;
            }
        }

        if (status == Status.ok)
        {
            using (var connect = DataBaseManager.GetConnection())
            {
                try
                {
                    var data = new
                    {
                        
                    }

                    connect.Execute(sql:@"
                        INSERT INTO @table
                        VALUES @values
                    ");

                    
                }
                catch (Exception error)
                {
                    StatusBar.Content = $"Ошибка записи в БД: {error.Message}";
                }
            }
        }
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

        Debug.WriteLine($"Из контекста: {TablesMeta}");
        foreach (var table in TablesMeta)
        {
            if (table.TableName is null) Debug.WriteLine("Пустота ёпт");

            TablesName.Add(table.TableName ?? "");
        }
        Debug.WriteLine($"{TablesName}");        
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