using System.Collections.Generic;
using System.Linq;
using Avalonia.Controls;
using Dapper;
using PcConfigurator.helpers.TableStructures;
using PcConfigurator.shared.DataBase;

namespace PcConfigurator.windows.Configurator;

internal partial class ConfiguratorWindow : Window
{
    private List<Cpu> cpus = new();
    private List<Cpu> gpus = new();
    private List<Cpu> rams = new();
    private List<Cpu> motherboards = new();
    private List<Cpu> powerunits = new();

    



    public ConfiguratorWindow()
    {
        InitializeComponent();

        ListsOfHadrwareInit();
    }

    private void ListsOfHadrwareInit()
    {
        using (var connect = DataBaseManager.GetConnection())
        {
            cpus = connect.Query<Cpu>("SELECT * FROM cpus").ToList();
            gpus = connect.Query<Cpu>("SELECT * FROM gpus").ToList();
            rams = connect.Query<Cpu>("SELECT * FROM rams").ToList();
            motherboards = connect.Query<Cpu>("SELECT * FROM motherboards").ToList();
            powerunits = connect.Query<Cpu>("SELECT * FROM powerunits").ToList();
        }
    }

}