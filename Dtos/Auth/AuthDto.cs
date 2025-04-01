namespace api.Dtos.Auth;
using System.ComponentModel.DataAnnotations;

public class AuthDto
{
    [MaxLength (50)]
    [Required]
    public string Username { get; set; } = String.Empty;
    [MaxLength (100)]
    [Required]
    public string Password { get; set; } = String.Empty;
}