using PcConfigurator.Shared;
using PcConfigurator.Feature.Admin;
using PcConfigurator.Ui.Windows.Configurator;

namespace PcConfigurator.Ui.Windows.Admin;

internal partial class AdminWindow : Window
{
    internal string SelectTable { get; set; } = string.Empty;
    private List<MetaData> TablesMeta { get; set; } = new();
    internal List<string> TablesName { get; set; } = new();

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

    private void DeleteButtonHandler(object sender, RoutedEventArgs e)
    {
        var data = GetValues();

        var result = RecordManager.DeleteRecord(data:data, selectTable:SelectTable, out var comment);
        ChangeStatusBar(comment: comment, IsSuccess: result);
    }

    private void AddButtonHandler(object sender, RoutedEventArgs e)
    {
        var data = GetValues();
        var result = RecordManager.AddRecord(data:data, selectTable:SelectTable, out var comment);

        ChangeStatusBar(comment: comment, IsSuccess: result);
    }

    private void ChangeStatusBar(string comment, bool IsSuccess)
    {
        StatusBar.Content = comment;

        if (IsSuccess)
        {
            StatusBar.Foreground = new SolidColorBrush(Color.Parse("#00ff08"));
        }
        else
        {
            StatusBar.Foreground = new SolidColorBrush(Color.Parse("#f80000"));
        }
    }

    private void initTablesCombo()
    {
        var context = new ApplicationContext();
        
        TablesMeta = context.Model.GetEntityTypes()
            .Select(data => new MetaData{
                TableName = data.GetTableName(), 
                Properties =data.GetProperties().Select(p => p.GetColumnName()).ToList()
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

        var dataItems = new List<PropertyListTemplate>();

        foreach (var table in TablesMeta)
        {
            if (table.TableName == SelectTable)
            {
                if (table.Properties is null) continue;
                
                foreach (var prop in table.Properties)
                {
                    dataItems.Add(new PropertyListTemplate {Name = prop, Tag = prop});
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