namespace PcConfigurator.helpers.TableStructures.Models;

public class Ram
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [MaxLength(100)]
    [Column("name")]
    public string Name { get; set; } = string.Empty;

    [Column("ddr_type", TypeName = "tinyint unsigned")]
    public int DdrType { get; set; }
    
    [Column("freq", TypeName = "smallint unsigned")]
    public int Freq { get; set; }
    
    [MaxLength(50)]
    [Column("timings")]
    public string Timings { get; set; } = string.Empty;
}