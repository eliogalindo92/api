namespace api.Dtos.Roles;
using Permissions;

public class RoleDto
{
    public int Id { get; set; }
    public string Denomination { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public Boolean Enabled { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public List<PermissionDto> Permissions { get; set; } = new List<PermissionDto>();
}