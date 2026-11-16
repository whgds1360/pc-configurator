using PcConfigurator.Windows.Main;
using PcConfigurator.Windows.Admin;


namespace PcConfigurator.Ui.Windows.Auth;

internal partial class AuthWindow : Window
{
    public AuthWindow()
    {
        InitializeComponent();
    }

    private async void AuthButtonHanlder(object sender, RoutedEventArgs e)
    {

        var login = LoginTextBox.Text;
        var password = PasswordTextBox.Text;

        using (var connect = DataBaseManager.GetConnection())
        {
            try
            {
                var response = await connect.QuerySingleOrDefaultAsync<string>("SELECT role FROM users WHERE login = @login AND password = @password", new {login = login, password = password});

                if (response != null)
                {
                    switch (response)
                    {
                        case "admin":

                            MainWindowsManager.changeWindow(closingWindow: this, newWindow: new AdminWindow());

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

            catch (Exception error)
            {
                Debug.WriteLine($"Ошибка в окне авторизации: {error}");
            }
        }
    }
}