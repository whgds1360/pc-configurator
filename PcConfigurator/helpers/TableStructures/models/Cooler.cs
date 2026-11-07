namespace PcConfigurator.helpers.TableStructures.Models;

public class Cooler
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [MaxLength(100)]
    [Column("name")]
    public string Name { get; set; } = string.Empty;

    [MaxLength(50)]
    [Column("socket")]
    public string Socket { get; set; } = string.Empty;

    [Column("tdp_cooling", TypeName = "smallint unsigned")]
    public int TdpCooling { get; set; }
}