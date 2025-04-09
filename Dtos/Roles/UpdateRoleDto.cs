using System.ComponentModel.DataAnnotations;

namespace api.Dtos.Roles;

public class UpdateRoleDto
{
    [MaxLength (50)]
    public string Denomination { get; set; } = string.Empty;
    
    [MaxLength (100)]
    public string Description { get; set; } = string.Empty;
    
    public Boolean Enabled { get; set; }
    
    public List<int> Permissions { get; set; } = new List<int>();
}