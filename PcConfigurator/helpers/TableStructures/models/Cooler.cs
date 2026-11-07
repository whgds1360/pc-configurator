namespace PcConfigurator.helpers.TableStructures;

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