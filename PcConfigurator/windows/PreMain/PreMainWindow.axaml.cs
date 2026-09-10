using Avalonia.Controls;
using Avalonia.Interactivity;
using PcConfigurator.shared.DataBase;
using PcConfigurator.services.WindowsManager;
using PcConfigurator.windows.Main;
using Avalonia.Media;
using PcConfigurator.helpers.TableStructures;
using System;

namespace PcConfigurator.windows.PreMain;

public partial class PreMainWindow : Window
{
    public PreMainWindow()
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

                using var db = new ApplicationContext();

                var nextWindow = new MainWindow();
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