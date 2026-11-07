namespace PcConfigurator.helpers.TableStructures.Models;
public class Motherboard
{
    [Key]
    public int Id { get; set; }

    [MaxLength(100)]
    public string Name { get; set; } = string.Empty;

    [Column(TypeName = "tinyint unsigned")]
    public int Usb2Ports { get; set; }

    [Column(TypeName = "tinyint unsigned")]
    public int Usb3Ports { get; set; }

    [MaxLength(50)]
    public string Socket { get; set; } = string.Empty;

    [Column(TypeName = "tinyint unsigned")]
    public int DdrType { get; set; }

    [MaxLength(50)]
    public string ChipSet { get; set; } = string.Empty;

    [Column(TypeName = "tinyint unsigned")]
    public int RamSlots { get; set; }

    [Column(TypeName = "tinyint unsigned")]
    public int M2slots { get; set; }

    [Column(TypeName = "tinyint unsigned")]
    public int PCIeVersion { get; set; }
    
    [MaxLength(50)]
    public string FormFactor { get; set; } = string.Empty;
}