using System;
using System.Diagnostics;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Dapper;
using PcConfigurator.windows.Main;
using PcConfigurator.services.WindowsManager;
using PcConfigurator.shared.DataBase;
using Avalonia.Media;
using Avalonia;

namespace PcConfigurator.windows.SaveConfiguration;

public partial class SaveConfigurationWindow : Window
{
    private readonly int _cpuId;
    private readonly int _gpuId;
    private readonly int _ramId;
    private readonly int _motherboardId;
    private readonly int _powerUnitId;
    private readonly int _pcCaseId;
    private readonly int _coolerId;

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

    public void AcceptConfiguration()
    {

        try
        {
            var _name = getConfigName();

            if (_name is null)
            {
                return;
            }

            using var connect = DataBaseManager.GetConnection();
            var newId = connect.Execute(
                @"INSERT INTO configs
                      (Name, CpuId, GpuId, MotherboardId, PowerUnitId, RamId, PcCaseId, CoolerId)
                  VALUES
                      (@Name, @CpuId, @GpuId, @MotherboardId, @PowerUnitId, @RamId, @PcCaseId, @CoolerId)
                  RETURNING Id;",
                new
                {
                    Name = _name,
                    CpuId         = _cpuId,
                    GpuId         = _gpuId,
                    MotherboardId = _motherboardId,
                    PowerUnitId   = _powerUnitId,
                    RamId         = _ramId,
                    PcCaseId      = _pcCaseId,
                    CoolerId      = _coolerId
                });
        }

        catch (Exception error)
        {
            Debug.WriteLine($"Ошибка при сохранении конфига: {error.Message}");
        }
    }

    private string? getConfigName()
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
        AcceptConfiguration();
        var nextWindows = new MainWindow();
        MainWindowsManager.changeWindow(closingWindow: this, newWindow: nextWindows);
    }
}