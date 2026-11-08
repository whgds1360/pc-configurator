namespace PcConfigurator.Shared.Models;

[Table("gpus")]
public class Gpu
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [MaxLength(100)]
    [Column("name")]
    public string Name { get; set; } = string.Empty;

    [Column("vram", TypeName = "tinyint unsigned")]
    public int VRAM { get; set; }
    
    [MaxLength(50)]
    [Column("memory_type")]
    public string MemoryType { get; set; } = string.Empty;
    
    [Column("memory_bus", TypeName = "smallint unsigned")]
    public int MemoryBus { get; set; }
    
    [Column("tdp", TypeName = "smallint unsigned")]
    public int TDP { get; set; }
    
    [MaxLength(50)]
    [Column("pcie")]
    public string PCIe { get; set; } = string.Empty;
}