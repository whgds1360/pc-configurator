using PcConfigurator.Feature.Auth;
using PcConfigurator.Shared;
using PcConfigurator.Ui.Helpers;
using PcConfigurator.Ui.Windows.Admin;
using PcConfigurator.Ui.Windows.Main;


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

        if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(password))
        {
            PanicStatusBar("Не все поля формы заполнены!");
            return;
        }

        var button = (Button)sender;
        button.IsEnabled = false;
        var (IsSuccess, Comment, Role) = await AuthResponse.CheckCorrectUserData(login: login, password: password);

        button.IsEnabled = true;
        if (!IsSuccess)
        {
            PanicStatusBar(Comment);
        }
        else
        {
            DataBaseManager.CurrentUser = login;
            switch (Role)
            {
                case "admin": 
                    WindowsManager.changeWindow(closingWindow: this, newWindow: new AdminWindow());
                    break;

                case "user":
                    WindowsManager.changeWindow(closingWindow: this, newWindow: new MainWindow());
                    break;

                default: 
                    PanicStatusBar(Comment);
                    break;
            }
        }
    }

    private void PanicStatusBar(string comment)
    {
        StatusText.Foreground = new SolidColorBrush(Color.Parse("#991f25"));
        StatusText.Text = comment;
    }
}