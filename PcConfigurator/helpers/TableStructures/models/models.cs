using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

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

    public int PcCaseId { get; set; }
    [ForeignKey(nameof(PcCaseId))]
    public PcCase pcCase { get; set; } = null!;    

    public int CoolerId { get; set; }
    [ForeignKey(nameof(CoolerId))]
    public Cooler Cooler { get; set; } = null!;
}

public class Cpu
{
    [Key]
    public int Id { get; set; }

    [MaxLength(100)]
    public string Name { get; set; } = string.Empty;

    [MaxLength(50)]
    public string Socket { get; set; } = string.Empty;

    [Column(TypeName = "tinyint unsigned")]
    public int Cores { get; set; }
    
    [Column(TypeName = "smallint unsigned")]
    public int TDP { get; set; }

    [Column(TypeName = "decimal(3,2)")]
    public decimal Freq { get; set; }

    [Column(TypeName = "tinyint unsigned")]
    public int L3Cache { get; set; }
}

public class Gpu
{
    [Key]
    public int Id { get; set; }

    [MaxLength(100)]
    public string Name { get; set; } = string.Empty;

    [Column(TypeName = "tinyint unsigned")]
    public int VRAM { get; set; }
    
    [MaxLength(50)]
    public string MemoryType { get; set; } = string.Empty;
    
    [Column(TypeName = "smallint unsigned")]
    public int MemoryBus { get; set; }
    
    [Column(TypeName = "smallint unsigned")]
    public int TDP { get; set; }
    
    [MaxLength(50)]
    public string PCIe { get; set; } = string.Empty;
}

public class Ram
{
    [Key]
    public int Id { get; set; }

    [MaxLength(100)]
    public string Name { get; set; } = string.Empty;

    [Column(TypeName = "tinyint unsigned")]
    public int DdrType { get; set; }
    
    [Column(TypeName = "smallint unsigned")]
    public int Freq { get; set; }
    
    [MaxLength(50)]
    public string Timings { get; set; } = string.Empty;
}

public class Motherboard
{
    [Key]
    public int Id { get; set; }

    [MaxLength(100)]
    public string Name { get; set; } = string.Empty;

    [Column(TypeName = "tinyint unsigned")]
    public int Usb2Ports { get; set; }

    [Column(TypeName = "tinyint unsigned")]
    public int Usb3Ports { get; set; }

    [MaxLength(50)]
    public string Socket { get; set; } = string.Empty;

    [Column(TypeName = "tinyint unsigned")]
    public int DdrType { get; set; }

    [MaxLength(50)]
    public string ChipSet { get; set; } = string.Empty;

    [Column(TypeName = "tinyint unsigned")]
    public int RamSlots { get; set; }

    [Column(TypeName = "tinyint unsigned")]
    public int M2slots { get; set; }

    [Column(TypeName = "tinyint unsigned")]
    public int PCIeVersion { get; set; }
    
    [MaxLength(50)]
    public string FormFactor { get; set; } = string.Empty;
}

public class PowerUnit
{
    [Key]
    public int Id { get; set; }

    [MaxLength(100)]
    public string Name { get; set; } = string.Empty;

    [Column(TypeName = "smallint unsigned")]
    public int Power { get; set; }

    [MaxLength(100)]
    public string Certificate { get; set; } = string.Empty;
}

public class Cooler
{
    [Key]
    public int Id { get; set; }

    [MaxLength(100)]
    public string Name { get; set; } = string.Empty;

    [MaxLength(50)]
    public string Socket { get; set; } = string.Empty;

    [Column(TypeName = "smallint unsigned")]
    public int TdpCooling { get; set; }
}

public class PcCase
{
    [Key]
    public int Id { get; set; }

    [MaxLength(100)]
    public string Name { get; set; } = string.Empty;
    
    [MaxLength(50)]
    public string SizeType { get; set; } = string.Empty;
}

[PrimaryKey(nameof(CaseId), nameof(MotherboardFormFactor))]
public class PcCaseCompatibility
{
    public int CaseId { get; set; }

    [ForeignKey(nameof(CaseId))]
    public PcCase PcCase { get; set; } = null!;

    [MaxLength(50)]
    public string MotherboardFormFactor { get; set; } = string.Empty;
}

[PrimaryKey(nameof(CpuId), nameof(Chipset))]
public class CpuCompatibility
{
    public int CpuId { get; set; }
    [ForeignKey(nameof(CpuId))]
    public Cpu Cpu { get; set; } = null!;

    [MaxLength(50)]
    public string Chipset { get; set; } = string.Empty;
}

[PrimaryKey(nameof(PowerUnitId), nameof(ConnectorType))]
public class PsuConnector
{
    public int PowerUnitId { get; set; }
    [ForeignKey(nameof(PowerUnitId))]
    public PowerUnit PowerUnit { get; set; } = null!;

    [MaxLength(50)]
    public string ConnectorType { get; set; } = string.Empty;

    [Column(TypeName = "tinyint unsigned")]
    public int ConnectorCount { get; set; }
}

[PrimaryKey(nameof(GpuId), nameof(ConnectorType))]
public class GpuPowerRequirement
{
    public int GpuId { get; set; }
    [ForeignKey(nameof(GpuId))]
    public Gpu Gpu { get; set; } = null!;

    [MaxLength(50)]
    public string ConnectorType { get; set; } = string.Empty;

    [Column(TypeName = "tinyint unsigned")]
    public int ConnectorCount { get; set; }
}