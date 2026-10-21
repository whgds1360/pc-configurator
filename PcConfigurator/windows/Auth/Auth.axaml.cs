using Dapper;
using PcConfigurator.shared.DataBase;
using PcConfigurator.services.WindowsManager;
using PcConfigurator.windows.Main;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Media;


namespace PcConfigurator.windows.Auth;

internal partial class AuthWindow : Window
{
    public AuthWindow()
    {
        InitializeComponent();
    }

    private void AuthButtonHanlder(object sender, RoutedEventArgs e)
    {

        var login = LoginTextBox.Text;
        var password = PasswordTextBox.Text;

        using (var connect = DataBaseManager.GetConnection())
        {
            var response = connect.QuerySingleOrDefault<string>(@"""
                SELECT role FROM users WHERE login = :login AND password = :password
            """, new {login = login, password = password});

            if (response != null)
            {
                switch (response)
                {
                    case "admin":

                        break;
                    
                    case "user":
                        MainWindowsManager.changeWindow(closingWindow: this, newWindow: new MainWindow());

                        break;
                }
            }

            else
            {
                StatusText.Foreground = new SolidColorBrush(Color.Parse("#991f25"));
                StatusText.Text = "НЕВЕРНЫЕ ДАННЫЕ ДЛЯ ВХОДА";
            }
        }
    }
}