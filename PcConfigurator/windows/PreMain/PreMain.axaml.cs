using Avalonia.Controls;
using Avalonia.Media;
using Avalonia.Interactivity;
using PcConfigurator.shared.DataBase;
using PcConfigurator.windows.Main;
using System.Net.Security;

namespace PcConfigurator.windows.PreMain;

public partial class PreMain : Window
{
    public PreMain()
    {
        InitializeComponent();
    }

    private void ConnectHandler(object sender, RoutedEventArgs e)
    {   
        var connectUrl = DataBaseManager.CreateUrlConnection(server:Server.Text, port:Port.Text, db:DbName.Text, user:Username.Text, password:Password.Text);

        if (DataBaseManager.TryInitConnection(url: connectUrl))
        {
            Status.Content = "Подключено";
            Status.Background = new SolidColorBrush(Color.Parse("#50996f"));

            var nextWindow = new MainWindow();
            nextWindow.Show();
            this.Close();
        }
        else
        {
            Status.Content = "Неверная ссылка";
            Status.Background = new SolidColorBrush(Color.Parse("#b8ab71"));
        }
    }
}