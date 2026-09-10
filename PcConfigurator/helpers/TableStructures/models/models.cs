using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace PcConfigurator.helpers.TableStructures;

public class Config
{
    [Key]
    public string? Name { get; set; }
    public string? CpuName { get; set; }

    [ForeignKey(nameof(CpuName))]
    public Cpu? Cpu { get; set; }

    public string? GpuName { get; set; }

    [ForeignKey(nameof(GpuName))]
    public Gpu? Gpu { get; set; }

    public string? MotherboardName { get; set; }

    [ForeignKey(nameof(MotherboardName))]
    public Motherboard? Motherboard { get; set; }

    public string? PowerUnitName { get; set; }

    [ForeignKey(nameof(MotherboardName))]
    public PowerUnit? PowerUnit { get; set; }

    public string? RamName { get; set; }

    [ForeignKey(nameof(RamName))]
    public Ram? Ram { get; set; } = new();
}

public class Cpu
{
    [Key]
    public string? Name { get; set; }
    public string? Soket { get; set; }
    public string? Theards { get; set; }
    public int TDP { get; set; }
    public string? Freq { get; set; }
    public string? L3Chache { get; set; }

    public List<Config> Configs { get; set; } = new();
}

public class Gpu
{
    [Key]
    public string? Name { get; set; }
    public string? VRAM { get; set; }
    public string? MemoryType { get; set; }
    public string? MemoryBus { get; set; }
    public int TDP { get; set; }
    public string? PCIe { get; set; }

    public List<Config> Configs { get; set; } = new();
}

public class Ram
{
    [Key]
    public string? Name { get; set; }
    public string? Type { get; set; }
    public string? Volume { get; set; }
    public string? Freq { get; set; }
    public int Timings { get; set; }
    public string? Voltage { get; set; }
    public List<Config> Configs { get; set; } = new();
}

public class Motherboard
{
    [Key]
    public string? Name { get; set; }
    public string? Soket { get; set; }
    public string? ChipSet { get; set; }
    public string? RamSlots { get; set; }
    public int M2slots { get; set; }
    public string? PCIe { get; set; }

    public List<Config> Configs { get; set; } = new();
}

public class PowerUnit
{
    [Key]
    public string? Name { get; set; }
    public string? Power { get; set; }
    public string? Sertificate { get; set; }
    public string? Modularity { get; set; }

    public List<Config> Configs { get; set; } = new();
}