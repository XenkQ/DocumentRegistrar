using System.ComponentModel.DataAnnotations;

namespace Dtos.UserDtos;

public class UserDto
{
    [Required]
    public int Id { get; set; }

    [MaxLength(25), Required]
    public string FirstName { get; set; }

    [MaxLength(25), Required]
    public string LastName { get; set; }

    [EmailAddress, Required]
    public string Email { get; set; }

    public string RoleName { get; set; }
}
