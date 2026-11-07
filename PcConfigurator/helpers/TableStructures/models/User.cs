namespace PcConfigurator.helpers.TableStructures;

public class User
{
    [Key]
    [Required]
    [MaxLength(100)]
    public string Login { get; set; } = string.Empty;
    
    [Required]
    [MaxLength(20)]
    public string Password { get; set; } = string.Empty;

    [Required]
    public Role role {get; set; }
}