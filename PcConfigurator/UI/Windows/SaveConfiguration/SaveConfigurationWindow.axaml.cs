using PcConfigurator.Ui.Windows.Main;
using PcConfigurator.Windows.Configurator;
using PcConfigurator.Feature.SaveConfiguration.Requests;
using PcConfigurator.Ui.Helpers;

namespace PcConfigurator.Ui.Windows.SaveConfiguration;

internal partial class SaveConfigurationWindow : Window
{
    private readonly int _cpuId;
    private readonly int _gpuId;
    private readonly int _ramId;
    private readonly int _motherboardId;
    private readonly int _powerUnitId;
    private readonly int _pcCaseId;
    private readonly int _coolerId;

    public SaveConfigurationWindow()
    {
        InitializeComponent();
    }

    public SaveConfigurationWindow(
        int cpuId,
        int gpuId,
        int ramId,
        int motherboardId,
        int powerUnitId,
        int pcCaseId,
        int coolerId)
    {
        _cpuId         = cpuId;
        _gpuId         = gpuId;
        _ramId         = ramId;
        _motherboardId = motherboardId;
        _powerUnitId   = powerUnitId;
        _pcCaseId      = pcCaseId;
        _coolerId      = coolerId;

        InitializeComponent();
    }

    private string? GetConfigName()
    {
        if (string.IsNullOrEmpty(ConfigNameTextBlock.Text))
        {
            ActivateErrorBorder();

            return null;
        }

        return ConfigNameTextBlock.Text;
    }

    private void ActivateErrorBorder()
    {
        ErrorBorder.BorderThickness = new Thickness(3);
        ErrorBorder.BorderBrush = new SolidColorBrush(Color.Parse("#921111"));
    }

    private void SendSaveRequest(object? sender, RoutedEventArgs e)
    {
        var configName = GetConfigName();
        if (string.IsNullOrEmpty(configName)) return;

        var result = SaveRequest.AcceptConfiguration(configName: configName,
                                        cpuId: _cpuId,
                                        gpuId: _gpuId,
                                        motherboardId: _motherboardId,
                                        powerUnitId: _powerUnitId,
                                        ramId: _ramId,
                                        pcCaseId: _pcCaseId,
                                        coolerId: _coolerId);

        if (result)
        {
            WindowsManager.changeWindow(closingWindow: this, newWindow: new MainWindow());
        }
        else
        {
            ActivateErrorBorder();
        }
    }

    private void CancelButtonHandler(object? sender, RoutedEventArgs e)
    {
        WindowsManager.changeWindow(closingWindow: this, newWindow: new ConfiguratorWindow());
    }
}