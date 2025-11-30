using System.ComponentModel.DataAnnotations;

namespace Dtos.UserDtos;

public class UpdateUserDto
{
    [MaxLength(25)]
    public string FirstName { get; set; }

    [MaxLength(25)]
    public string LastName { get; set; }

    [EmailAddress]
    public string Email { get; set; }

    [MaxLength(200)]
    public string Password { get; set; }

    public int RoleId { get; set; }
}
