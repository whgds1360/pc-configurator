using PcConfigurator.Feature.PreMain.Connect;
using PcConfigurator.Windows.PreMain.ControlTemplates;

namespace PcConfigurator.Windows.PreMain;

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
        var ip = _dbProperties.FirstOrDefault(x => x.Tag == "ip")?.Value ?? string.Empty;
        var port = _dbProperties.FirstOrDefault(x => x.Tag == "port")?.Value ?? string.Empty;
        var db = _dbProperties.FirstOrDefault(x => x.Tag == "db")?.Value ?? string.Empty;
        var user = _dbProperties.FirstOrDefault(x => x.Tag == "username")?.Value ?? string.Empty;
        var password = _dbProperties.FirstOrDefault(x => x.Tag == "password")?.Value ?? string.Empty;

        var result = await ConnectToDatabaseHandler.ConnectToDataBase(server: ip, port: port, db: db, user: user, password: password);

        if (!result)
        {
            StatusBar.Content = "Неверная ссылка";
            StatusBar.Background = new SolidColorBrush(Color.Parse("#b8ab71"));
        }
    }
}