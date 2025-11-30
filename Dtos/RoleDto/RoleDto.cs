using System.ComponentModel.DataAnnotations;

namespace Dtos.RoleDto;

public class RoleDto
{
    [Required]
    public int Id { get; set; }

    [Required]
    public string Name { get; set; }
}
