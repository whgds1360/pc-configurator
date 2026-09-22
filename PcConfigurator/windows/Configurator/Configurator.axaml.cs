using System.Collections.Generic;
using System.Linq;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Dapper;
using PcConfigurator.helpers.TableStructures;
using PcConfigurator.shared.DataBase;

namespace PcConfigurator.windows.Configurator;

internal partial class ConfiguratorWindow : Window
{
    public List<Cpu> Cpus { get; set; } = new();
    public List<Gpu> Gpus { get; set; } = new();
    public List<Ram> Rams { get; set; } = new();
    public List<Motherboard> Motherboards { get; set; } = new();
    public List<PowerUnit> Powerunits { get; set; } = new();
    public List<PcCase> PcCases { get; set; } = new();
    public List<Cooler> Coolers { get; set; } = new();

    public Cpu? SelectedCpu { get; set; }
    public Gpu? SelectedGpu { get; set; }
    public Ram? SelectedRam { get; set; }
    public Motherboard? SelectedMotherboard { get; set; }
    public PowerUnit? SelectedPowerunit { get; set; }
    public PcCase? SelectedPcCase { get; set; }
    public Cooler? SelectedCooler { get; set; }

    public ConfiguratorWindow()
    {
        DataContext = this;
        ListsOfHardwareInit();
        InitializeComponent();
    }

    private void ListsOfHardwareInit()
    {
        using (var connect = DataBaseManager.GetConnection())
        {
            Cpus = connect.Query<Cpu>("SELECT * FROM cpus").ToList();
            Gpus = connect.Query<Gpu>("SELECT * FROM gpus").ToList();
            Rams = connect.Query<Ram>("SELECT * FROM rams").ToList();
            Motherboards = connect.Query<Motherboard>("SELECT * FROM motherboards").ToList();
            Powerunits = connect.Query<PowerUnit>("SELECT * FROM powerunits").ToList();
            PcCases = connect.Query<PcCase>("SELECT * FROM pccases").ToList();
            Coolers = connect.Query<Cooler>("SELECT * FROM coolers").ToList();
        }
    }

    private void ChangedHandler(object sender, RoutedEventArgs e)
    {
        var target = sender as ComboBox;

        if (target is null) return;

        switch (target.Tag)
        {
            case "Cpu" :

                if (SelectedCpu is null) return;

                CpuSoketLabel.Content = SelectedCpu.Socket;
                CpuCoresLabel.Content = SelectedCpu.Cores;
                CpuFreqLabel.Content = SelectedCpu.Freq;
                CpuTDPLabel.Content = SelectedCpu.TDP;
                CpuCacheLabel.Content = SelectedCpu.L3Cache;
                break;

            case "Gpu":

                if (SelectedGpu is null) return;

                GpuMemoryBusLabel.Content = SelectedGpu.MemoryBus;
                GpuMemoryTypeLabel.Content = SelectedGpu.MemoryType;
                GpuPCIeLabel.Content = SelectedGpu.PCIe;
                GpuTDPLabel.Content = SelectedGpu.TDP;
                GpuVRAMLabel.Content = SelectedGpu.VRAM;

                break;

            case "Ram": 

                if (SelectedRam is null) return;

                RamTypeLabel.Content = SelectedRam.Type;
                RamFreqLabel.Content = SelectedRam.Freq;
                RamTimingsLabel.Content = SelectedRam.Timings;

                break;

            case "MotherBoard":

                if (SelectedMotherboard is null) return;

                MotherboardChipsetLabel.Content = SelectedMotherboard.ChipSet;
                MotherboardDDrTypeLabel.Content = SelectedMotherboard.DdrType;
                MotherboardFormFactorLabel.Content = SelectedMotherboard.FormFactor;
                MotherboardM2SlotsLabel.Content = SelectedMotherboard.M2slots;
                MotherboardPCIeLabel.Content = SelectedMotherboard.PCIeVersion;
                MotherboardRamSlotsLabel.Content = SelectedMotherboard.RamSlots;
                MotherboardSocketLabel.Content = SelectedMotherboard.Socket;
                MotherboardUSB2Label.Content = SelectedMotherboard.Usb2Ports;
                MotherboardUSB3SlotsLabel.Content = SelectedMotherboard.Usb3Ports;

                break;

            case "PowerUnit":

                if (SelectedPowerunit is null) return;

                PowerUnitCertificateLabel.Content = SelectedPowerunit.Certificate;
                PowerUnitPowerLabel.Content = SelectedPowerunit.Power;

                break;

            case "Cooler":

                if (SelectedCooler is null) return;

                CoolerSocketLabel.Content = SelectedCooler.Socket;
                CoolerTdpCoolingLabel.Content = SelectedCooler.TdpCooling;

                break;

            case "PcCase":

                if (SelectedPcCase is null) return;

                PcCaseSizeTypeLabel.Content = SelectedPcCase.SizeType;

                break;

        }
        
    }
}