namespace PcConfigurator.helpers.TableStructures.Models;

public class Config
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("user_login")]
    public string UserLogin { get; set; } = string.Empty;
    [ForeignKey(nameof(UserLogin))]
    public User User { get; set; } = null!;

    [MaxLength(100)]
    [Column("name")]
    public string Name { get; set; } = string.Empty;

    [Column("cpu_id")]
    public int CpuId { get; set; }
    [ForeignKey(nameof(CpuId))]
    public Cpu Cpu { get; set; } = null!;

    [Column("gpu_id")]
    public int GpuId { get; set; }
    [ForeignKey(nameof(GpuId))]
    public Gpu Gpu { get; set; } = null!;

    [Column("motherboard_id")]
    public int MotherboardId { get; set; }
    [ForeignKey(nameof(MotherboardId))]
    public Motherboard Motherboard { get; set; } = null!;

    [Column("power_unit_id")]
    public int PowerUnitId { get; set; }
    [ForeignKey(nameof(PowerUnitId))]
    public PowerUnit PowerUnit { get; set; } = null!;

    [Column("ram_id")]
    public int RamId { get; set; }
    [ForeignKey(nameof(RamId))]
    public Ram Ram { get; set; } = null!;

    [Column("pc_case_id")]
    public int PcCaseId { get; set; }
    [ForeignKey(nameof(PcCaseId))]
    public PcCase pcCase { get; set; } = null!;    

    [Column("cooler_id")]
    public int CoolerId { get; set; }
    [ForeignKey(nameof(CoolerId))]
    public Cooler Cooler { get; set; } = null!;
}