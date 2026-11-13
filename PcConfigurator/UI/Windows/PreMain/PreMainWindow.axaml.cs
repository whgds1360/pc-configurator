using PcConfigurator.services.WindowsManager;
using PcConfigurator.windows.Auth;
using PcConfigurator.Shared;


namespace PcConfigurator.windows.PreMain;

internal partial class PreMainWindow : Window
{
    public PreMainWindow()
    {
        InitializeComponent();
    }

    private async void ConnectHandler(object sender, RoutedEventArgs e)
    {   
        if (DataBaseManager.GetStatus()== DbConnectionStatus.Connected)
        {
            var connectUrl = DataBaseManager.CreateUrlConnection(server:Server.Text ?? String.Empty, 
                                                                    port:Port.Text ?? String.Empty, 
                                                                    db:DbName.Text ?? String.Empty, 
                                                                    user:Username.Text ?? String.Empty, 
                                                                    password:Password.Text ?? String.Empty);

            if (await DataBaseManager.TryInitConnection(url: connectUrl))
            {   
                DataBaseManager.ChangeStatus(newStatus: OrderStatus.Connect);

                using (var db = new ApplicationContext())
                {
                    await db.Database.EnsureCreatedAsync();
                };

                var nextWindow = new AuthWindow();
                MainWindowsManager.changeWindow(closingWindow: this, newWindow: nextWindow);
            }
            else
            {
                DataBaseManager.ChangeStatus(newStatus:OrderStatus.NoConnect);
                Status.Content = "Неверная ссылка";
                Status.Background = new SolidColorBrush(Color.Parse("#b8ab71"));
            }
        }
    }
}