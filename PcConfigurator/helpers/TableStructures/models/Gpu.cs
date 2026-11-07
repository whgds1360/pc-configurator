namespace PcConfigurator.helpers.TableStructures.Models;

public class Gpu
{
    [Key]
    public int Id { get; set; }

    [MaxLength(100)]
    public string Name { get; set; } = string.Empty;

    [Column(TypeName = "tinyint unsigned")]
    public int VRAM { get; set; }
    
    [MaxLength(50)]
    public string MemoryType { get; set; } = string.Empty;
    
    [Column(TypeName = "smallint unsigned")]
    public int MemoryBus { get; set; }
    
    [Column(TypeName = "smallint unsigned")]
    public int TDP { get; set; }
    
    [MaxLength(50)]
    public string PCIe { get; set; } = string.Empty;
}