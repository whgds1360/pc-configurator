using System.ComponentModel.DataAnnotations;

namespace PcConfigurator.helpers.TableStructures;

public class Config
{
    [Key]
    public int Id { get; set; }
    public string? Name { get; set; }
    public int Age { get; set; }
}

public class Cpu
{  
    [Key]
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Soket { get; set; }
    public string? Theards { get; set; }
    public int TDP { get; set; }
    public string? Freq { get; set; }
    public string? L3Chache { get; set; }
}

public class Gpu
{
    [Key]
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? VRAM { get; set; }
    public string? MemoryType { get; set; }
    public string? MemoryBus { get; set; }
    public int TDP { get; set; }
    public string? PCIe { get; set; }
}

public class Ram
{
    [Key]
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Type { get; set; }
    public string? Volume { get; set; }
    public string? Freq { get; set; }
    public int Timings { get; set; }
    public string? Voltage { get; set; }
}

public class Motherboard
{
    [Key]
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Soket { get; set; }
    public string? ChipSet { get; set; }
    public string? RamSlots { get; set; }
    public int M2slots { get; set; }
    public string? PCIe { get; set; }
}

public class PowerUnit
{
    [Key]
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Power { get; set; }
    public string? Sertificate { get; set; }
    public string? Modularity { get; set; }
}