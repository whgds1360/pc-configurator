namespace PcConfigurator.Shared.Models;

[Table("users")]
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
    [MaxLength(10)]
    [Column("role")]
    public string Role {get; set; } = string.Empty;
}