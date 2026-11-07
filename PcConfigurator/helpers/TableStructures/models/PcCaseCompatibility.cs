namespace PcConfigurator.helpers.TableStructures.Models;

[PrimaryKey(nameof(CaseId), nameof(MotherboardFormFactor))]
public class PcCaseCompatibility
{
    public int CaseId { get; set; }

    [ForeignKey(nameof(CaseId))]
    public PcCase PcCase { get; set; } = null!;

    [MaxLength(50)]
    public string MotherboardFormFactor { get; set; } = string.Empty;
}