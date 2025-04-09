namespace api.Dtos.User;
using System.ComponentModel.DataAnnotations;

public class UpdateUserDto
{
    [MaxLength (50)]
    public string FullName {get; set;} = String.Empty;
    [MaxLength (50)]
    public string Username { get; set; } = String.Empty;
    [MaxLength (50)]
    public string Email { get; set; } = String.Empty;
    [MaxLength (100)]
    public string Password { get; set; } = String.Empty;
    [MaxLength (10)]
    public string Status { get; set; } = String.Empty;
    
    public List<int> Roles { get; set; } = new List<int>();
}