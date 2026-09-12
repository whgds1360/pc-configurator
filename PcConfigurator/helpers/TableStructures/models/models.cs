using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PcConfigurator.helpers.TableStructures;

public class Config
{
    [Key]
    [Column(TypeName = "varchar(100)")]
    [MaxLength(100)]
    public string Name { get; set; } = string.Empty;

    [Column(TypeName = "varchar(100)")]
    [MaxLength(100)]
    public string CpuName { get; set; } = string.Empty;

    [ForeignKey(nameof(CpuName))]
    public Cpu Cpu { get; set; } = null!;

    [Column(TypeName = "varchar(100)")]
    [MaxLength(100)]
    public string GpuName { get; set; } = string.Empty;

    [ForeignKey(nameof(GpuName))]
    public Gpu Gpu { get; set; } = null!;

    [Column(TypeName = "varchar(100)")]
    [MaxLength(100)]
    public string MotherboardName { get; set; } = string.Empty;

    [ForeignKey(nameof(MotherboardName))]
    public Motherboard Motherboard { get; set; } = null!;

    [Column(TypeName = "varchar(100)")]
    [MaxLength(100)]
    public string PowerUnitName { get; set; } = string.Empty;

    [ForeignKey(nameof(PowerUnitName))]
    public PowerUnit PowerUnit { get; set; } = null!;

    [Column(TypeName = "varchar(100)")]
    [MaxLength(100)]
    public string RamName { get; set; } = string.Empty;

    [ForeignKey(nameof(RamName))]
    public Ram Ram { get; set; } = null!;
}

public class Cpu
{
    [Key]
    [Column(TypeName = "varchar(100)")]
    [MaxLength(100)]
    public string Name { get; set; } = string.Empty;

    [Column(TypeName = "varchar(100)")]
    [MaxLength(100)]
    public string? Soket { get; set; }

    [Column(TypeName = "varchar(100)")]
    [MaxLength(100)]
    public string? Cores { get; set; }

    [Column(TypeName = "int")]
    public int TDP { get; set; }

    [Column(TypeName = "varchar(100)")]
    [MaxLength(100)]
    public string? Freq { get; set; }

    [Column(TypeName = "varchar(100)")]
    [MaxLength(100)]
    public string? L3Chache { get; set; }
}

public class Gpu
{
    [Key]
    [Column(TypeName = "varchar(100)")]
    [MaxLength(100)]
    public string Name { get; set; } = string.Empty;

    [Column(TypeName = "varchar(100)")]
    [MaxLength(100)]
    public string? VRAM { get; set; }

    [Column(TypeName = "varchar(100)")]
    [MaxLength(100)]
    public string? MemoryType { get; set; }

    [Column(TypeName = "varchar(100)")]
    [MaxLength(100)]
    public string? MemoryBus { get; set; }

    [Column(TypeName = "int")]
    public int TDP { get; set; }

    [Column(TypeName = "varchar(100)")]
    [MaxLength(100)]
    public string? PCIe { get; set; }
}

public class Ram
{
    [Key]
    [Column(TypeName = "varchar(100)")]
    [MaxLength(100)]
    public string Name { get; set; } = string.Empty;

    [Column(TypeName = "varchar(100)")]
    [MaxLength(100)]
    public string? Type { get; set; }

    [Column(TypeName = "varchar(100)")]
    [MaxLength(100)]
    public string? Volume { get; set; }

    [Column(TypeName = "varchar(100)")]
    [MaxLength(100)]
    public string? Freq { get; set; }

    [Column(TypeName = "varchar(100)")]
    [MaxLength(100)]
    public string? Timings { get; set; }
}

public class Motherboard
{
    [Key]
    [Column(TypeName = "varchar(100)")]
    [MaxLength(100)]
    public string Name { get; set; } = string.Empty;

    [Column(TypeName = "varchar(100)")]
    [MaxLength(100)]
    public string? Soket { get; set; }

    [Column(TypeName = "varchar(100)")]
    [MaxLength(100)]
    public string? ChipSet { get; set; }

    [Column(TypeName = "varchar(100)")]
    [MaxLength(100)]
    public string? RamSlots { get; set; }

    [Column(TypeName = "varchar(100)")]
    [MaxLength(100)]
    public string? M2slots { get; set; }

    [Column(TypeName = "varchar(100)")]
    [MaxLength(100)]
    public string? PCIe { get; set; }
}

public class PowerUnit
{
    [Key]
    [Column(TypeName = "varchar(100)")]
    [MaxLength(100)]
    public string Name { get; set; } = string.Empty;

    [Column(TypeName = "int")]
    public int Power { get; set; }

    [Column(TypeName = "varchar(100)")]
    [MaxLength(100)]
    public string? Sertificate { get; set; }
}