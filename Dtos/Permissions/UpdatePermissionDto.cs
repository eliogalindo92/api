using System.ComponentModel.DataAnnotations;

namespace api.Dtos.Permissions;

public class UpdatePermissionDto
{
    [MaxLength (50)]
    public string Denomination { get; set; } = string.Empty;
   
    [MaxLength (100)]
    public string Description { get; set; } = string.Empty;
}