namespace api.Models;
using System.ComponentModel.DataAnnotations;

public class Role
{
    public int Id { get; set; }
    [MaxLength (50)]
    public string Denomination { get; set; } = string.Empty;
    [MaxLength (100)]
    public string Description { get; set; } = string.Empty;
    public List<User> Users { get; set; } = new List<User>();
    public Boolean Enabled { get; set; } = true;
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public List<Permission> Permissions { get; set; } = new List<Permission>();
}