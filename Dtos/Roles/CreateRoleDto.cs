using System.ComponentModel.DataAnnotations;
using api.Models;

namespace api.Dtos.Roles;

public class CreateRoleDto
{
    [Required]
    [MaxLength (50)]
    public string Denomination { get; set; } = string.Empty;
    [Required]
    [MaxLength (100)]
    public string Description { get; set; } = string.Empty;
    public Boolean Enabled { get; set; }
    [Required]
    public List<int> Permissions { get; set; } = new List<int>();
}