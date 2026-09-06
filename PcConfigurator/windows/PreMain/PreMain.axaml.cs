using Avalonia.Controls;
using Avalonia.Media;
using Avalonia.Interactivity;
using PcConfigurator.shared.DataBase;
using PcConfigurator.windows.Main;

namespace PcConfigurator.windows.PreMain;

public partial class PreMain : Window
{
    public PreMain()
    {
        InitializeComponent();
    }

    private void ConnectHandler(object sender, RoutedEventArgs e)
    {
        if (DataBaseManager.TryInitConnection(url: DbURL.Text?? string.Empty))
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