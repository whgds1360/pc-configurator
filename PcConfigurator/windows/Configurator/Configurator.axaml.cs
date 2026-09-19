using System.Collections.Generic;
using System.Linq;
using Avalonia.Controls;
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
}