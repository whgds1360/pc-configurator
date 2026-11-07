namespace PcConfigurator.helpers.TableStructures;

public class PowerUnit
{
    [Key]
    public int Id { get; set; }

    [MaxLength(100)]
    public string Name { get; set; } = string.Empty;

    [Column(TypeName = "smallint unsigned")]
    public int Power { get; set; }

    [MaxLength(100)]
    public string Certificate { get; set; } = string.Empty;
}