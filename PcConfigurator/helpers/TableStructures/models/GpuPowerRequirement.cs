namespace PcConfigurator.helpers.TableStructures;

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