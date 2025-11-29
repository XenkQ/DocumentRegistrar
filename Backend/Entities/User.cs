using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Entities;

public class User
{
    public int Id { get; set; }

    [MaxLength(25), Required]
    public string FirstName { get; set; }

    [MaxLength(25), Required]
    public string LastName { get; set; }

    [EmailAddress, Required]
    public string Email { get; set; }

    [Required]
    public string PasswordHash { get; set; }

    [Required, ForeignKey("RoleId")]
    public int RoleId { get; set; }

    [Required]
    public Role Role { get; set; }
}
