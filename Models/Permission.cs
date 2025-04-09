namespace api.Models;
using System.ComponentModel.DataAnnotations;

public class Permission
{
    public int Id { get; set; }
    [MaxLength (50)]
    public string Denomination { get; set; } = string.Empty;
    [MaxLength (100)]
    public string Description { get; set; } = string.Empty;
    
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public List<Role> Roles { get; set; } = new List<Role>();
}