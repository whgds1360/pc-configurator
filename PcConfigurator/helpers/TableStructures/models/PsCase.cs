namespace PcConfigurator.helpers.TableStructures.Models;

public class PcCase
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [MaxLength(100)]
    [Column("name")]
    public string Name { get; set; } = string.Empty;
    
    [MaxLength(50)]
    [Column("size_type")]
    public string SizeType { get; set; } = string.Empty;
}