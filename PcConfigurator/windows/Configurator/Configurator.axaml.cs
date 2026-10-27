using System.Collections.Generic;
using System.Linq;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Dapper;
using PcConfigurator.windows.SaveConfiguration;
using PcConfigurator.services.WindowsManager;
using PcConfigurator.helpers.TableStructures;
using PcConfigurator.shared.DataBase;
using PcConfigurator.windows.Configurator.Helpers;
using Avalonia.Media;
using PcConfigurator.windows.Main;
using System;

namespace PcConfigurator.windows.Configurator;

enum Status
{
    compatible = 0,
    incompatible = 1,
}

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

    private async void AiQuery(object sender, RoutedEventArgs e)
    {
        AIManager.AgenInit(apiKey: "",
        model: "openai/gpt-oss-120b",
        temperature: 0.5,
        maxtokensonresp: 1000);
        try
        {
            var response = await AIManager.SendQuery(prompt:$"""

                Ты — опытный эксперт по сборке ПК. Оцени представленную сборку на наличие «бутылочного горлышка» (bottleneck).

                **Комплектующие:**
                {SelectedCpu?.Name}, {SelectedGpu?.Name}, {SelectedMotherboard?.Name}, {SelectedPcCase?.Name}, {SelectedPowerunit?.Name},
                {SelectedRam?.Name}, {SelectedCooler?.Name}

                **Что нужно сделать:**
                1. **Общая оценка: X/10** — насколько сбалансирована сборка (10 = идеальный баланс, 1 = критические несоответствия).
                2. **Комментарий по каждому компоненту** (1–2 предложения): уместен ли он в этой сборке, не создаёт ли узкое место, нет ли избыточности.
                3. **Главное бутылочное горлышко** — укажи, что сильнее всего ограничивает систему, и в каких задачах (игры, рендер, работа).
                4. **Рекомендации** — что заменить/добавить в первую очередь (кратко, по приоритету).

                **Требования:**
                - Учитывай баланс CPU↔GPU, достаточность БП, объём и скорость RAM, охлаждение.
                - Пиши по делу, без воды. **Максимум 300 слов.**
                - Формат: короткие блоки с заголовками, оценка в начале.  
             """);
            AiResponse.Text = response;
        }
        catch (Exception error)
        {
            AiResponse.Text = $"Ошибка запроса: {error}";
        }
    } 

    private void SaveButtonHandler(object sender, RoutedEventArgs e)
    {
        if (SelectedCpu is null ||  SelectedGpu is null || SelectedRam is null ||
            SelectedMotherboard is null || SelectedPowerunit is null || SelectedPcCase is null ||
            SelectedCooler is null)
        {
            StatusBar.Content = "Сборка не завершена!";
            StatusBar.Foreground = new SolidColorBrush(Color.Parse("#540989"));
            
            return;
        }

        var nextWindows = new SaveConfigurationWindow(
            cpuId:         SelectedCpu!.Id,
            gpuId:         SelectedGpu!.Id,
            ramId:         SelectedRam!.Id,
            motherboardId: SelectedMotherboard!.Id,
            powerUnitId:   SelectedPowerunit!.Id,
            pcCaseId:      SelectedPcCase!.Id,
            coolerId:      SelectedCooler!.Id
        );
        MainWindowsManager.changeWindow(closingWindow: this, newWindow: nextWindows);
    }

    private void CancelButtonHandler(object sender, RoutedEventArgs e)
    {
        var nextWindows = new MainWindow();
        
        MainWindowsManager.changeWindow(closingWindow: this, newWindow: nextWindows);
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

    private void CheckCompatilibityHandler(object sender, RoutedEventArgs e)
    {
        if (SelectedCpu is null ||  SelectedGpu is null || SelectedRam is null ||
            SelectedMotherboard is null || SelectedPowerunit is null || SelectedPcCase is null ||
            SelectedCooler is null)
        {
            StatusBar.Content = "Сборка не завершена!";
            StatusBar.Foreground = new SolidColorBrush(Color.Parse("#540989"));
            
            return;
        }

        changeStatusBar(CompatibilityResult: CompatibilityCheck());

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

                RamTypeLabel.Content = SelectedRam.DdrType;
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

    private void changeStatusBar(object[] CompatibilityResult)
    {
        switch (CompatibilityResult[0])
        {
            case Status.compatible: 

            StatusBar.Content = "Совместимо";
            StatusBar.Foreground = new SolidColorBrush(Color.Parse("#288016"));
            CommentStatusBar.Content = string.Empty;

            break;

            case Status.incompatible:

            StatusBar.Content = "Не совместимо";
            StatusBar.Foreground = new SolidColorBrush(Color.Parse("#921111"));

            CommentStatusBar.Content = CompatibilityResult[1];

            break;
        
        }
    }

    private object[] CompatibilityCheck()
    {
        string message = "";
        Status status = Status.incompatible;
        
        if (!CompatibilityManager.CheckChipset(selectedMotherboard: SelectedMotherboard, selectedCpu: SelectedCpu))
        {
            message += "- Чипсет материнской платы и процессора НЕСОВМЕСТИМЫ\n";
        }

        if (!CompatibilityManager.CheckPowerTDP(selectedGpu: SelectedGpu, selectedCpu: SelectedCpu, selectedPowerUnit: SelectedPowerunit))
        {
            message += "- Кулер не подходит по TDP для выбранного процессора\n";
        }

        if (!CompatibilityManager.CheckDdrType(selectedMotherboard: SelectedMotherboard, selectedRam: SelectedRam))
        {
            message += "- Тип DDR материнской платы и RAM НЕСОВМЕСТИМЫ\n";
        }

        if (!CompatibilityManager.CheckFormFactor(selectedMotherboard: SelectedMotherboard, selectedPccase: SelectedPcCase))
        {
            message += "- Форм-фактор материнской платы и корпуса НЕСОВМЕСТИМЫ\n";
        }

        if (!CompatibilityManager.CheckGpuCables(selectedGpu: SelectedGpu, selectedPowerUnit: SelectedPowerunit))
        {
            message += "- Блока питания недостаточно для питания видеокарты\n";
        }

        if (!CompatibilityManager.CheckSoketCooling(selectedCooler: SelectedCooler, selectedMotherboard: SelectedMotherboard))
        {
            message += "- Сокет кулера не подходит к сокету процессора\n";
        }

        if (string.IsNullOrEmpty(message))
        {
            status = Status.compatible;
            message = "Все компоненты совместимы";
        }

        return [status, message];
    }   
}