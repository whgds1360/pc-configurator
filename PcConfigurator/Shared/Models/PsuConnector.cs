namespace PcConfigurator.Shared.Models;

[Table("psu_connector")]
[PrimaryKey(nameof(PowerUnitId), nameof(ConnectorType))]
public class PsuConnector
{
    [Column("power_unit_id")]
    public int PowerUnitId { get; set; }
    [ForeignKey(nameof(PowerUnitId))]
    public PowerUnit PowerUnit { get; set; } = null!;

    [MaxLength(50)]
    [Column("connector_type")]
    public string ConnectorType { get; set; } = string.Empty;

    [Column("connector_count", TypeName = "tinyint unsigned")]
    public int ConnectorCount { get; set; }
}