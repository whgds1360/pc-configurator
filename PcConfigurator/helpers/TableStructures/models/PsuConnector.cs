namespace PcConfigurator.helpers.TableStructures.Models;

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