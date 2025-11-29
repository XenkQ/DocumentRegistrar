using System.ComponentModel.DataAnnotations;

namespace Backend.Entities;

public class Role
{
    public int Id { get; set; }

    [MaxLength(35), Required]
    public string Name { get; set; }
}
