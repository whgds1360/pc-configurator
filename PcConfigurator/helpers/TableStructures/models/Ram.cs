namespace PcConfigurator.helpers.TableStructures;

public class Ram
{
    [Key]
    public int Id { get; set; }

    [MaxLength(100)]
    public string Name { get; set; } = string.Empty;

    [Column(TypeName = "tinyint unsigned")]
    public int DdrType { get; set; }
    
    [Column(TypeName = "smallint unsigned")]
    public int Freq { get; set; }
    
    [MaxLength(50)]
    public string Timings { get; set; } = string.Empty;
}