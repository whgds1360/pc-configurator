using PcConfigurator.Windows.Configurator;
using PcConfigurator.Ui.Helpers;
using PcConfigurator.Shared;
using PcConfigurator.Feature.Main.Configs;

namespace PcConfigurator.Windows.Main;

internal partial class MainWindow : Window
{
    private Button? _currentConfiguration = null;

    public MainWindow()
    {
        InitializeComponent();
        Loaded += CreateButtons;
    }

    private void CreateButtons(object? sender, RoutedEventArgs e)
    {
        Configuration.Children.Clear();

        var configs = GetConfigs.GetAllConfiguration();

        foreach (var config in configs)
        {
            Button btn = new();
            btn.Content = config.Name;
            btn.Classes.Add("Configuration");
            btn.Click += SelectHandler;
            
            Configuration.Children.Add(btn);
        }   
    }

    private void SelectHandler(object? sender, RoutedEventArgs e)
    {   
        if (sender is Button)
        {   
            _currentConfiguration?.Background = Brush.Parse("#3F4347");

            _currentConfiguration = (Button)sender;
            _currentConfiguration.Background = Brush.Parse("#2b0179");
        }
    }

    private void CreateHandler(object? sender, RoutedEventArgs e)
    {
        WindowsManager.changeWindow(closingWindow: this, newWindow: new ConfiguratorWindow());
    }

    private void DeleteHandler(object? sender, RoutedEventArgs e)
    {
        using(var connect = DataBaseManager.GetConnection())
        {   
            try 
            {
                if (_currentConfiguration is null) return;

                var affected = connect.Execute(
                    "DELETE FROM `configs` WHERE Name = @Name",
                    new { Name = _currentConfiguration.Content });

                if (affected == 0)
                {
                    Debug.WriteLine("Запись не найдена");
                    return;
                }

                Configuration.Children.Remove(_currentConfiguration);
                _currentConfiguration = null;
            }
            catch (Exception error)
            {
                Debug.WriteLine($"Ошибка удаления: {error}");
            }
        }
    }
}