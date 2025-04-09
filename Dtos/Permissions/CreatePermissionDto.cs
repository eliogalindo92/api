using System.ComponentModel.DataAnnotations;

namespace api.Dtos.Permissions;

public class CreatePermissionDto
{
    [Required]
    [MaxLength (50)]
    public string Denomination { get; set; } = string.Empty;
    [Required]
    [MaxLength (100)]
    public string Description { get; set; } = string.Empty;
}