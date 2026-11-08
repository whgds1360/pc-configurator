namespace PcConfigurator.helpers.TableStructures.Models;

[Table("cpus")]
public class Cpu
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

    [Column("cores", TypeName = "tinyint unsigned")]
    public int Cores { get; set; }
    
    [Column("tdp", TypeName = "smallint unsigned")]
    public int TDP { get; set; }

    [Column("freq", TypeName = "decimal(3,2)")]
    public decimal Freq { get; set; }

    [Column("l3cache", TypeName = "tinyint unsigned")]
    public int L3Cache { get; set; }
}