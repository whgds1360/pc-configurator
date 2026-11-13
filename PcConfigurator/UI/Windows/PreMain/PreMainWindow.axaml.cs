using PcConfigurator.Feature.PreMain.Connect;

namespace PcConfigurator.Windows.PreMain;

internal partial class PreMainWindow : Window
{
    public PreMainWindow()
    {
        InitializeComponent();
    }

    private async void ConnectHandler(object sender, RoutedEventArgs e)
    {   
        ConnectToDatabaseHandler.ConnectToDataBase()
    }
}