namespace PcConfigurator.Shared.Models;

[Table("pc_case_compatibility")]
[PrimaryKey(nameof(CaseId), nameof(MotherboardFormFactor))]
public class PcCaseCompatibility
{
    [Column("case_id")]
    public int CaseId { get; set; }

    [ForeignKey(nameof(CaseId))]
    public PcCase PcCase { get; set; } = null!;

    [MaxLength(50)]
    [Column("motherboard_form_factor")]
    public string MotherboardFormFactor { get; set; } = string.Empty;
}