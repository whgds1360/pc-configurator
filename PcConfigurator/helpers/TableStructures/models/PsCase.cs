namespace PcConfigurator.helpers.TableStructures.Models;

public class PcCase
{
    [Key]
    public int Id { get; set; }

    [MaxLength(100)]
    public string Name { get; set; } = string.Empty;
    
    [MaxLength(50)]
    public string SizeType { get; set; } = string.Empty;
}