using PcConfigurator.Feature.PreMain.Connect;
using PcConfigurator.Windows.Main;
using PcConfigurator.Ui.Helpers;
using PcConfigurator.Ui.Windows.PreMain.ControlTemplates;

namespace PcConfigurator.Ui.Windows.PreMain;

internal partial class PreMainWindow : Window
{
    private List<DbProperties> _dbProperties = new(); 

    public PreMainWindow()
    {
        InitializeComponent();
        Loaded += CreateItems;
    }

    private void CreateItems(object? sender, RoutedEventArgs e)
    {
        _dbProperties = new List<DbProperties>()
        {
            new DbProperties {Title = "iP сервера", Tag = "ip"},
            new DbProperties {Title = "Порт для подключения", Tag = "port"},
            new DbProperties {Title = "Имя БД", Tag = "db"},
            new DbProperties {Title = "Имя пользователя", Tag = "username"},
            new DbProperties {Title = "Пароль", Tag = "password"}
        };

        ConnectProperties.ItemsSource = _dbProperties;
    }
    
    private async void ConnectHandler(object sender, RoutedEventArgs e)
    {   
        var ip = _dbProperties.FirstOrDefault(v => v.Tag == "ip")?.Value ?? string.Empty;
        var port = _dbProperties.FirstOrDefault(v => v.Tag == "port")?.Value ?? string.Empty;
        var db = _dbProperties.FirstOrDefault(v => v.Tag == "db")?.Value ?? string.Empty;
        var user = _dbProperties.FirstOrDefault(v => v.Tag == "username")?.Value ?? string.Empty;
        var password = _dbProperties.FirstOrDefault(v => v.Tag == "password")?.Value ?? string.Empty;

        var result = await ConnectToDatabaseHandler.ConnectToDataBase(server: ip, port: port, db: db, user: user, password: password);

        if (result)
        {
            WindowsManager.changeWindow(closingWindow: this, newWindow: new MainWindow());
        }
        else
        {
            StatusBar.Content = "Неверная ссылка";
            StatusBar.Background = new SolidColorBrush(Color.Parse("#b8ab71"));
        }
    }
}