using Avalonia.Controls;
using Dapper;
using PcConfigurator.shared.DataBase;
using System;
using PcConfigurator.windows.Configurator;
using System.Diagnostics;

namespace PcConfigurator.windows.SaveConfiguration;

public partial class SaveConfigurationWindow : Window
{
    public SaveConfigurationWindow()
    {
        InitializeComponent();
    }

    public void SendSaveRequest()
    {
        var Cpu = ConfiguratorWindow.SelectedCpu?.Id;
        var Gpu = ConfiguratorWindow.SelectedGpu?.Id;
        var Ram = ConfiguratorWindow.SelectedRam?.Id;
        var PcCase = ConfiguratorWindow.SelectedPcCase?.Id;
        var Cooler = ConfiguratorWindow.SelectedCooler?.Id;
        var Motherboard = ConfiguratorWindow.SelectedMotherboard?.Id;
        var PowerUnit = ConfiguratorWindow.SelectedPowerunit?.Id;

        using (var connect = DataBaseManager.GetConnection())
        {
            try
            {
                if (connect.Execute(@"INSERT INTO сonfigs (CpuId, GpuId, MotherboardId, PowerUnitId, RamId, PcCaseId, CoolerId)
                    VALUES (@Cpu, @Gpu, @Motherboard, @PowerUnit, @Ram, @PcCase, @Cooler)",
                    new{
                        Cpu = Cpu, Gpu = Gpu, Motherboard = Motherboard, PowerUnit = PowerUnit,
                        Ram = Ram, PcCase = PcCase, Cooler = Cooler
                    }) == 0)
                {
                    throw new ArgumentException("Ошибка сохранения конфига в БД");
                }
            }
            catch (Exception error)
            {
                Debug.WriteLine($"Ошибка при сохранении: {error}");
            }
        }
    }
}