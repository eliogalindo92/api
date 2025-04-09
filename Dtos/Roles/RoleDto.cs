using api.Models;

namespace api.Dtos.Roles;

public class RoleDto
{
    public int Id { get; set; }
    public string Denomination { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public Boolean Enabled { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public List<Permission> Permissions { get; set; } = new List<Permission>();
}