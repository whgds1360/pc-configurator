using PcConfigurator.Ui.Windows.Configurator;
using PcConfigurator.Ui.Helpers;
using PcConfigurator.Feature.Main.Configs;
using PcConfigurator.Shared.Models;

namespace PcConfigurator.Ui.Windows.Main;

internal partial class MainWindow : Window
{
    private Button? _currentConfiguration = null;

    public MainWindow()
    {
        InitializeComponent();
        Loaded += CreateButtons;
    }

    private async void CreateButtons(object? sender, RoutedEventArgs e)
    {
        Configuration.Children.Clear();

        List<Config> configs;

        try { configs = await GetConfigs.GetAllConfiguration(); }
        catch (Exception error) { ConfigStatus.Content = $"Ошибка загрузки конфигураций: {error.Message}"; return; }


        foreach (var config in configs)
        {
            Button btn = new();
            btn.Content = config.Name;
            btn.Tag = config.Id;
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

    private async void DeleteHandler(object? sender, RoutedEventArgs e)
    {
        if (_currentConfiguration is null) return;
        
        var selected = _currentConfiguration;
        if (selected.Tag is not int id) return;

        var button = (Button)sender!;
        button.IsEnabled = false;
        
        var result = await DeleteConfigs.DeleteOneConfiguration(id: id);    

         

        button.IsEnabled = true;
    }
}