namespace api.Models;
using System.ComponentModel.DataAnnotations;

public class User
{
    public int Id { get; set; }
    [MaxLength (50)]
    public string FullName {get; set;} = String.Empty;
    [MaxLength (50)]
    public string Username { get; set; } = String.Empty;
    [MaxLength (50)]
    public string Email { get; set; } = String.Empty;
    [MaxLength (100)]
    public string Password { get; set; } = String.Empty; // Stores hashed password
    [MaxLength (10)]
    public string Status { get; set; } = String.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.Now;
}