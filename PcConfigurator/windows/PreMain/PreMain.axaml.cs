using Avalonia.Controls;
using Avalonia.Interactivity;
using PcConfigurator.shared.DataBase;
using PcConfigurator.windows.Main;
using Avalonia.Media;
using System;

namespace PcConfigurator.windows.PreMain;

public partial class PreMain : Window
{
    public PreMain()
    {
        InitializeComponent();
    }

    private void ConnectHandler(object sender, RoutedEventArgs e)
    {   
        if (DataBaseManager.currentStatus != OrderStatus.Connect)
        {
            var connectUrl = DataBaseManager.CreateUrlConnection(server:Server.Text ?? String.Empty, 
                                                                    port:Port.Text ?? String.Empty, 
                                                                    db:DbName.Text ?? String.Empty, 
                                                                    user:Username.Text ?? String.Empty, 
                                                                    password:Password.Text ?? String.Empty);

            if (DataBaseManager.TryInitConnection(url: connectUrl))
            {   
                DataBaseManager.ChangeStatus(newStatus:OrderStatus.Connect);

                var nextWindow = new MainWindow();
                nextWindow.Show();
                this.Close();
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