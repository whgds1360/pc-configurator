namespace PcConfigurator.Shared.Models;

[Table("motherboards")]
public class Motherboard
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [MaxLength(100)]
    [Column("name")]
    public string Name { get; set; } = string.Empty;

    [Column("usb_2_ports", TypeName = "tinyint unsigned")]
    public int Usb2Ports { get; set; }

    [Column("usb_3_ports", TypeName = "tinyint unsigned")]
    public int Usb3Ports { get; set; }

    [MaxLength(50)]
    [Column("socket")]
    public string Socket { get; set; } = string.Empty;

    [Column("ddr_type", TypeName = "tinyint unsigned")]
    public int DdrType { get; set; }

    [MaxLength(50)]
    [Column("chipset")]
    public string ChipSet { get; set; } = string.Empty;

    [Column("ram_slots", TypeName = "tinyint unsigned")]
    public int RamSlots { get; set; }

    [Column("m2_slots", TypeName = "tinyint unsigned")]
    public int M2slots { get; set; }

    [Column("pcie_version", TypeName = "tinyint unsigned")]
    public int PCIeVersion { get; set; }
    
    [MaxLength(50)]
    [Column("form_factor")]
    public string FormFactor { get; set; } = string.Empty;
}