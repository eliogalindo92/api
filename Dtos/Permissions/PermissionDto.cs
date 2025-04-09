namespace api.Dtos.Permissions;

public class PermissionDto
{
    public int Id { get; set; }
    public string Denomination { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    
    public DateTime CreatedAt { get; set; } = DateTime.Now;
}