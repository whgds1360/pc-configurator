namespace PcConfigurator.helpers.TableStructures.Models;

public class Config
{
    [Key]
    public int Id { get; set; }

    public string UserLogin { get; set; } = string.Empty;
    [ForeignKey(nameof(UserLogin))]
    public User User { get; set; } = null!;

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