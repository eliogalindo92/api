using api.Dtos.Roles;

namespace api.Dtos.User;
using Models;
using System.ComponentModel.DataAnnotations;

public class UserDto
{
    public int Id { get; set; }
    [MaxLength (50)]
    public string FullName {get; set;} = String.Empty;
    [MaxLength (50)]
    public string Username { get; set; } = String.Empty;
    [MaxLength (50)]
    public string Email { get; set; } = String.Empty;
    [MaxLength (10)]
    public string Status { get; set; } = String.Empty;
    
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    
    public List<RoleDto> Roles { get; set; } 
}