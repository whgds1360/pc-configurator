using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PcConfigurator.helpers.TableStructures;

public class Config
{
    [Key]
    public int Id { get; set; }

    [MaxLength(100)]
    public string Name { get; set; } = string.Empty;

    public int CpuId { get; set; }
    [ForeignKey(nameof(CpuId))]
    public Cpu Cpu { get; set; } = null!;

    public int GpuId { get; set; }
    [ForeignKey(nameof(GpuId))]
    public Gpu Gpu { get; set; } = null!;

    public int MotherboardId { get; set; }
    [ForeignKey(nameof(MotherboardId))]
    public Motherboard Motherboard { get; set; } = null!;

    public int PowerUnitId { get; set; }
    [ForeignKey(nameof(PowerUnitId))]
    public PowerUnit PowerUnit { get; set; } = null!;

    public int RamId { get; set; }
    [ForeignKey(nameof(RamId))]
    public Ram Ram { get; set; } = null!;
    
    public int? CoolerId { get; set; }
    [ForeignKey(nameof(CoolerId))]
    public Cooler? Cooler { get; set; }
}

public class Cpu
{
    [Key]
    public int Id { get; set; }

    [MaxLength(100)]
    public string Name { get; set; } = string.Empty;

    [MaxLength(50)]
    public string? Socket { get; set; }

    public int Cores { get; set; }
    
    public int TDP { get; set; }

    [Column(TypeName = "decimal(4,2)")]
    public decimal Freq { get; set; }

    public int L3Cache { get; set; }
}

public class Gpu
{
    [Key]
    public int Id { get; set; }

    [MaxLength(100)]
    public string Name { get; set; } = string.Empty;

    public int VRAM { get; set; }
    
    [MaxLength(50)]
    public string? MemoryType { get; set; }
    
    public int MemoryBus { get; set; }
    
    public int TDP { get; set; }
    
    [MaxLength(50)]
    public string? PCIe { get; set; }
}

public class Ram
{
    [Key]
    public int Id { get; set; }

    [MaxLength(100)]
    public string Name { get; set; } = string.Empty;

    [MaxLength(50)]
    public string? Type { get; set; }

    public int Volume { get; set; }
    
    public int Freq { get; set; }
    
    [MaxLength(50)]
    public string? Timings { get; set; }
    
    public int ModulesCount { get; set; }
}

public class Motherboard
{
    [Key]
    public int Id { get; set; }

    [MaxLength(100)]
    public string Name { get; set; } = string.Empty;

    [MaxLength(100)]
    public string UsbPorts { get; set; } = string.Empty;

    [MaxLength(100)]
    public string LanPorts { get; set; } = string.Empty;

    [MaxLength(50)]
    public string? Socket { get; set; }

    [MaxLength(50)]
    public string? DdrType { get; set; }

    [MaxLength(50)]
    public string? ChipSet { get; set; }

    public int RamSlots { get; set; }

    public int M2slots { get; set; }

    [MaxLength(50)]
    public string? PCIe { get; set; }
    
    [MaxLength(50)]
    public string? FormFactor { get; set; }
}

public class PowerUnit
{
    [Key]
    public int Id { get; set; }

    [MaxLength(100)]
    public string Name { get; set; } = string.Empty;

    public int Power { get; set; }

    [MaxLength(100)]
    public string? Certificate { get; set; }
}

public class CpuCompatibility
{
    [Key]
    public int Id { get; set; }

    public int CpuId { get; set; }
    [ForeignKey(nameof(CpuId))]
    public Cpu Cpu { get; set; } = null!;

    [MaxLength(50)]
    public string Chipset { get; set; } = string.Empty;
}

public class PsuConnector
{
    [Key]
    public int Id { get; set; }

    public int PowerUnitId { get; set; }
    [ForeignKey(nameof(PowerUnitId))]
    public PowerUnit PowerUnit { get; set; } = null!;

    [MaxLength(50)]
    public string ConnectorType { get; set; } = string.Empty;

    public int ConnectorCount { get; set; }
}

public class GpuPowerRequirement
{
    [Key]
    public int Id { get; set; }

    public int GpuId { get; set; }
    [ForeignKey(nameof(GpuId))]
    public Gpu Gpu { get; set; } = null!;

    [MaxLength(50)]
    public string ConnectorType { get; set; } = string.Empty;

    public int ConnectorCount { get; set; }
}

public class Cooler
{
    [Key]
    public int Id { get; set; }

    [MaxLength(100)]
    public string Name { get; set; } = string.Empty;

    [MaxLength(50)]
    public string Socket { get; set; } = string.Empty;

    public int TdpCooling { get; set; }

    public int Height { get; set; }
}