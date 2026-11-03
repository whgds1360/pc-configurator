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

internal class DbInfoTemplate
{
    internal string Key { get; set; } = string.Empty;
    internal string Value { get; set; } = string.Empty;
}

internal class PropetryListTemplate
{
    internal string Name { get; set; } = string.Empty;
    internal string Tag { get; set; } = string.Empty;
}

internal partial class AdminWindow : Window
{
    internal string SelectTable { get; set; } = string.Empty;
    private List<MetaData> TablesMeta { get; set; } = new();
    internal List<string> TablesName { get; set; } = new();

    private List<string> placeholders = new();
    private Dictionary<string, string> parameters = new();

    public AdminWindow()
    {
        initTablesCombo();
        InitializeComponent();
        DataContext = this; 
        GetDbInfo();
    }

    private List<TextBox> GetTexboxes()
    {
        var textBoxes = PropertyList
            .GetVisualDescendants()
            .OfType<TextBox>()
            .ToList();

        return textBoxes;
    }

    private Dictionary<string, List<string>> GetValues()
    {   
        Dictionary<string, List<string>> data = new();

        data["Valid"] = new List<string>();
        data["Problem"] =  new List<string>();

        foreach (var textbox in GetTexboxes())
        {
            if (!string.IsNullOrEmpty(textbox.Text))
            {
                data["Valid"].Add(textbox.Text.Trim());
            }
            else
            {
                var problem_value = textbox?.Tag?.ToString();

                if (problem_value is not null)
                {
                    data["Problem"].Add(problem_value);
                }
            }
        }

        return data;
    }

    private Dictionary<string, object> CreateParams(List<string> values)
    {
        var placeholders = new List<string>();
        var parameters = new Dictionary<string, string>();

            for (int i = 0; i < values.Count; i++)
            {   
                placeholders.Add($"@p{i}");
                parameters[$"p{i}"] = values[i];
            }

        return new Dictionary<string, object>
        {
            { "placeholders", placeholders },
            { "parameters", parameters}
        };
    }

    private void DeleteButtonHandler(object sender, RoutedEventArgs e)
    {
        var data = GetValues();

        if (data["Valid"].Count > 1)
        {
           var parameters = CreateParams(data["Valid"]);
        }
        else if (data["Valid"].Count == 1)
        {
            using (var connect = DataBaseManager.GetConnection())
            {
                connect.Execute("DELETE FROM @table WHERE id = @id;" , new{table = SelectTable, id = data["Valid"][0]});
            }
        }
        else
        {
            StatusBar.Content = $"Нету данных по которым можно понять, что именно удалять.";
            StatusBar.Foreground = new SolidColorBrush(Color.Parse("#f80000"));
        }
    }

    private void AddButtonHandler(object sender, RoutedEventArgs e)
    {
        var data = GetValues();

        if (data["Valid"].Count == 0)
        {
            StatusBar.Content = "Нет данных для вставки.";
            StatusBar.Foreground = new SolidColorBrush(Color.Parse("#f80000"));
            return;
        }

        using var connect = DataBaseManager.GetConnection();
        try
        {
            var parameters = CreateParams(data["Valid"]);

            connect.Execute($"INSERT INTO \"{SelectTable}\" VALUES ({string.Join(", ", placeholders)})", parameters);

            StatusBar.Content = "Запись добавлена!";
            StatusBar.Foreground = new SolidColorBrush(Color.Parse("#00a000"));
        }
        catch (Exception error)
        {
            StatusBar.Content = $"Ошибка записи в БД: {error.Message}";
            StatusBar.Foreground = new SolidColorBrush(Color.Parse("#f80000"));
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

        foreach (var table in TablesMeta)
        {
            TablesName.Add(table.TableName ?? "");
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