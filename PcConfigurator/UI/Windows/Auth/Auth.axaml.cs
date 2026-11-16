using PcConfigurator.Ui.Windows.Main;
using PcConfigurator.Ui.Windows.Admin;
using PcConfigurator.Shared;


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

        StatusText.Foreground = new SolidColorBrush(Color.Parse("#991f25"));
        StatusText.Text = "НЕВЕРНЫЕ ДАННЫЕ ДЛЯ ВХОДА";

    }
}