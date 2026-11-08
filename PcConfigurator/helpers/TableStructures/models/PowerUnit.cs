namespace PcConfigurator.helpers.TableStructures.Models;

[Table("power_units")]
public class PowerUnit
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [MaxLength(100)]
    [Column("name")]
    public string Name { get; set; } = string.Empty;

    [Column("power", TypeName = "smallint unsigned")]
    public int Power { get; set; }

    [MaxLength(100)]
    [Column("certificate")]
    public string Certificate { get; set; } = string.Empty;
}