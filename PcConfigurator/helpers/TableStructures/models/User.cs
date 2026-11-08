namespace PcConfigurator.helpers.TableStructures.Models;

public class User
{
    [Key]
    [Required]
    [MaxLength(100)]
    [Column("login")]
    public string Login { get; set; } = string.Empty;
    
    [Required]
    [MaxLength(20)]
    [Column("password")]
    public string Password { get; set; } = string.Empty;

    [Required]
    [Column("role")]
    public Role role {get; set; }
}