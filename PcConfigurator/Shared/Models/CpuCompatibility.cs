namespace PcConfigurator.Shared.Models;

[Table("cpu_compatibility")]
[PrimaryKey(nameof(CpuId), nameof(Chipset))]
public class CpuCompatibility
{
    [Column("cpu_id")]
    public int CpuId { get; set; }
    [ForeignKey(nameof(CpuId))]
    public Cpu Cpu { get; set; } = null!;

    [MaxLength(50)]
    [Column("chipset")]
    public string Chipset { get; set; } = string.Empty;
}