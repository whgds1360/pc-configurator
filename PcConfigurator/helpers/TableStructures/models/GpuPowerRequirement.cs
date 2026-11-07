namespace PcConfigurator.helpers.TableStructures.Models;

[PrimaryKey(nameof(GpuId), nameof(ConnectorType))]
public class GpuPowerRequirement
{

    [Column("gpu_id")]
    public int GpuId { get; set; }
    [ForeignKey(nameof(GpuId))]
    public Gpu Gpu { get; set; } = null!;

    [MaxLength(50)]
    [Column("connector_type")]
    public string ConnectorType { get; set; } = string.Empty;

    [Column("connector_count", TypeName = "tinyint unsigned")]
    public int ConnectorCount { get; set; }
}