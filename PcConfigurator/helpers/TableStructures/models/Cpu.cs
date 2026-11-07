namespace PcConfigurator.helpers.TableStructures;

public class Cpu
{
    [Key]
    public int Id { get; set; }

    [MaxLength(100)]
    public string Name { get; set; } = string.Empty;

    [MaxLength(50)]
    public string Socket { get; set; } = string.Empty;

    [Column(TypeName = "tinyint unsigned")]
    public int Cores { get; set; }
    
    [Column(TypeName = "smallint unsigned")]
    public int TDP { get; set; }

    [Column(TypeName = "decimal(3,2)")]
    public decimal Freq { get; set; }

    [Column(TypeName = "tinyint unsigned")]
    public int L3Cache { get; set; }
}