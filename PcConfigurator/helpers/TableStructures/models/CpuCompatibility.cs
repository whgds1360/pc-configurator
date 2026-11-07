namespace PcConfigurator.helpers.TableStructures.Models;

[PrimaryKey(nameof(CpuId), nameof(Chipset))]
public class CpuCompatibility
{
    public int CpuId { get; set; }
    [ForeignKey(nameof(CpuId))]
    public Cpu Cpu { get; set; } = null!;

    [MaxLength(50)]
    public string Chipset { get; set; } = string.Empty;
}