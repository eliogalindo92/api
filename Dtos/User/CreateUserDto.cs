namespace api.Dtos.User;
using System.ComponentModel.DataAnnotations;

public class CreateUserDto
{
    [Required]
    [MaxLength (50)]
    public string FullName {get; set;} = String.Empty;
    [Required]
    [MaxLength (50)]
    public string Username { get; set; } = String.Empty;
    [Required]
    [MaxLength (50)]
    public string Email { get; set; } = String.Empty;
    [Required]
    [MaxLength (100)]
    public string Password { get; set; } = String.Empty;
    [Required]
    [MaxLength (10)]
    public string Status { get; set; } = String.Empty;
}